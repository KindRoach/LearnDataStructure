using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P173MargeSort
{
    class Program
    {
        // 小数组N
        static readonly int CutCount = 15;

        static void Main(string[] args)
        {
            var A = GetNums(10000);
            Msort(A);
            foreach (var item in A)
            {
                Console.Write($"{item} ");
            }
        }

        static int[] GetNums(int size)
        {
            var random = new Random();
            var nums = new int[size];

            //for (int i = 0; i < size; i++)
            //{
            //    nums[i] = random.Next(size / 100);
            //}

            for (int i = 0; i < size; i++)
            {
                nums[i] = i;
            }
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

        static void MargeSort(int[] A)
        {
            int n = A.Count();

            // 确保整个排序只是用一个O(N)的临时空间
            var tempArray = new int[n];
            Msort(A, 0, n - 1, tempArray);
        }

        // 非递归版
        static void Msort(int[] A)
        {
            int n = A.Count();
            for (int i = 0; i < n; i = i + CutCount)
            {
                InsertSort(A, i, Math.Min(n - 1, i + CutCount - 1));
            }
            int[] tempArray = new int[n];
            for (int subLen = CutCount; subLen < n; subLen *= 2)
            {
                int startA = 0;
                while (startA + subLen < n)
                {
                    int endA = startA + subLen - 1;
                    int endB = Math.Min(n - 1, endA + subLen);
                    Marge(A, startA, endA, endB, tempArray);
                    startA = endB + 1;
                }
            }
        }

        static void Msort(int[] A, int left, int right, int[] tempArray)
        {
            if (left + CutCount < right)
            {
                int mid = left + (right - left) / 2;
                Msort(A, left, mid, tempArray);
                Msort(A, mid + 1, right, tempArray);
                Marge(A, left, mid, right, tempArray);
            }
            else
            {
                InsertSort(A, left, right);
            }
        }

        private static void InsertSort(int[] A, int left, int right)
        {
            int temp, j;
            for (int i = left; i < right + 1; i++)
            {
                temp = A[i];
                for (j = i; j > left && A[j - 1] > temp; j--)
                {
                    A[j] = A[j - 1];
                }
                A[j] = temp;
            }
        }

        static void Marge(int[] A, int startA, int endA, int endB, int[] tempArray)
        {
            int indexA = startA;
            int indexB = endA + 1;
            int indexT = 0;
            while (indexA <= endA && indexB <= endB)
            {
                if (A[indexA] <= A[indexB])
                {
                    tempArray[indexT] = A[indexA];
                    indexA++;
                }
                else
                {
                    tempArray[indexT] = A[indexB];
                    indexB++;
                }
                indexT++;
            }
            while (indexA <= endA)
            {
                tempArray[indexT] = A[indexA];
                indexA++;
                indexT++;
            }
            while (indexB <= endB)
            {
                tempArray[indexT] = A[indexB];
                indexB++;
                indexT++;
            }
            for (int i = 0; i < endB - startA + 1; i++)
            {
                A[startA + i] = tempArray[i];
            }
        }
    }
}
