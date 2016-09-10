using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P62T3._1PrintList
{
    // 顺序打印一个链表
    // 按照一个index链表打印链表中的对应项
    class Program
    {
        static void Main(string[] args)
        {
            var list = GetList();
            var indexes = GetIndexes();
            PrintLost(list, indexes);
        }

        static LinkedList<int> GetList()
        {
            var list = new LinkedList<int>();
            var p = list.AddFirst(1);
            for (int i = 2; i < 6; i++)
            {
                p = list.AddAfter(p, i);
            }

            return list;
        }

        static LinkedList<int> GetIndexes()
        {
            var list = new LinkedList<int>();
            var p = list.AddFirst(1);
            p = list.AddAfter(p, 3);
            p = list.AddAfter(p, 5);
            return list;
        }

        // 空链表没有输出，换行
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

        // 空链表不输出
        // 超出的index不输出
        // 非法index(小于1)，输出错误信息并继续
        static void PrintLost(LinkedList<int> L, LinkedList<int> P)
        {
            var pL = L.First;
            var pP = P.First;
            int index = 1;
            while (pL != null && pP != null)
            {
                if (index == pP.Value)
                {
                    Console.Write($"{pL.Value} ");
                    pP = pP.Next;
                }
                else if (index > pP.Value)
                {
                    Console.WriteLine("Wrong index.");
                    pP = pP.Next;
                }
                pL = pL.Next;
                index++;
            }

        }
    }
}
