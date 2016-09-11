using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P63T3._16DeleteDuplicateItem
{
    // 删除链表中的重复项
    class Program
    {
        static void Main(string[] args)
        {
            var list = GetNumbers(100000);
            var copyA = new LinkedList<int>(list);
            int[] copyB = list;

            DateTime startTime = DateTime.Now;
            Delete_n(copyA);
            DateTime endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);

            Console.WriteLine();

            startTime = DateTime.Now;
            var resultB = Delete_nlogn(copyB);
            endTime = DateTime.Now;
            Console.WriteLine(endTime - startTime);

            Console.WriteLine();
            Console.WriteLine(copyA.Count == resultB.Count);
        }

        static int[] GetNumbers(int size)
        {
            int[] A = new int[size];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                A[i] = random.Next(10000);
            }
            return A;
        }

        // 10W以上的数据规模占优
        static void Delete_n(LinkedList<int> list)
        {
            var Duplictae = new HashSet<int>();
            var p = list.First;
            var temp = p;
            while (p != null)
            {
                if (Duplictae.Contains(p.Value))
                {
                    temp = p;
                    p = p.Next;
                    list.Remove(temp);
                }
                else
                {
                    Duplictae.Add(p.Value);
                    p = p.Next;
                }
            }
        }

        // 10W以下的数据规模占优
        static LinkedList<int> Delete_nlogn(int[] A)
        {
            Array.Sort(A);
            var list = new LinkedList<int>(A);
            var p = list.First.Next;
            var temp = p;
            while (p != null)
            {
                if (p.Value == p.Previous.Value)
                {
                    temp = p;
                    p = p.Next;
                    list.Remove(temp);
                }
                else
                {
                    p = p.Next;
                }
            }
            return list;
        }
    }
}
