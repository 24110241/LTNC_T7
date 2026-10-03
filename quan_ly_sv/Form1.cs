using System;
using System.Linq;
using System.Windows.Forms;
using quan_ly_sv.Models;
using quan_ly_sv.Services;

namespace quan_ly_sv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            cboFilterLop.SelectedIndex = -1;
            nudFilterDiem.Value = 0;
            LoadSinhViensToGrid();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilter();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ApplyFilter()
        {
            var keyword = txtSearch.Text?.Trim();
            var lop = cboFilterLop.SelectedItem as LopHoc;
            double minDiem = (double)nudFilterDiem.Value;

            var items = DataStore.GetSinhViens().AsEnumerable();
            if (!string.IsNullOrEmpty(keyword))
            {
                items = items.Where(s => (s.MaSV ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || (s.HoVaTen ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || (s.Email ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                    || (s.DienThoai ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (lop != null)
            {
                items = items.Where(s => s.MaLop == lop.MaLop);
            }

            if (minDiem > 0)
            {
                items = items.Where(s => s.Diem >= minDiem);
            }

            var list = items.Select(s => new
            {
                s.MaSV,
                s.HoVaTen,
                NgaySinh = s.NgaySinh.ToShortDateString(),
                s.GioiTinh,
                s.Email,
                s.DienThoai,
                s.Diem,
                Lop = s.LopHoc?.TenLop,
                Nganh = s.LopHoc?.Nganh
            }).ToList();

            dgvStudents.DataSource = list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadLopHocToCombo();
            // Clear form first so no student appears selected on startup
            ClearAll();
            // Load grid without triggering SelectionChanged
            LoadSinhViensToGrid();
            // Ensure input controls are enabled for adding new students
            if (txtMaSV != null) { txtMaSV.Enabled = true; txtMaSV.ReadOnly = false; }
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            rbNam.Checked = true;
            txtMaSV.Focus();
        }

        private void LoadLopHocToCombo()
        {
            var lops = DataStore.GetLopHocs();
            cboLop.DataSource = lops.ToList();
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            // populate nganh combo with distinct values
            if (cboNganh != null)
            {
                var nganhs = lops.Select(l => l.Nganh).Where(n => !string.IsNullOrEmpty(n)).Distinct().ToList();
                cboNganh.DataSource = nganhs;
            }
            // handle selection changes to keep nganh in sync
            if (cboLop != null)
            {
                cboLop.SelectedIndexChanged -= CboLop_SelectedIndexChanged;
                cboLop.SelectedIndexChanged += CboLop_SelectedIndexChanged;
            }
        }

        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedItem is LopHoc lop && cboNganh != null)
            {
                if (!string.IsNullOrEmpty(lop.Nganh) && cboNganh.Items.Contains(lop.Nganh))
                    cboNganh.SelectedItem = lop.Nganh;
                else if (!string.IsNullOrEmpty(lop.Nganh))
                {
                    // add if missing
                    var list = cboNganh.Items.Cast<object>().ToList();
                    list.Add(lop.Nganh);
                    cboNganh.DataSource = list;
                    cboNganh.SelectedItem = lop.Nganh;
                }
            }
        }

        private void LoadSinhViensToGrid()
        {
            // Prevent SelectionChanged from firing while rebinding the grid
            try
            {
                dgvStudents.SelectionChanged -= dgvStudents_SelectionChanged;
            }
            catch { }

            var list = DataStore.GetSinhViens()
                .Select(s => new
                {
                    s.MaSV,
                    s.HoVaTen,
                    NgaySinh = s.NgaySinh.ToShortDateString(),
                    s.GioiTinh,
                    s.Email,
                    s.DienThoai,
                    s.Diem,
                    Lop = s.LopHoc?.TenLop,
                    Nganh = s.LopHoc?.Nganh
                })
                .ToList();
            dgvStudents.DataSource = list;
            // clear any selection so the form doesn't auto-fill
            try
            {
                dgvStudents.ClearSelection();
            }
            catch { }

            // reattach handler
            try
            {
                dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
            }
            catch { }
        }

        private void txtMaSV_Leave(object sender, EventArgs e)
        {
            var ma = txtMaSV.Text?.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                ClearFormExceptMa();
                btnAdd.Enabled = true;
                btnEdit.Enabled = btnDelete.Enabled = false;
                return;
            }

            var sv = DataStore.FindByMaSV(ma);
            if (sv != null)
            {
                FillForm(sv);
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                ClearFormExceptMa();
                btnAdd.Enabled = true;
                btnEdit.Enabled = btnDelete.Enabled = false;
            }
        }

        private void FillForm(SinhVien sv)
        {
            if (sv == null) return;
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoVaTen;
            dtpNgaySinh.Value = sv.NgaySinh == default ? DateTime.Now : sv.NgaySinh;
            if (!string.IsNullOrEmpty(sv.GioiTinh) && sv.GioiTinh.ToLower().StartsWith("n")) rbNu.Checked = true; else rbNam.Checked = true;
            txtEmail.Text = sv.Email;
            txtDienThoai.Text = sv.DienThoai;
            nudDiem.Value = (decimal)sv.Diem;
            if (sv.MaLop != null) cboLop.SelectedValue = sv.MaLop;
        }

        private void ClearFormExceptMa()
        {
            txtHoTen.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            rbNam.Checked = true;
            txtEmail.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            nudDiem.Value = 0;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
        }

        private void ClearAll()
        {
            txtMaSV.Text = string.Empty;
            ClearFormExceptMa();
            btnAdd.Enabled = true;
            btnEdit.Enabled = btnDelete.Enabled = false;
            txtMaSV.Focus();
        }

        private SinhVien GetSinhVienFromForm()
        {
            var sv = new SinhVien
            {
                MaSV = txtMaSV.Text?.Trim(),
                HoVaTen = txtHoTen.Text?.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = rbNu.Checked ? "Nu" : "Nam",
                Email = txtEmail.Text?.Trim(),
                DienThoai = txtDienThoai.Text?.Trim(),
                Diem = (double)nudDiem.Value,
                MaLop = cboLop.SelectedValue?.ToString()
            };
            return sv;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var sv = GetSinhVienFromForm();
            // ensure class is selected
            if (string.IsNullOrEmpty(sv.MaLop))
            {
                MessageBox.Show("Vui lòng chọn lớp học.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!sv.Validate(out var results))
            {
                MessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DataStore.FindByMaSV(sv.MaSV) != null)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataStore.AddSinhVien(sv);
            LoadSinhViensToGrid();
            ClearAll();
            MessageBox.Show("Thêm sinh viên thành công.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdd.Enabled = false;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var sv = GetSinhVienFromForm();
            if (!sv.Validate(out var results))
            {
                MessageBox.Show(string.Join("\n", results.Select(r => r.ErrorMessage)), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc muốn sửa sinh viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            DataStore.UpdateSinhVien(sv);
            LoadSinhViensToGrid();
            MessageBox.Show("Sửa sinh viên thành công.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // allow delete by selected row or by MaSV in textbox
            var ma = txtMaSV.Text?.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                if (dgvStudents.CurrentRow != null)
                {
                    ma = dgvStudents.CurrentRow.Cells["MaSV"].Value?.ToString();
                }
            }

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã sinh viên để xóa.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa sinh viên '{ma}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            DataStore.DeleteSinhVien(ma);
            LoadSinhViensToGrid();
            ClearAll();
            MessageBox.Show("Xóa sinh viên thành công.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // reset filters and form, reload grid
            try
            {
                txtSearch.Text = string.Empty;
                if (cboFilterLop != null) cboFilterLop.SelectedIndex = -1;
                if (cboNganh != null) cboNganh.SelectedIndex = -1;
                if (nudFilterDiem != null) nudFilterDiem.Value = 0;
                // reload grid first (this temporarily unhooks SelectionChanged)
                LoadSinhViensToGrid();
                // then clear the form to ensure no handler repopulates fields
                ClearAll();
                if (txtMaSV != null) { txtMaSV.Enabled = true; txtMaSV.ReadOnly = false; txtMaSV.Focus(); }
            }
            catch (Exception)
            {
                // fallback: clear form
                ClearAll();
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow == null) return;
            var ma = dgvStudents.CurrentRow.Cells["MaSV"].Value?.ToString();
            if (string.IsNullOrEmpty(ma)) return;

            var sv = DataStore.FindByMaSV(ma);
            if (sv != null)
            {
                FillForm(sv);
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

    }
}
