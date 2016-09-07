using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P17MaxSumOfSubstring
{
    class Program
    {
        // 找出n个数组成的序列中，和最大的子序列的和（P13）
        static void Main(string[] args)
        {
            int[] numbers = GetNumbers(10000000);
            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(GetMaxSum_nlogn(numbers, 0, numbers.Count() - 1));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();

            before = DateTime.Now;
            Console.WriteLine(GetMaxSum_n(numbers));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static int[] GetNumbers(int size)
        {
            int[] numbers = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.Next(-1000, 1000);
            }
            return numbers;
        }

        static int GetMaxSum_nlogn(int[] numbers, int left, int right)
        {
            if (left == right)
            {
                if (numbers[left] > 0)
                {
                    return numbers[left];
                }
                else
                {
                    return 0;
                }
            }

            int mid = left + (right - left) / 2;
            int maxSumLeft = GetMaxSum_nlogn(numbers, left, mid);
            int maxSumRight = GetMaxSum_nlogn(numbers, mid + 1, right);

            int maxBoderSumLeft = 0;
            int sumLeft = 0;
            for (int i = mid; i >= left; i--)
            {
                sumLeft = sumLeft + numbers[i];
                maxBoderSumLeft = Math.Max(sumLeft, maxBoderSumLeft);
            }

            int maxBoderSumRight = 0;
            int sumRight = 0;
            for (int i = mid + 1; i <= right; i++)
            {
                sumRight = sumRight + numbers[i];
                maxBoderSumRight = Math.Max(sumRight, maxBoderSumRight);
            }

            int result = Math.Max(maxSumLeft, maxSumRight);
            result = Math.Max(result, maxBoderSumLeft + maxBoderSumRight);
            return result;
        }

        static int GetMaxSum_n(int[] numbers)
        {
            int num = numbers.Count();
            int thisSum = 0;
            int maxSum = thisSum;
            for (int i = 0; i < num; i++)
            {
                thisSum = thisSum + numbers[i];
                if (thisSum > maxSum)
                {
                    maxSum = thisSum;
                }
                else if (thisSum < 0)
                {
                    thisSum = 0;
                }
            }
            return maxSum;
        }
    }
}
