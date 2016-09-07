using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9T1._1SelectionProblem
{
    // 已知n个数，找出第k大（k=n/2）

    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = GetNumbers(100000);
            int[] copyA = (int[])numbers.Clone();
            int[] copyB = (int[])numbers.Clone();
            DateTime startTime = DateTime.Now;
            Console.WriteLine(GetNumerKwithQsort(copyA));
            DateTime endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);

            Console.WriteLine();

            startTime = DateTime.Now;
            Console.WriteLine(GetNumberKwithHalfSort(copyB));
            endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);
        }

        private static int[] GetNumbers(int num)
        {
            int[] numbers = new int[num];
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                numbers[i] = random.Next();
            }
            return numbers;
        }

        public static int GetNumerKwithQsort(int[] numbers)
        {
            int k = numbers.Count() / 2;
            Array.Sort(numbers);
            return numbers[k - 1];
        }

        public static int GetNumberKwithHalfSort(int[] numbers)
        {

            int k = numbers.Count() / 2;
            int[] sortedNumber = new int[k];
            Array.Copy(numbers, 0, sortedNumber, 0, k);
            Array.Sort(sortedNumber);
            for (int i = k; i < numbers.Count(); i++)
            {
                if (numbers[i] >= sortedNumber[k - 1])
                {
                    continue;
                }
                for (int j = 0; j < k; j++)
                {
                    if (sortedNumber[j] < numbers[i])
                    {
                        continue;
                    }
                    else
                    {
                        for (int h = k - 1; h > j; h--)
                        {
                            sortedNumber[h] = sortedNumber[h - 1];
                        }
                        sortedNumber[j] = numbers[i];
                        break;
                    }
                }
            }
            return sortedNumber[k - 1];
        }

    }
}
