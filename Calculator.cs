using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CalculatorApp
{
    internal static class GraphicsUtil
    {
        public static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        public static Region RoundedRegion(Rectangle r, int radius)
        {
            using (GraphicsPath p = RoundedRect(r, radius))
                return new Region(p);
        }
    }

    public class RoundButton : Button
    {
        private bool hovering;
        private bool pressed;

        public Color BaseColor { get; set; }
        public Color HoverColor { get; set; }
        public Color PressColor { get; set; }

        public RoundButton()
        {
            BaseColor = Color.FromArgb(52, 58, 70);
            HoverColor = Color.FromArgb(62, 69, 82);
            PressColor = Color.FromArgb(39, 44, 53);
            Font = new Font("Segoe UI", 17, FontStyle.Bold);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            TabStop = false;
            Cursor = Cursors.Hand;
            ForeColor = Color.FromArgb(238, 241, 246);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                   | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                   | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); hovering = true; Invalidate(); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); hovering = false; pressed = false; Invalidate(); }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left) { pressed = true; Invalidate(); }
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            pressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle(1, 0, Width - 2, Height - 1);

            using (GraphicsPath shadow = GraphicsUtil.RoundedRect(
                new Rectangle(r.X, r.Y + 2, r.Width, r.Height), 16))
            using (SolidBrush sb = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                g.FillPath(sb, shadow);

            Color fill = pressed ? PressColor : (hovering ? HoverColor : BaseColor);
            using (GraphicsPath body = GraphicsUtil.RoundedRect(r, 16))
            {
                using (SolidBrush b = new SolidBrush(fill)) g.FillPath(b, body);
                using (GraphicsPath top = GraphicsUtil.RoundedRect(
                    new Rectangle(r.X, r.Y + 1, r.Width - 2, r.Height - 2), 15))
                using (Pen p = new Pen(Color.FromArgb(35, 255, 255, 255)))
                    g.DrawPath(p, top);
            }

            TextRenderer.DrawText(g, Text, Font, r, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                | TextFormatFlags.SingleLine);
        }
    }

    public class TitleBar : Control
    {
        private const int BarHeight = 44;
        private const int BtnWidth = 52;
        private const string TitleText = "Calculator";

        private bool hoverClose;
        private bool hoverMin;
        private Rectangle closeRect;
        private Rectangle minRect;
        private Font titleFont;

        public TitleBar()
        {
            Dock = DockStyle.Top;
            Height = BarHeight;
            titleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                   | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public event EventHandler MinimizeClicked;
        public event EventHandler CloseClicked;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            closeRect = new Rectangle(Width - BtnWidth, 0, BtnWidth, BarHeight);
            minRect = new Rectangle(Width - BtnWidth * 2, 0, BtnWidth, BarHeight);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool hc = closeRect.Contains(e.Location);
            bool hm = minRect.Contains(e.Location);
            if (hc != hoverClose || hm != hoverMin)
            {
                hoverClose = hc;
                hoverMin = hm;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (hoverClose || hoverMin)
            {
                hoverClose = false;
                hoverMin = false;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;

            if (closeRect.Contains(e.Location))
            {
                if (CloseClicked != null) CloseClicked(this, EventArgs.Empty);
                return;
            }
            if (minRect.Contains(e.Location))
            {
                if (MinimizeClicked != null) MinimizeClicked(this, EventArgs.Empty);
                return;
            }

            ReleaseCapture();
            SendMessage(FindForm().Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush bg = new SolidBrush(Color.FromArgb(38, 43, 53)))
                g.FillRectangle(bg, ClientRectangle);

            SizeF titleSize = g.MeasureString(TitleText, titleFont);
            TextRenderer.DrawText(g, TitleText, titleFont,
                new Point(18, (int)((BarHeight - titleSize.Height) / 2)),
                Color.FromArgb(223, 227, 234), TextFormatFlags.NoPadding);

            DrawGlyph(g, minRect, Color.FromArgb(154, 160, 171),
                hoverMin ? Color.FromArgb(64, 255, 255, 255) : Color.Transparent, false);
            DrawGlyph(g, closeRect, Color.FromArgb(154, 160, 171),
                hoverClose ? Color.FromArgb(225, 83, 79) : Color.Transparent, true);
        }

        private void DrawGlyph(Graphics g, Rectangle rect, Color color, Color hover, bool close)
        {
            if (hover.A > 0)
            {
                using (SolidBrush b = new SolidBrush(hover))
                    g.FillRectangle(b, rect);
            }
            using (Pen p = new Pen(color, 1.6f))
            {
                p.StartCap = LineCap.Round;
                p.EndCap = LineCap.Round;
                int cx = rect.X + rect.Width / 2;
                int cy = rect.Y + rect.Height / 2;
                if (close)
                {
                    g.DrawLine(p, cx - 7, cy - 7, cx + 7, cy + 7);
                    g.DrawLine(p, cx - 7, cy + 7, cx + 7, cy - 7);
                }
                else
                {
                    g.DrawLine(p, cx - 8, cy, cx + 8, cy);
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }

    public class DisplayPanel : Panel
    {
        public string ExpressionText { get; set; }
        public string ResultText { get; set; }
        public Font ResultFont { get; set; }
        public Font ExpressionFont { get; set; }

        public DisplayPanel()
        {
            ExpressionText = "";
            ResultText = "0";
            ExpressionFont = new Font("Segoe UI", 12.5f);
            ResultFont = new Font("Segoe UI", 38, FontStyle.Bold);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                   | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle body = new Rectangle(20, 8, Width - 40, Height - 16);
            using (GraphicsPath p = GraphicsUtil.RoundedRect(body, 18))
            using (SolidBrush b = new SolidBrush(Color.FromArgb(20, 24, 32)))
                g.FillPath(b, p);

            Rectangle area = new Rectangle(body.X + 22, body.Y + 14, body.Width - 44, body.Height - 28);

            if (ExpressionText.Length > 0)
            {
                TextRenderer.DrawText(g, ExpressionText, ExpressionFont,
                    new Rectangle(area.X, area.Y, area.Width, 26),
                    Color.FromArgb(126, 134, 148), TextFormatFlags.Right | TextFormatFlags.Bottom
                    | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            }

            Color resultColor = ResultText == "Error"
                ? Color.FromArgb(255, 107, 107)
                : Color.White;
            TextRenderer.DrawText(g, ResultText, ResultFont, area, resultColor,
                TextFormatFlags.Right | TextFormatFlags.Bottom | TextFormatFlags.SingleLine
                | TextFormatFlags.EndEllipsis);
        }
    }

    public class MainForm : Form
    {
        private DisplayPanel display;
        private string current = "0";
        private string previous = null;
        private string operatorSymbol = null;
        private bool resetOnNext = false;
        private string expressionHistory = "";

        public MainForm()
        {
            Text = "Calculator";
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(30, 34, 42);
            ClientSize = new Size(360, 560);
            StartPosition = FormStartPosition.CenterScreen;

            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 5,
                Padding = new Padding(18, 4, 18, 18),
                BackColor = Color.FromArgb(30, 34, 42)
            };
            for (int i = 0; i < 4; i++) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int i = 0; i < 5; i++) panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            RoundButton ac = AddButton(panel, "AC", 0, 0,
                Color.FromArgb(64, 51, 58), Color.FromArgb(75, 59, 68), Color.FromArgb(47, 38, 46));
            ac.ForeColor = Color.FromArgb(243, 154, 160);
            ac.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            RoundButton del = AddButton(panel, "\u232B", 1, 0,
                Color.FromArgb(64, 51, 58), Color.FromArgb(75, 59, 68), Color.FromArgb(47, 38, 46));
            del.ForeColor = Color.FromArgb(154, 163, 178);

            AddButton(panel, "%", 2, 0, Amber(0), Amber(0.12f), Amber(-0.12f));
            AddButton(panel, "\u00F7", 3, 0, Amber(0), Amber(0.12f), Amber(-0.12f));

            AddButton(panel, "7", 0, 1);
            AddButton(panel, "8", 1, 1);
            AddButton(panel, "9", 2, 1);
            AddButton(panel, "\u00D7", 3, 1, Amber(0), Amber(0.12f), Amber(-0.12f));

            AddButton(panel, "4", 0, 2);
            AddButton(panel, "5", 1, 2);
            AddButton(panel, "6", 2, 2);
            AddButton(panel, "\u2212", 3, 2, Amber(0), Amber(0.12f), Amber(-0.12f));

            AddButton(panel, "1", 0, 3);
            AddButton(panel, "2", 1, 3);
            AddButton(panel, "3", 2, 3);
            AddButton(panel, "+", 3, 3, Amber(0), Amber(0.12f), Amber(-0.12f));

            AddButton(panel, "0", 0, 4);
            AddButton(panel, ".", 1, 4);
            RoundButton equals = AddButton(panel, "=", 2, 4,
                Color.FromArgb(108, 123, 255), Color.FromArgb(130, 144, 255), Color.FromArgb(87, 98, 230));
            equals.ForeColor = Color.White;
            panel.SetColumnSpan(equals, 2);

            display = new DisplayPanel { Dock = DockStyle.Top, Height = 145 };

            TitleBar titleBar = new TitleBar();
            titleBar.MinimizeClicked += (s, e) => WindowState = FormWindowState.Minimized;
            titleBar.CloseClicked += (s, e) => Close();

            Controls.Add(panel);
            Controls.Add(display);
            Controls.Add(titleBar);

            KeyPreview = true;
            KeyDown += MainForm_KeyDown;
        }

        private static Color Amber(float delta)
        {
            float r = Math.Max(0, Math.Min(1, 240f / 255f + delta));
            float g = Math.Max(0, Math.Min(1, 163f / 255f + delta));
            float b = Math.Max(0, Math.Min(1, 63f / 255f + delta));
            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Region = GraphicsUtil.RoundedRegion(new Rectangle(0, 0, Width, Height), 14);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }

        private RoundButton AddButton(TableLayoutPanel panel, string text, int col, int row,
            Color? normal = null, Color? hover = null, Color? press = null)
        {
            RoundButton b = new RoundButton { Text = text, Margin = new Padding(6) };
            if (normal.HasValue)
            {
                b.BaseColor = normal.Value;
                b.HoverColor = hover.HasValue ? hover.Value : ControlPaint.Light(normal.Value, 0.12f);
                b.PressColor = press.HasValue ? press.Value : ControlPaint.Dark(normal.Value, 0.15f);
            }
            b.Click += Button_Click;
            panel.Controls.Add(b, col, row);
            return b;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            RoundButton b = (RoundButton)sender;
            switch (b.Text)
            {
                case "AC": ClearAll(); break;
                case "\u232B": Backspace(); break;
                case "=": Calculate(); break;
                case "+": SetOperator("+"); break;
                case "\u2212": SetOperator("-"); break;
                case "\u00D7": SetOperator("*"); break;
                case "\u00F7": SetOperator("/"); break;
                case "%": SetOperator("%"); break;
                case ".": InputDot(); break;
                default: InputDigit(b.Text); break;
            }
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            display.ExpressionText = expressionHistory;
            display.ResultText = current;
            display.Invalidate();
            if (current == "Error")
            {
                previous = null;
                operatorSymbol = null;
            }
        }

        private void InputDigit(string digit)
        {
            if (current == "Error") resetOnNext = true;
            if (resetOnNext)
            {
                current = digit;
                resetOnNext = false;
                expressionHistory = "";
            }
            else if (current == "0")
            {
                current = digit;
            }
            else
            {
                current += digit;
            }
        }

        private void InputDot()
        {
            if (current == "Error") resetOnNext = true;
            if (resetOnNext)
            {
                current = "0.";
                resetOnNext = false;
                expressionHistory = "";
            }
            else if (!current.Contains("."))
            {
                current += ".";
            }
        }

        private void SetOperator(string op)
        {
            if (current == "Error") return;
            if (operatorSymbol != null)
            {
                Calculate();
            }
            previous = current;
            operatorSymbol = op;
            expressionHistory = current + " " + Symbol(op) + " ";
            resetOnNext = true;
        }

        private void Calculate()
        {
            if (operatorSymbol == null || previous == null) return;
            double a;
            double b;
            if (!double.TryParse(previous, NumberStyles.Float, CultureInfo.InvariantCulture, out a) ||
                !double.TryParse(current, NumberStyles.Float, CultureInfo.InvariantCulture, out b)) return;

            string result;
            switch (operatorSymbol)
            {
                case "+": result = FormatResult(a + b); break;
                case "-": result = FormatResult(a - b); break;
                case "*": result = FormatResult(a * b); break;
                case "/": result = b == 0 ? "Error" : FormatResult(a / b); break;
                case "%": result = b == 0 ? "Error" : FormatResult(a % b); break;
                default: return;
            }

            expressionHistory = previous + " " + Symbol(operatorSymbol) + " " + current + " =";
            current = result;
            previous = null;
            operatorSymbol = null;
            resetOnNext = true;
            UpdateDisplay();
        }

        private static string FormatResult(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) return "Error";
            return value.ToString("G12", CultureInfo.InvariantCulture);
        }

        private static string Symbol(string op)
        {
            switch (op)
            {
                case "+": return "+";
                case "-": return "\u2212";
                case "*": return "\u00D7";
                case "/": return "\u00F7";
                case "%": return "%";
                default: return op;
            }
        }

        private void ClearAll()
        {
            current = "0";
            previous = null;
            operatorSymbol = null;
            resetOnNext = false;
            expressionHistory = "";
        }

        private void Backspace()
        {
            if (resetOnNext || current == "Error") return;
            if (current.Length > 1)
            {
                current = current.Substring(0, current.Length - 1);
            }
            else
            {
                current = "0";
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                InputDigit((e.KeyCode - Keys.D0).ToString());
                UpdateDisplay();
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                InputDigit((e.KeyCode - Keys.NumPad0).ToString());
                UpdateDisplay();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) { SetOperator("+"); UpdateDisplay(); }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) { SetOperator("-"); UpdateDisplay(); }
            else if (e.KeyCode == Keys.Multiply) { SetOperator("*"); UpdateDisplay(); }
            else if (e.KeyCode == Keys.Divide) { SetOperator("/"); UpdateDisplay(); }
            else if (e.Shift && e.KeyCode == Keys.D5) { SetOperator("%"); UpdateDisplay(); }
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal) { InputDot(); UpdateDisplay(); }
            else if (e.KeyCode == Keys.Enter) { Calculate(); }
            else if (e.KeyCode == Keys.Back) { Backspace(); UpdateDisplay(); }
            else if (e.KeyCode == Keys.Escape) { ClearAll(); UpdateDisplay(); }
            e.Handled = true;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}