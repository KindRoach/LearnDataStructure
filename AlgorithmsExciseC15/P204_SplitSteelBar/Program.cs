using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P204_SplitSteelBar
{
    class Program
    {
        static void Main(string[] args)
        {
            var price = new int[] { 0, 1, 5, 8, 9, 10, 15, 17, 20, 24, 30 };
            var ans = MaxInc(price, 6);
            Console.WriteLine(ans[0]);
            ans.RemoveAt(0);
            foreach (var item in ans)
            {
                Console.Write($"{item} ");
            }
        }

        // 第一格int为最大收益，之后的int为分个方案
        static List<int> MaxInc(int[] price, int n)
        {
            var dp = new int[n + 1];
            var path = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                int max = int.MinValue;
                for (int j = 1; j <= i; j++)
                {
                    if (max < (price[j] + dp[i - j]))
                    {
                        max = price[j] + dp[i - j];
                        path[i] = j;
                    }
                }
                dp[i] = max;
            }

            var ans = new List<int>() { dp[n] };
            Helper(ans, path, n);
            return ans;
        }

        private static void Helper(List<int> ans, int[] path, int n)
        {
            if (n == path[n])
            {
                ans.Add(n);
                return;
            }

            Helper(ans, path, path[n]);
            Helper(ans, path, n - path[n]);
        }
    }
}
