using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P24QuickPow
{
    // 快速幂
    class Program
    {
        const int mod = 10007;

        static void Main(string[] args)
        {
            Random random = new Random();
            int x = random.Next(mod);
            int exp = random.Next(mod);

            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(NormalPow(x, exp));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();

            before = DateTime.Now;
            Console.WriteLine(QuickPow(x, exp));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static int NormalPow(int x, int exp)
        {
            int result = 1;
            for (int i = 0; i < exp; i++)
            {
                result = (result * x) % mod;
            }
            return result;
        }

        static int QuickPow(int x, int exp)
        {
            if (exp == 0)
            {
                return 1;
            }
            else if (exp == 1)
            {
                return x % mod;
            }
            else
            {
                if ((exp % 2) == 0)
                {
                    return QuickPow((x * x) % mod, exp / 2) % mod;
                }
                else
                {
                    return (QuickPow((x * x) % mod, (exp - 1) / 2) * x) % mod;
                }
            }
        }
    }
}
