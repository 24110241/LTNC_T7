using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace quan_ly_sv.Models
{
    public class SinhVien
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(200)]
        public string HoVaTen { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        [RegularExpression("Nam|Nu|Nam|Nữ|\u00D0|Female|Male|male|female", ErrorMessage = "Gioi tinh must be 'Nam' or 'Nu'")]
        public string GioiTinh { get; set; } = "Nam";

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string DienThoai { get; set; }

        [Range(0, 10)]
        public double Diem { get; set; }

        [Required]
        public string MaLop { get; set; }

        // optional navigation
        public LopHoc LopHoc { get; set; }

        public SinhVien()
        {
            NgaySinh = DateTime.Now;
        }

        public bool Validate(out ICollection<ValidationResult> results)
        {
            var ctx = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, ctx, results, true);
        }
    }
}
