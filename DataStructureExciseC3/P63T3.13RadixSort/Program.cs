using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P63T3._13RadixSort
{
    // 基数排序
    class Program
    {
        const int SortTimes = 3;
        const int BarrelNum = 1000; // 3 digit for every sortting.

        static void Main(string[] args)
        {
            int[] numbers = GetNumbers(10000000);
            int[] copyA = (int[])numbers.Clone();
            int[] copyB = (int[])numbers.Clone();

            DateTime startTime = DateTime.Now;
            RadixSort(copyA);
            DateTime endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);

            Console.WriteLine();

            startTime = DateTime.Now;
            QuickSort(copyB);
            endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);

            Console.WriteLine();

            for (int i = 0; i < numbers.Count(); i++)
            {
                if (copyA[i] != copyB[i])
                {
                    Console.WriteLine("Wrong");
                    break;
                }
            }
        }

        static int[] GetNumbers(int size)
        {
            int[] A = new int[size];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                A[i] = random.Next(999999999);
            }
            return A;
        }

        static void RadixSort(int[] A)
        {
            var Barrels = new LinkedList<int>[BarrelNum];
            for (int i = 0; i < BarrelNum; i++)
            {
                Barrels[i] = new LinkedList<int>();
            }
            int mode = 1;
            for (int i = 0; i < SortTimes; i++)
            {
                foreach (var item in Barrels)
                {
                    item.Clear();
                }
                mode = mode * BarrelNum;

                for (int j = 0; j < A.Count(); j++)
                {
                    int digits = A[j] % mode / (mode / BarrelNum);
                    Barrels[digits].AddLast(A[j]);
                }

                int indexe = -1;
                for (int j = 0; j < BarrelNum; j++)
                {
                    var p = Barrels[j].First;
                    while (p != null)
                    {
                        indexe++;
                        A[indexe] = p.Value;
                        p = p.Next;
                    }
                }
            }
        }

        static void QuickSort(int[] A)
        {
            Array.Sort(A);
        }
    }
}
