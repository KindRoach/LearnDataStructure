using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P28T11AiEqualsI
{
    // 一个排好序的数组，确定是否存在A[i]=i;
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = GetNumbers();
            Console.WriteLine(FindIndex_n(numbers));
            Console.WriteLine(FindIndex_logn(numbers));
        }

        static int[] GetNumbers()
        {
            int[] numbers = new int[] { -19, -10, -5, 7, 9, 101 };
            return numbers;
        }

        static int FindIndex_n(int[] A)
        {
            for (int i = 0; i < A.Count(); i++)
            {
                if (i == A[i])
                {
                    return i;
                }
                else if (i < A[i])
                {
                    return -1;
                }
            }
            return -1;
        }

        static int FindIndex_logn(int[] A)
        {
            int left = 0;
            int right = A.Count() - 1;
            int mid;

            while (left <= right)
            {
                mid = left + (right - left) / 2;
                if (A[mid] < mid)
                {
                    left = mid + 1;
                }
                else if (A[mid] > mid)
                {
                    right = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return -1;

        }
    }
}
