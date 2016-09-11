using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P62T3._4ListAndOr
{
    // 对于两个排好序的链表，进行与操作和或操作
    class Program
    {
        static void Main(string[] args)
        {
            var L1 = GetList(1, 6);
            var L2 = GetList(4, 8);
            var Land = ListAnd(L1, L2);
            PrintList(Land);

            var Lor = ListOr(L1, L2);
            PrintList(Lor);
        }

        static LinkedList<int> GetList(int start, int end)
        {
            var list = new LinkedList<int>();
            for (int i = start; i < end; i++)
            {
                list.AddLast(i);
            }
            return list;
        }

        static LinkedList<int> ListAnd(LinkedList<int> L1, LinkedList<int> L2)
        {
            var result = new LinkedList<int>();

            var p1 = L1.First;
            var p2 = L2.First;
            while (p1 != null && p2 != null)
            {
                if (p1.Value < p2.Value)
                {
                    p1 = p1.Next;
                }
                else if (p1.Value > p2.Value)
                {
                    p2 = p2.Next;
                }
                else
                {
                    result.AddLast(p1.Value);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            return result;
        }

        static LinkedList<int> ListOr(LinkedList<int> L1, LinkedList<int> L2)
        {
            var result = new LinkedList<int>();

            var p1 = L1.First;
            var p2 = L2.First;
            while (p1 != null && p2 != null)
            {
                if (p1.Value < p2.Value)
                {
                    result.AddLast(p1.Value);
                    p1 = p1.Next;
                }
                else if (p1.Value > p2.Value)
                {
                    result.AddLast(p2.Value);
                    p2 = p2.Next;
                }
                else
                {
                    result.AddLast(p1.Value);
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }


            while (p1 != null)
            {
                result.AddLast(p1.Value);
                p1 = p1.Next;
            }
            while (p2 != null)
            {
                result.AddLast(p2.Value);
                p2 = p2.Next;
            }

            return result;
        }

        static void PrintList(LinkedList<int> list)
        {
            var p = list.First;
            while (p != null)
            {
                Console.Write($"{p.Value} ");
                p = p.Next;
            }
            Console.WriteLine();
        }

    }
}
