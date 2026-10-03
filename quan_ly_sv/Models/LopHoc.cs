using System.ComponentModel.DataAnnotations;

namespace quan_ly_sv.Models
{
    public class LopHoc
    {
        public LopHoc() { }

        public LopHoc(string maLop, string tenLop, string nganh = null)
        {
            MaLop = maLop;
            TenLop = tenLop;
            Nganh = nganh;
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string MaLop { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLop { get; set; }

        [StringLength(100)]
        public string Nganh { get; set; }

        public override string ToString() => TenLop;
    }
}
