using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P185FindKth
{
    class Program
    {
        static readonly int CutCount = 15;

        static void Main(string[] args)
        {
            int size = 1000000;
            var A = GetNums(size);
            var copyA = (int[])A.Clone();

            var before = new DateTime();
            var after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(FindKth(A, size / 2));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            before = DateTime.Now;
            Array.Sort(copyA);
            Console.WriteLine(copyA[size / 2 - 1]);
            after = DateTime.Now;
            Console.WriteLine(after - before);
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

        static int FindKth(int[] A, int k)
        {
            return Find(A, k, 0, A.Count() - 1);
        }

        static void Swap(ref int v1, ref int v2)
        {
            var temp = v1;
            v1 = v2;
            v2 = temp;
        }

        static int Find(int[] A, int k, int left, int right)
        {
            if (left + CutCount < right)
            {
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
                if (k < i + 1)
                {
                    return Find(A, k, left, i - 1);
                }
                else if (k > i + 1)
                {
                    return Find(A, k, i + 1, right);
                }
                else
                {
                    return A[i];
                }
            }
            else
            {
                return FindNor(A, k, left, right);
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

        static int FindNor(int[] A, int k, int left, int right)
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
            return A[k - 1];
        }
    }
}
