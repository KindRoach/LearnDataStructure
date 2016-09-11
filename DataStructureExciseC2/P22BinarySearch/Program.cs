using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P22BinarySearch
{
    class Program
    {
        // 二分查找
        static void Main(string[] args)
        {
            int target = Int32.MaxValue;
            int[] numbers = GetNumbers(100000000, out target);
            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(FindIndexOf_n(target, numbers));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();

            before = DateTime.Now;
            Console.WriteLine(FindIndexOf_logn_recursion(target, numbers, 0, numbers.Count() - 1));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            Console.WriteLine();


            before = DateTime.Now;
            Console.WriteLine(FindIndexOf_logn(target, numbers));
            after = DateTime.Now;
            Console.WriteLine(after - before);
        }

        static int[] GetNumbers(int size, out int target)
        {
            int[] numbers = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers[i] = random.Next(Int32.MaxValue - 1);
            }
            target = random.Next();
            numbers[random.Next(size)] = target;
            Array.Sort(numbers);
            return numbers;
        }

        static int FindIndexOf_logn(int target, int[] numbers)
        {
            int left = 0;
            int right = numbers.Count() - 1;
            int mid = 0;
            while (left <= right)
            {
                mid = left + (right - left) / 2;
                if (target < numbers[mid])
                {
                    right = mid - 1;
                }
                else if (target > numbers[mid])
                {
                    left = mid + 1;
                }
                else
                {
                    return mid;
                }
            }
            return -1;
        }

        static int FindIndexOf_n(int target, int[] numbers)
        {
            for (int i = 0; i < numbers.Count(); i++)
            {
                if (target == numbers[i])
                {
                    return i;
                }
            }
            return -1;
        }

        static int FindIndexOf_logn_recursion(int target, int[] numbers, int left, int right)
        {
            if (left > right)
            {
                return -1;
            }

            int mid = left + (right - left) / 2;
            if (target < numbers[mid])
            {
                return FindIndexOf_logn_recursion(target, numbers, left, mid - 1);
            }
            else if (target > numbers[mid])
            {
                return FindIndexOf_logn_recursion(target, numbers, mid + 1, right);
            }
            else
            {
                return mid;
            }
        }
    }
}
