using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P223_LongestCommonSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LCA("ABCCB", "ACBGB"));
        }

        static int LCA(string s1, string s2)
        {
            int m = s1.Length, n = s2.Length;
            var dp = new int[m + 1, n + 1];
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            Print(dp, s1, s2);
            return dp[m, n];
        }

        private static void Print(int[,] dp, string s1, string s2)
        {
            int i = s1.Length, j = s2.Length;
            var ans = new char[dp[i, j]];
            int index = dp[i, j];
            while (i > 0 && j > 0)
            {
                if (s1[i - 1] == s2[j - 1])
                {
                    ans[--index] = s1[i - 1];
                    i--; j--;
                }
                else
                {
                    if (dp[i - 1, j] > dp[i, j - 1]) i--;
                    else j--;
                }
            }
            Console.WriteLine(new string(ans));
        }
    }
}
