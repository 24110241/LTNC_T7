using System.Collections.Generic;
using System.Linq;
using quan_ly_sv.Models;

namespace quan_ly_sv.Services
{
    public static class DataStore
    {
        private static readonly List<LopHoc> _lops = new List<LopHoc>();
        private static readonly List<SinhVien> _svs = new List<SinhVien>();

        static DataStore()
        {
            // sample classes
            _lops.Add(new LopHoc("L01", "Kỹ thuật phần mềm 01", "Công nghệ thông tin"));
            _lops.Add(new LopHoc("L02", "Mạng máy tính 01", "Công nghệ thông tin"));

            // sample students
            _svs.Add(new SinhVien
            {
                MaSV = "SV0001",
                HoVaTen = "Nguyễn Văn A",
                NgaySinh = new System.DateTime(2003, 8, 15),
                GioiTinh = "Nam",
                Email = "an.nv@vnu.edu.vn",
                DienThoai = "0912345678",
                Diem = 8.5,
                MaLop = "L01",
                LopHoc = _lops.FirstOrDefault()
            });
        }

        public static IList<LopHoc> GetLopHocs() => _lops;

        public static IList<SinhVien> GetSinhViens() => _svs;

        public static SinhVien FindByMaSV(string ma)
        {
            return _svs.FirstOrDefault(s => s.MaSV == ma);
        }

        public static void AddSinhVien(SinhVien sv)
        {
            var lop = _lops.FirstOrDefault(l => l.MaLop == sv.MaLop);
            sv.LopHoc = lop;
            _svs.Add(sv);
        }

        public static void UpdateSinhVien(SinhVien sv)
        {
            var existing = FindByMaSV(sv.MaSV);
            if (existing == null) return;
            existing.HoVaTen = sv.HoVaTen;
            existing.NgaySinh = sv.NgaySinh;
            existing.GioiTinh = sv.GioiTinh;
            existing.Email = sv.Email;
            existing.DienThoai = sv.DienThoai;
            existing.Diem = sv.Diem;
            existing.MaLop = sv.MaLop;
            existing.LopHoc = _lops.FirstOrDefault(l => l.MaLop == sv.MaLop);
        }

        public static void DeleteSinhVien(string ma)
        {
            var existing = FindByMaSV(ma);
            if (existing != null) _svs.Remove(existing);
        }
    }
}
