using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P28T2._12SubstringProblem
{
    // a.求最小子序列和。
    // b.求最小的正子序列和。
    // c.求最大子序列乘积。
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = GetNumbers(10000000);
            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(GetMaxProduct_n(A));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();

            before = DateTime.Now;
            Console.WriteLine(GetMaxProduct_n2(A));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static int[] GetNumbers(int size)
        {
            //int[] numbers = new int[size];
            //Random random = new Random();
            //for (int i = 0; i < size; i++)
            //{
            //    numbers[i] = random.Next(-1000000000, 1000000000);
            //}
            //return numbers;
            return new int[] { 8, 3, -1, 9, -7 };
        }

        static int GetMinSum_n(int[] A)
        {
            int thisSum = 0;
            int minSum = Int32.MaxValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisSum = thisSum + A[i];
                minSum = Math.Min(minSum, thisSum);
                if (thisSum > 0)
                {
                    thisSum = 0;
                }
            }
            return minSum;
        }

        static int GetMinSum_n2(int[] A)
        {
            int[] sum = new int[A.Count()];
            int thisSum = 0;
            int minSum = int.MaxValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisSum = thisSum + A[i];
                sum[i] = thisSum;
                minSum = Math.Min(minSum, thisSum);
            }
            for (int i = 0; i < A.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    thisSum = sum[i] - sum[j];
                    minSum = Math.Min(minSum, thisSum);
                }
            }
            return minSum;
        }

        static int GetMinPlusSum_n2(int[] A)
        {
            int[] sum = new int[A.Count()];
            int thisSum = 0;
            int minSum = int.MaxValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisSum = thisSum + A[i];
                sum[i] = thisSum;
                if (thisSum > 0) minSum = Math.Min(minSum, thisSum);
            }

            for (int i = 0; i < A.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    thisSum = sum[i] - sum[j];
                    if (thisSum > 0)
                    {
                        minSum = Math.Min(minSum, thisSum);
                    }
                }
            }
            return minSum;
        }


        struct SumPair : IComparable<SumPair>
        {
            public int Sum;
            public int Index;

            public int CompareTo(SumPair other)
            {
                return this.Sum.CompareTo(other.Sum);
            }
        }
        static int GetMinPlusSum_nlogn_List(int[] A)
        {
            var sumByIndex = new List<SumPair>(A.Count());
            int thisSum = 0;
            int minSum = int.MaxValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisSum = thisSum + A[i];
                if (thisSum > 0) minSum = Math.Min(minSum, thisSum);
                var sumPair = new SumPair();
                sumPair.Sum = thisSum;
                sumPair.Index = i;
                sumByIndex.Add(sumPair);
            }
            sumByIndex.Sort();
            for (int i = 1; i < sumByIndex.Count(); i++)
            {
                if (sumByIndex[i].Index > sumByIndex[i - 1].Index
                    &&
                   (sumByIndex[i].Sum - sumByIndex[i - 1].Sum) > 0)
                {
                    minSum = Math.Min(minSum, sumByIndex[i].Sum - sumByIndex[i - 1].Sum);
                }
            }
            return minSum;
        }

        // 数组比List还要慢一点（10%以内）
        static int GetMinPlusSum_nlogn_Struct(int[] A)
        {
            SumPair[] sumByIndex = new SumPair[A.Count()];
            int thisSum = 0;
            int minSum = int.MaxValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisSum = thisSum + A[i];
                if (thisSum > 0) minSum = Math.Min(minSum, thisSum);
                var sumPair = new SumPair();
                sumPair.Sum = thisSum;
                sumPair.Index = i;
                sumByIndex[i] = sumPair;
            }
            Array.Sort(sumByIndex);
            for (int i = 1; i < sumByIndex.Count(); i++)
            {
                if (sumByIndex[i].Index > sumByIndex[i - 1].Index
                    &&
                   (sumByIndex[i].Sum - sumByIndex[i - 1].Sum) > 0)
                {
                    minSum = Math.Min(minSum, sumByIndex[i].Sum - sumByIndex[i - 1].Sum);
                }
            }
            return minSum;
        }

        static int GetMaxProduct_n2(int[] A)
        {
            int[] product = new int[A.Count()];
            int thisProduct = 1;
            int maxProduct = int.MinValue;
            for (int i = 0; i < A.Count(); i++)
            {
                thisProduct = thisProduct * A[i];
                product[i] = thisProduct;
                maxProduct = Math.Max(maxProduct, thisProduct);
            }

            for (int i = 0; i < A.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    thisProduct = product[i] / product[j];
                    maxProduct = Math.Max(maxProduct, thisProduct);
                }
            }
            return maxProduct;
        }


        // 顺道求了最小积
        static int GetMaxProduct_n(int[] A)
        {
            int maxProduct = int.MinValue;
            int minProduct = int.MaxValue;
            int plusProduct = 1;
            int minusProduct = 1;
            for (int i = 0; i < A.Count(); i++)
            {
                plusProduct = plusProduct * A[i];
                minusProduct = minusProduct * A[i];
                if (plusProduct < minusProduct)
                {
                    int temp = plusProduct;
                    plusProduct = minusProduct;
                    minusProduct = temp;
                }
                if (plusProduct < 1)
                {
                    plusProduct = 1;
                }
                maxProduct = Math.Max(maxProduct, plusProduct);
                minProduct = Math.Min(minProduct, minusProduct);
            }
            return maxProduct;
        }
    }
}
