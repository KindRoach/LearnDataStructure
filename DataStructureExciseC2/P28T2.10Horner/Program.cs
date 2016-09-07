using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P28T2._10Horner
{
    // 霍纳法则（秦九韶算法）
    // Sum(A[i] * X^i):0~N
    class Program
    {
        const int N = 100000;

        static void Main(string[] args)
        {
            int[] numbers = GetNumbers(N+1);
            int x = new Random().Next();
            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(Horner_n(numbers, x, N));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();

            before = DateTime.Now;
            Console.WriteLine(Normal_n2(numbers, x, N));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static int[] GetNumbers(int size)
        {
            int[] numbers = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.Next();
            }
            return numbers;
        }

        static int Horner_n(int[] A, int x, int n)
        {
            int result = 0;
            for (int i = n; i >= 0; i--)
            {
                result = result * x + A[i];
            }
            return result;
        }

        static int Normal_n2(int[] A, int x, int n)
        {
            int result = 0;
            for (int i = 0; i < n + 1; i++)
            {
                int pow = 1;
                for (int j = 0; j < i; j++)
                {
                    pow = pow * x;
                }
                result = result + A[i] * pow;
            }
            return result;
        }
    }
}
