using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P211_Matrix_chainMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MinMulti(new int[] { 30, 35, 15, 5, 10, 20, 25 }));
        }

        static int MinMulti(int[] P)
        {
            int n = P.Count();
            var m = new int[n, n];
            var s = new int[n, n];
            n--;
            for (int l = 2; l <= n; l++)
                for (int i = 1; i <= n - l + 1; i++)
                {
                    int j = i + l - 1;
                    m[i, j] = int.MaxValue;
                    for (int k = i; k <= j - 1; k++)
                    {
                        int sum = m[i, k] + m[k + 1, j] + P[i - 1] * P[k] * P[j];
                        if (m[i, j] > sum)
                        {
                            m[i, j] = sum;
                            s[i, j] = k;
                        }
                    }
                }
            Print(s, 1, n);
            return m[1, n];
        }

        static void Print(int[,] S, int i, int j)
        {
            if (i == j)
            {
                Console.Write($"A{i}");
                return;
            }
            Console.Write("(");
            Print(S, i, S[i, j]);
            Print(S, S[i, j] + 1, j);
            Console.Write(")");
            
        }
    }
}
