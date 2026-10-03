using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace t5
{
    public partial class Form1 : Form
    {
        // UI fields and InitializeComponent are defined in Form1.Designer.cs (partial class)
        private List<SinhVien> students = new List<SinhVien>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // sample data
            students.Add(new SinhVien("SV000123", "Nguyễn Văn An", new DateTime(2006, 8, 15), "Nam", "an.nv@vju.ac.vn", "0912345678", 8.5m, "Kỹ thuật phần mềm 01", "Đang học"));
            students.Add(new SinhVien("SV000124", "Trần Minh Anh", new DateTime(2006, 1, 22), "Nữ", "anh.tm@vju.ac.vn", "0987654321", 9.0m, "Trí tuệ nhân tạo 01", "Đang học"));
            students.Add(new SinhVien("SV000125", "Lê Hoàng Bình", new DateTime(2006, 5, 9), "Nam", "binh.lh@vju.ac.vn", "0355556677", 7.4m, "Kỹ thuật phần mềm 01", "Đang học"));

            // defaults
            if (cboClass.Items.Count > 0) cboClass.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            if (cboFilterClass.Items.Count > 0) cboFilterClass.SelectedIndex = 0;

            RefreshGrid();
        }

        private void RefreshGrid(IEnumerable<SinhVien> source = null)
        {
            var list = source ?? students;
            dgvStudents.Rows.Clear();
            foreach (var s in list)
            {
                // first cell is Matched checkbox - default true when showing this list
                int idx = dgvStudents.Rows.Add(true, s.Id, s.FullName, s.Dob.ToString("dd/MM/yyyy"), s.Gender, s.Email, s.Phone, s.Score.ToString("0.0"), s.Lop, s.Status);
                dgvStudents.Rows[idx].Tag = s;
            }
            toolStripStatusLabel.Text = $"Tổng số: {dgvStudents.Rows.Count} sinh viên";
        }

        private void ClearInput()
        {
            txtStudentId.Text = "";
            txtFullName.Text = "";
            dtpDob.Value = DateTime.Today.AddYears(-18);
            rdoMale.Checked = true;
            txtEmail.Text = "";
            txtPhone.Text = "";
            if (cboClass.Items.Count > 0) cboClass.SelectedIndex = 0;
            nudScore.Value = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
        }

        private void PopulateInputs(SinhVien s)
        {
            if (s == null) return;
            txtStudentId.Text = s.Id;
            txtFullName.Text = s.FullName;
            dtpDob.Value = s.Dob;
            if (s.Gender == "Nữ") rdoFemale.Checked = true; else rdoMale.Checked = true;
            txtEmail.Text = s.Email;
            txtPhone.Text = s.Phone;
            cboClass.SelectedItem = s.Lop;
            nudScore.Value = Math.Max(nudScore.Minimum, Math.Min(nudScore.Maximum, s.Score));
            cboStatus.SelectedItem = s.Status;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Mã sinh viên và Họ và tên là bắt buộc.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var s = new SinhVien(
                txtStudentId.Text.Trim(),
                txtFullName.Text.Trim(),
                dtpDob.Value.Date,
                rdoFemale.Checked ? "Nữ" : "Nam",
                txtEmail.Text.Trim(),
                txtPhone.Text.Trim(),
                nudScore.Value,
                cboClass.SelectedItem?.ToString() ?? string.Empty,
                cboStatus.SelectedItem?.ToString() ?? string.Empty
            );

            students.Add(s);
            RefreshGrid();
            // select newly added
            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                if (row.Tag == s) { row.Selected = true; dgvStudents.CurrentCell = row.Cells[0]; break; }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) { MessageBox.Show("Chọn một sinh viên để sửa."); return; }
            var row = dgvStudents.SelectedRows[0];
            var s = row.Tag as SinhVien;
            if (s == null) return;

            s.Id = txtStudentId.Text.Trim();
            s.FullName = txtFullName.Text.Trim();
            s.Dob = dtpDob.Value.Date;
            s.Gender = rdoFemale.Checked ? "Nữ" : "Nam";
            s.Email = txtEmail.Text.Trim();
            s.Phone = txtPhone.Text.Trim();
            s.Score = nudScore.Value;
            s.Lop = cboClass.SelectedItem?.ToString() ?? string.Empty;
            s.Status = cboStatus.SelectedItem?.ToString() ?? string.Empty;

            RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) { MessageBox.Show("Chọn một sinh viên để xóa."); return; }
            var row = dgvStudents.SelectedRows[0];
            var s = row.Tag as SinhVien;
            if (s == null) return;

            var res = MessageBox.Show($"Xóa sinh viên {s.FullName} ({s.Id})?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                students.Remove(s);
                RefreshGrid();
                ClearInput();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgvStudents.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var kw = txtKeyword.Text?.Trim().ToLower() ?? string.Empty;
            var cls = cboFilterClass.SelectedItem?.ToString();
            var scoreFrom = nudFilterScoreFrom.Value;

            var filtered = students.Where(s =>
                (string.IsNullOrEmpty(kw) || s.Id.ToLower().Contains(kw) || s.FullName.ToLower().Contains(kw) || s.Email.ToLower().Contains(kw) || s.Phone.ToLower().Contains(kw))
                && (string.IsNullOrEmpty(cls) || cls == "Tất cả lớp" || s.Lop == cls)
                && s.Score >= scoreFrom
            );

            RefreshGrid(filtered);
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;
            var row = dgvStudents.SelectedRows[0];
            var s = row.Tag as SinhVien;
            if (s != null) PopulateInputs(s);
        }

        // keep these handlers because the designer wires them
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e) { }

        private void headerPanel_Paint(object sender, PaintEventArgs e)
        {
            // leave empty if you don't need custom painting
            // or add custom painting code here, e.g.:
            // using (var brush = new SolidBrush(headerPanel.BackColor)) { e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle); }
        }

        private void logoPictureBox_Click(object sender, EventArgs e) { }
        private void lblAppTitle_Click(object sender, EventArgs e) { }
        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private class Student
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public DateTime Dob { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public decimal Score { get; set; }
            public string Class { get; set; }
            public string Status { get; set; }

            public Student(string id, string fullName, DateTime dob, string gender, string email, string phone, decimal score, string cls, string status)
            {
                Id = id; FullName = fullName; Dob = dob; Gender = gender; Email = email; Phone = phone; Score = score; Class = cls; Status = status;
            }
        }
    }
}
