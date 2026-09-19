using System;

namespace HinhHoc
{
    public interface Hinh
    {
        double GetDienTich();
        double GetChuVi();
        void Nhap();
        void HienThi();
    }

    public class HinhTron : Hinh
    {
        private double banKinh;

        public double BanKinh
        {
            get { return banKinh; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Bán kính phải là số dương (> 0)!");
                banKinh = value;
            }
        }

        public HinhTron() { banKinh = 1; }

        public HinhTron(double banKinh)
        {
            BanKinh = banKinh;
        }

        public double GetDienTich() { return Math.PI * banKinh * banKinh; }
        public double GetChuVi() { return 2 * Math.PI * banKinh; }

        public void Nhap()
        {
            Console.WriteLine("== Nhập hình tròn ==");
            do
            {
                try
                {
                    Console.Write("Nhập bán kính R: ");
                    BanKinh = Convert.ToDouble(Console.ReadLine());
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sai dữ liệu: " + ex.Message + " Nhập lại!");
                }
            } while (true);
        }

        public void HienThi()
        {
            Console.WriteLine("Hình tròn: R = " + banKinh
                + " | Diện tích = " + Math.Round(GetDienTich(), 2)
                + " | Chu vi = " + Math.Round(GetChuVi(), 2));
        }
    }

    public class HinhChuNhat : Hinh
    {
        private double chieuDai;
        private double chieuRong;

        public double ChieuDai
        {
            get { return chieuDai; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Chiều dài phải là số dương (> 0)!");
                chieuDai = value;
            }
        }

        public double ChieuRong
        {
            get { return chieuRong; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Chiều rộng phải là số dương (> 0)!");
                chieuRong = value;
            }
        }

        public HinhChuNhat() { chieuDai = 1; chieuRong = 1; }

        public HinhChuNhat(double chieuDai, double chieuRong)
        {
            ChieuDai = chieuDai;
            ChieuRong = chieuRong;
        }

        public double GetDienTich() { return chieuDai * chieuRong; }
        public double GetChuVi() { return 2 * (chieuDai + chieuRong); }

        public void Nhap()
        {
            Console.WriteLine("== Nhập hình chữ nhật ==");
            do
            {
                try
                {
                    Console.Write("Nhập chiều dài: ");
                    ChieuDai = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Nhập chiều rộng: ");
                    ChieuRong = Convert.ToDouble(Console.ReadLine());
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sai dữ liệu: " + ex.Message + " Nhập lại!");
                }
            } while (true);
        }

        public void HienThi()
        {
            Console.WriteLine("Hình chữ nhật: Dài = " + chieuDai + ", Rộng = " + chieuRong
                + " | Diện tích = " + Math.Round(GetDienTich(), 2)
                + " | Chu vi = " + Math.Round(GetChuVi(), 2));
        }
    }

    public class HinhTamGiac : Hinh
    {
        private double canhA;
        private double canhB;
        private double canhC;

        public double CanhA
        {
            get { return canhA; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cạnh A phải là số dương (> 0)!");
                canhA = value;
            }
        }

        public double CanhB
        {
            get { return canhB; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cạnh B phải là số dương (> 0)!");
                canhB = value;
            }
        }

        public double CanhC
        {
            get { return canhC; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cạnh C phải là số dương (> 0)!");
                canhC = value;
            }
        }

        public HinhTamGiac() { canhA = 3; canhB = 4; canhC = 5; }

        public HinhTamGiac(double canhA, double canhB, double canhC)
        {
            CanhA = canhA;
            CanhB = canhB;
            CanhC = canhC;
            if (!IsTamGiac())
                throw new ArgumentException("Ba cạnh không tạo thành một tam giác hợp lệ!");
        }

        public bool IsTamGiac()
        {
            return canhA > 0 && canhB > 0 && canhC > 0
                && canhA + canhB > canhC
                && canhA + canhC > canhB
                && canhB + canhC > canhA;
        }

        public double GetDienTich()
        {
            double p = GetChuVi() / 2;
            return Math.Sqrt(p * (p - canhA) * (p - canhB) * (p - canhC));
        }

        public double GetChuVi() { return canhA + canhB + canhC; }

        public void Nhap()
        {
            Console.WriteLine("== Nhập hình tam giác ==");
            do
            {
                try
                {
                    Console.Write("Nhập cạnh A: ");
                    CanhA = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Nhập cạnh B: ");
                    CanhB = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Nhập cạnh C: ");
                    CanhC = Convert.ToDouble(Console.ReadLine());
                    if (IsTamGiac())
                        return;
                    Console.WriteLine("Ba cạnh không tạo thành tam giác hợp lệ! Nhập lại!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sai dữ liệu: " + ex.Message + " Nhập lại!");
                }
            } while (true);
        }

        public void HienThi()
        {
            Console.WriteLine("Hình tam giác: A = " + canhA + ", B = " + canhB + ", C = " + canhC
                + " | Tam giác hợp lệ: " + IsTamGiac()
                + " | Diện tích = " + Math.Round(GetDienTich(), 2)
                + " | Chu vi = " + Math.Round(GetChuVi(), 2));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Hinh[] danhSach = new Hinh[3];
            danhSach[0] = new HinhTron();
            danhSach[1] = new HinhChuNhat();
            danhSach[2] = new HinhTamGiac();

            Console.WriteLine("=== CHƯƠNG TRÌNH QUẢN LÝ HÌNH ===");
            Console.WriteLine();

            foreach (Hinh h in danhSach)
                h.Nhap();

            Console.WriteLine();
            Console.WriteLine("=== DANH SÁCH CÁC HÌNH (Đa hình) ===");
            foreach (Hinh h in danhSach)
                h.HienThi();

            Console.WriteLine();
            Console.WriteLine("=== KIỂM TRA TAM GIÁC ===");
            do
            {
                try
                {
                    Console.Write("Nhập cạnh A: ");
                    double a = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Nhập cạnh B: ");
                    double b = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Nhập cạnh C: ");
                    double c = Convert.ToDouble(Console.ReadLine());

                    HinhTamGiac tg = new HinhTamGiac(a, b, c);
                    if (tg.IsTamGiac())
                    {
                        Console.WriteLine("Ba cạnh tạo thành tam giác hợp lệ.");
                        tg.HienThi();
                    }
                    else
                    {
                        Console.WriteLine("Ba cạnh KHÔNG tạo thành tam giác.");
                    }
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sai dữ liệu: " + ex.Message + " Nhập lại!");
                }
            } while (true);
        }
    }
}