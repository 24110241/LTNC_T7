namespace quan_ly_sv
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Control fields
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterLop;
        private System.Windows.Forms.NumericUpDown nudFilterDiem;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView dgvStudents;


        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlButtons = new Panel();
            btnRefresh = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            pnlFilter = new Panel();
            lblTuKhoa = new Label();
            txtSearch = new TextBox();
            cboFilterLop = new ComboBox();
            nudFilterDiem = new NumericUpDown();
            btnSearch = new Button();
            btnShowAll = new Button();
            dgvStudents = new DataGridView();
            cboNganh = new ComboBox();
            lblNganh = new Label();
            lblDiem = new Label();
            nudDiem = new NumericUpDown();
            tblInfo = new TableLayoutPanel();
            lblMaSV = new Label();
            txtMaSV = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblNgaySinh = new Label();
            lblGioiTinh = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDienThoai = new Label();
            txtDienThoai = new TextBox();
            pnlGender = new Panel();
            rbNam = new RadioButton();
            rbNu = new RadioButton();
            dtpNgaySinh = new DateTimePicker();
            grpInfo = new GroupBox();
            lblLop = new Label();
            cboLop = new ComboBox();
            pnlHeader.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFilterDiem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            tblInfo.SuspendLayout();
            pnlGender.SuspendLayout();
            grpInfo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(18, 92, 160);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(980, 56);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(16, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(237, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ SINH VIÊN";
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Controls.Add(btnAdd);
            pnlButtons.Controls.Add(btnEdit);
            pnlButtons.Controls.Add(btnDelete);
            pnlButtons.Location = new Point(12, 234);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(1202, 44);
            pnlButtons.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(530, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(84, 32);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "Làm mới";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(620, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 32);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Thêm";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(730, 6);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 32);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Sửa";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(840, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 32);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Xóa";
            btnDelete.Click += btnDelete_Click;
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(lblTuKhoa);
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(cboFilterLop);
            pnlFilter.Controls.Add(nudFilterDiem);
            pnlFilter.Controls.Add(btnSearch);
            pnlFilter.Controls.Add(btnShowAll);
            pnlFilter.Location = new Point(12, 292);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(956, 40);
            pnlFilter.TabIndex = 3;
            // 
            // lblTuKhoa
            // 
            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(6, 11);
            lblTuKhoa.Name = "lblTuKhoa";
            lblTuKhoa.Size = new Size(62, 20);
            lblTuKhoa.TabIndex = 5;
            lblTuKhoa.Text = "Từ khóa";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(72, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
            txtSearch.Size = new Size(320, 27);
            txtSearch.TabIndex = 0;
            // 
            // cboFilterLop
            // 
            cboFilterLop.Location = new Point(402, 8);
            cboFilterLop.Name = "cboFilterLop";
            cboFilterLop.Size = new Size(180, 28);
            cboFilterLop.TabIndex = 1;
            // 
            // nudFilterDiem
            // 
            nudFilterDiem.DecimalPlaces = 1;
            nudFilterDiem.Location = new Point(592, 8);
            nudFilterDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudFilterDiem.Name = "nudFilterDiem";
            nudFilterDiem.Size = new Size(80, 27);
            nudFilterDiem.TabIndex = 2;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(688, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(93, 31);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Location = new Point(787, 6);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(120, 31);
            btnShowAll.TabIndex = 4;
            btnShowAll.Text = "Hiển thị tất cả";
            btnShowAll.Click += btnShowAll_Click;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeight = 29;
            dgvStudents.Location = new Point(12, 338);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(1202, 292);
            dgvStudents.TabIndex = 4;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
            // 
            // cboNganh
            // 
            cboNganh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNganh.Location = new Point(1016, 32);
            cboNganh.Name = "cboNganh";
            cboNganh.Size = new Size(156, 28);
            cboNganh.TabIndex = 13;
            // 
            // lblNganh
            // 
            lblNganh.Anchor = AnchorStyles.Left;
            lblNganh.AutoSize = true;
            lblNganh.Location = new Point(948, 35);
            lblNganh.Name = "lblNganh";
            lblNganh.Size = new Size(53, 20);
            lblNganh.TabIndex = 12;
            lblNganh.Text = "Ngành";
            // 
            // lblDiem
            // 
            lblDiem.Anchor = AnchorStyles.Left;
            lblDiem.AutoSize = true;
            lblDiem.Location = new Point(948, 106);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(45, 20);
            lblDiem.TabIndex = 3;
            lblDiem.Text = "Điểm";
            // 
            // nudDiem
            // 
            nudDiem.DecimalPlaces = 1;
            nudDiem.Location = new Point(1016, 104);
            nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(120, 27);
            nudDiem.TabIndex = 4;
            // 
            // tblInfo
            // 
            tblInfo.ColumnCount = 4;
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tblInfo.Controls.Add(lblMaSV, 0, 0);
            tblInfo.Controls.Add(txtMaSV, 1, 0);
            tblInfo.Controls.Add(lblHoTen, 2, 0);
            tblInfo.Controls.Add(txtHoTen, 3, 0);
            tblInfo.Controls.Add(lblNgaySinh, 0, 1);
            tblInfo.Controls.Add(lblGioiTinh, 2, 1);
            tblInfo.Controls.Add(lblEmail, 0, 2);
            tblInfo.Controls.Add(txtEmail, 1, 2);
            tblInfo.Controls.Add(lblDienThoai, 2, 2);
            tblInfo.Controls.Add(txtDienThoai, 3, 2);
            tblInfo.Controls.Add(pnlGender, 3, 1);
            tblInfo.Controls.Add(dtpNgaySinh, 1, 1);
            tblInfo.Location = new Point(10, 26);
            tblInfo.Name = "tblInfo";
            tblInfo.RowCount = 3;
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblInfo.Size = new Size(932, 140);
            tblInfo.TabIndex = 0;
            // 
            // lblMaSV
            // 
            lblMaSV.Anchor = AnchorStyles.Left;
            lblMaSV.AutoSize = true;
            lblMaSV.Location = new Point(3, 13);
            lblMaSV.Name = "lblMaSV";
            lblMaSV.Size = new Size(91, 20);
            lblMaSV.TabIndex = 0;
            lblMaSV.Text = "Mã sinh viên";
            // 
            // txtMaSV
            // 
            txtMaSV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtMaSV.Location = new Point(170, 9);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(292, 27);
            txtMaSV.TabIndex = 1;
            txtMaSV.Leave += txtMaSV_Leave;
            // 
            // lblHoTen
            // 
            lblHoTen.Anchor = AnchorStyles.Left;
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(468, 13);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Họ và tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtHoTen.Location = new Point(635, 9);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(294, 27);
            txtHoTen.TabIndex = 3;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.Anchor = AnchorStyles.Left;
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(3, 59);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(74, 20);
            lblNgaySinh.TabIndex = 4;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.Anchor = AnchorStyles.Left;
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Location = new Point(468, 59);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(65, 20);
            lblGioiTinh.TabIndex = 6;
            lblGioiTinh.Text = "Giới tính";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left;
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(3, 106);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Location = new Point(170, 102);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(292, 27);
            txtEmail.TabIndex = 9;
            // 
            // lblDienThoai
            // 
            lblDienThoai.Anchor = AnchorStyles.Left;
            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(468, 106);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(78, 20);
            lblDienThoai.TabIndex = 10;
            lblDienThoai.Text = "Điện thoại";
            // 
            // txtDienThoai
            // 
            txtDienThoai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDienThoai.Location = new Point(635, 102);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(294, 27);
            txtDienThoai.TabIndex = 11;
            // 
            // pnlGender
            // 
            pnlGender.Controls.Add(rbNam);
            pnlGender.Controls.Add(rbNu);
            pnlGender.Location = new Point(635, 49);
            pnlGender.Name = "pnlGender";
            pnlGender.Size = new Size(200, 34);
            pnlGender.TabIndex = 7;
            // 
            // rbNam
            // 
            rbNam.AutoSize = true;
            rbNam.Location = new Point(3, 8);
            rbNam.Name = "rbNam";
            rbNam.Size = new Size(62, 24);
            rbNam.TabIndex = 0;
            rbNam.Text = "Nam";
            // 
            // rbNu
            // 
            rbNu.AutoSize = true;
            rbNu.Location = new Point(121, 8);
            rbNu.Name = "rbNu";
            rbNu.Size = new Size(50, 24);
            rbNu.TabIndex = 1;
            rbNu.Text = "Nữ";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(170, 49);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(200, 27);
            dtpNgaySinh.TabIndex = 5;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(tblInfo);
            grpInfo.Controls.Add(lblLop);
            grpInfo.Controls.Add(nudDiem);
            grpInfo.Controls.Add(cboLop);
            grpInfo.Controls.Add(lblDiem);
            grpInfo.Controls.Add(lblNganh);
            grpInfo.Controls.Add(cboNganh);
            grpInfo.Location = new Point(12, 62);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(1202, 170);
            grpInfo.TabIndex = 1;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin sinh viên";
            // 
            // lblLop
            // 
            lblLop.Anchor = AnchorStyles.Left;
            lblLop.AutoSize = true;
            lblLop.Location = new Point(948, 69);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(62, 20);
            lblLop.TabIndex = 1;
            lblLop.Text = "Lớp học";
            // 
            // cboLop
            // 
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(1016, 66);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(156, 28);
            cboLop.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1219, 650);
            Controls.Add(pnlHeader);
            Controls.Add(grpInfo);
            Controls.Add(pnlButtons);
            Controls.Add(pnlFilter);
            Controls.Add(dgvStudents);
            Name = "Form1";
            Text = "Ứng dụng quản lý sinh viên";
            Load += Form1_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlButtons.ResumeLayout(false);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFilterDiem).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
            tblInfo.ResumeLayout(false);
            tblInfo.PerformLayout();
            pnlGender.ResumeLayout(false);
            pnlGender.PerformLayout();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cboNganh;
        private Label lblNganh;
        private Label lblDiem;
        private NumericUpDown nudDiem;
        private TableLayoutPanel tblInfo;
        private Label lblMaSV;
        private TextBox txtMaSV;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblNgaySinh;
        private Label lblGioiTinh;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDienThoai;
        private TextBox txtDienThoai;
        private Panel pnlGender;
        private RadioButton rbNam;
        private RadioButton rbNu;
        private DateTimePicker dtpNgaySinh;
        private GroupBox grpInfo;
        private Label lblLop;
        private ComboBox cboLop;
    }
}
