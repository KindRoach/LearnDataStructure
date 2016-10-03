using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P165InsertSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var A = GetNums(10000);
            InsertSort(A);
            foreach (var item in A)
            {
                Console.Write(item);
                Console.Write(" ");
            }
        }

        static int[] GetNums(int size)
        {
            var nums = new int[size];
            for (int i = 0; i < size; i++)
            {
                nums[i] = i;
            }
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                Swap(ref nums[i], ref nums[random.Next(i + 1)]);
            }
            return nums;
        }

        static void Swap(ref int v1, ref int v2)
        {
            var temp = v1;
            v1 = v2;
            v2 = temp;
        }

        static void InsertSort(int[] A)
        {
            int n = A.Count();
            int temp, j;
            for (int i = 0; i < n; i++)
            {
                temp = A[i];
                for (j = i; j > 0 && A[j - 1] > temp; j--)
                {
                    A[j] = A[j - 1];
                }
                A[j] = temp;
            }
        }
    }
}
