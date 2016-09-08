using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P28T2._13CheckPrime
{
    // 判断N是否为素数
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            long x = random.Next((int)Math.Pow(2, 29), (int)Math.Pow(2, 30));
            DateTime before, after;
            before = DateTime.Now;
            Console.WriteLine(IsPrime(x));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static bool IsPrime(long x)
        {
            long factor = 2;
            long sqrt = Convert.ToInt32(Math.Sqrt(x)) + 1;
            while (factor < sqrt)
            {
                if (x % factor == 0)
                {
                    return false;
                }
                factor++;
            }
            return true;
        }
    }
}
