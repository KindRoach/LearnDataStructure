using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P62T3._3SwapListNode
{
    // 只更改"指针"交换链表中的项
    class Program
    {
        static void Main(string[] args)
        {
            var list = GetList();
            var node1 = list.FindFirst(2);
            var node2 = list.FindFirst(5);
            SwapNode(list, node1, node2);
            list.Print();
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

        static void SwapNode(MyList<int> list, MyListNode<int> node1, MyListNode<int> node2)
        {
            if (node1 == null ||
                node2 == null ||
                node1 == list.Head ||
                node2 == list.Head)
            {
                throw new ArgumentException("Can't swap null node or head.");
            }

            var p1 = list.FindPrevious(node1);
            var p2 = list.FindPrevious(node2);
            var n2 = node2.Next;

            p1.Next = node2;
            node2.Next = node1.Next;
            p2.Next = node1;
            node1.Next = n2;
        }
    }
}
