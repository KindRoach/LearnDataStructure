using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P28T2._19FiindMainElement
{
    // N个数的数组中，找到主要元素（出现次数>N/2）
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = GetNumbers(1000000);
            DateTime before = new DateTime();
            DateTime after = new DateTime();

            before = DateTime.Now;
            bool found = false;
            int mainElement = FindMainElement(A, out found);
            if (found)
            {
                Console.WriteLine(mainElement);
            }
            else
            {
                Console.WriteLine("Not Found");
            }
            after = DateTime.Now;
            Console.WriteLine(after - before);

        }

        static int[] GetNumbers(int size)
        {
            int[] numbers = new int[size];
            Random random = new Random();
            for (int i = 0; i < size / 2 - 100; i++)
            {
                numbers[i] = random.Next();
            }
            for (int i = size / 2 - 100; i < size; i++)
            {
                numbers[i] = 1996;
            }
            return numbers;
            //return new int[] { 3, 3, 1, 2, 6, 4, 2, 4, 4 };
        }

        // nlogn水平
        static int FindMainElement(int[] A, out bool found)
        {
            int n = A.Count();
            var dic = new Dictionary<int, int>();
            for (int i = 0; i < A.Count(); i++)
            {
                if (dic.ContainsKey(A[i]))
                {
                    dic[A[i]]++;
                }
                else
                {
                    dic.Add(A[i], 1);
                }
            }
            foreach (var pair in dic)
            {
                if (pair.Value > (n / 2))
                {
                    found = true;
                    return pair.Key;
                }
            }
            found = false;
            return -1;
        }
    }
}
