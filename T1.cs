using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b = 0;
            int c;

            Console.Write("Nhập a:");
            a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nhập b:");
            bool ok = false;
            while (!ok)
            {
                try
                {
                    b = int.Parse(Console.ReadLine());
                    ok = true;
                }
                catch (Exception)
                {
                    Console.Write("Nhập sai, vui lòng nhập lại b:");
                }
            }

            while (true)
            {
                Console.Write("Nhập c:");
                if (int.TryParse(Console.ReadLine(), out c))
                    break;
                else
                    Console.WriteLine("Nhập sai, vui lòng nhập lại:");
            }

            Console.WriteLine(a + " + " + b + " = " + (a + b));
            Console.WriteLine(string.Format("{0} + {1} + {2} = {3}", a, b, c, a + b + c));
        }
    }
}