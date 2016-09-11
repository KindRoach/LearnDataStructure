using P62T3._3SwapListNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P63T3._12ReverseList
{
    // 使用常数空间，O(n)时间，反转链表
    class Program
    {
        static void Main(string[] args)
        {
            var L = GetList();
            ReverseList(L);
            L.Print();
        }

        static MyList<int> GetList()
        {
            MyList<int> list = new MyList<int>();
            var p = list.Head;
            for (int i = 0; i < 5; i++)
            {
                p = list.InsertAfter(p, i + 1);
            }
            return list;
        }

        static void ReverseList(MyList<int> L)
        {
            if (L.Count <= 1)
            {
                return;
            }

            if (L.Count == 2)
            {
                var p1 = L.Head.Next;
                var p2 = p1.Next;
                p2.Next = p1;
                p1.Next = null;
                L.Head.Next = p2;
                return;
            }

            var p = L.Head.Next; //Previous
            var n = p.Next;      //Node
            var a = n.Next;      //Advance

            while (a != null)
            {
                n.Next = p;
                p = n;
                n = a;
                a = a.Next;
            }

            n.Next = p;
            // Fisrt Node.Next -->  null
            L.Head.Next.Next = null;
            L.Head.Next = n;
        }
    }
}
