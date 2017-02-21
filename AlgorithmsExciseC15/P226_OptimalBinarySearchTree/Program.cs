using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P226_OptimalBinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new double[] { double.NaN, 0.15, 0.10, 0.05, 0.10, 0.20 };
            var q = new double[] { 0.05, 0.10, 0.05, 0.05, 0.05, 0.10 };
            Console.WriteLine(OBST(p, q));
        }

        static double OBST(double[] p, double[] q)
        {
            int n = p.Count();
            var w = new double[n + 1, n + 1];
            var e = new double[n + 1, n + 1];
            var s = new int[n + 1, n + 1];
            n--;
            for (int i = 1; i <= n + 1; i++)
            {
                e[i, i - 1] = q[i - 1];
                w[i, i - 1] = q[i - 1];
            }
            for (int l = 1; l <= n; l++)
            {
                for (int i = 1; i <= n - l + 1; i++)
                {
                    int j = i + l - 1;
                    w[i, j] = w[i, j - 1] + p[j] + q[j];
                    e[i, j] = double.MaxValue;
                    for (int k = i; k <= j; k++)
                    {
                        double sum = e[i, k - 1] + e[k + 1, j] + w[i, j];
                        if (sum < e[i, j])
                        {
                            e[i, j] = sum;
                            s[i, j] = k;
                        }
                    }
                }
            }
            return e[1, n];
        }
    }
}
