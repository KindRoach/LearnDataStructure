using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P177QuickSort
{
    class Program
    {
        static readonly int CutCount = 15;

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var A = GetNums(100000);
                var copyA = (int[])A.Clone();
                var copyB = (int[])A.Clone();
                var before = new DateTime();
                var after = new DateTime();

                before = DateTime.Now;
                QuickSort(A);
                after = DateTime.Now;
                var mySort = (after - before).TotalMilliseconds;

                before = DateTime.Now;
                Array.Sort(copyA);
                after = DateTime.Now;
                var arraySort = (after - before).TotalMilliseconds;

                before = DateTime.Now;
                copyB = copyB.OrderBy(x => x).ToArray();
                after = DateTime.Now;
                var orderBy = (after - before).TotalMilliseconds;

                Console.WriteLine($"my qsort: {mySort}-----{(double)mySort / arraySort}");
                Console.WriteLine($"Array.Sort: {arraySort}-----{(double)arraySort / arraySort}");
                Console.WriteLine($"OrderBy: { orderBy}-----{(double)orderBy / arraySort}");
                Console.WriteLine();
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

        static void QuickSort(int[] A)
        {
            int n = A.Count();
            Qsort(A, 0, n - 1);
        }

        static void Qsort(int[] A, int left, int right)
        {
            if (left + CutCount < right)
            {
                // 该部分无法在right-left<2时运行
                int pivot = GetMid(A, left, right);
                int i = left + 1, j = right - 2;
                while (true)
                {
                    while (A[i] < pivot) i++;
                    while (A[j] > pivot) j--;
                    if (i < j)
                    {
                        Swap(ref A[i], ref A[j]);
                        i++;
                        j--;
                    }
                    else break;
                }
                Swap(ref A[i], ref A[right - 1]);
                Qsort(A, left, i - 1);
                Qsort(A, i + 1, right);
            }
            else
            {
                InsertSort(A, left, right);
            }
        }

        static int GetMid(int[] A, int left, int right)
        {
            int mid = left + (right - left) / 2;

            // make sure A[left]<=A[mid]<=A[right]
            if (A[left] > A[mid])
                Swap(ref A[left], ref A[mid]);
            if (A[mid] > A[right])
                Swap(ref A[mid], ref A[right]);
            if (A[left] > A[mid])
                Swap(ref A[left], ref A[mid]);

            Swap(ref A[mid], ref A[right - 1]);
            return A[right - 1];
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
    }
}
