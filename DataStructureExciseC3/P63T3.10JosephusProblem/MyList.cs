using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P62T3._3SwapListNode
{
    public class MyList<T>
    {
        public MyListNode<T> Head { get; set; }
        public int Count { get; set; }

        public MyList()
        {
            Head = new MyListNode<T>();
            Count = 0;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        // NUll for T not found
        public MyListNode<T> FindFirst(T target)
        {
            var p = Head.Next;
            int searched = 0;
            // 循环链表搜索数目达到总数，即为没有找到
            while (searched <= Count && !p.Value.Equals(target))
            {
                p = p.Next;
                searched++;
            }
            return p;
        }

        // p is the last of list for T not found
        public MyListNode<T> FindPreviousFirst(T target)
        {
            var p = Head;
            int searched = 0;
            // 循环链表搜索数目达到总数，即为没有找到
            while (searched < Count && !p.Next.Value.Equals(target))
            {
                p = p.Next;
                searched++;
            }
            // 循环链表第一项的前一项是最后一项
            if (p == Head)
            {
                p = Head.Next.Next;
                while (p.Next != null && !p.Next.Value.Equals(target))
                {
                    p = p.Next;
                }
            }
            return p;
        }

        public MyListNode<T> FindPrevious(MyListNode<T> target)
        {
            var p = Head;
            while (p.Next != null && p.Next != target)
            {
                p = p.Next;
            }
            // 循环链表第一项的前一项是最后一项
            if (p == Head)
            {
                p = Head.Next.Next;
                while (p.Next != null && p.Next != target)
                {
                    p = p.Next;
                }
            }
            return p;
        }

        // No change for T not found
        public void DeleteFirst(T target)
        {
            var p = FindPreviousFirst(target);
            if (!p.IsLast())
            {
                var temp = p.Next;
                p.Next = temp.Next;
                Count--;
                // 对于C#没有内存回收
            }
        }

        public void Delete(MyListNode<T> target)
        {
            var p = FindPrevious(target);
            if (!p.IsLast())
            {
                var temp = p.Next;
                p.Next = temp.Next;
                Count--;
                // 对于C#没有内存回收
            }
        }

        public MyListNode<T> InsertAfter(MyListNode<T> last, T newItem)
        {
            var temp = new MyListNode<T>(newItem);
            temp.Next = last.Next;
            last.Next = temp;
            Count++;
            return temp;
        }

        public void Print()
        {
            var p = Head.Next;
            while (p != null)
            {
                Console.Write($"{p.Value} ");
                p = p.Next;
            }
            Console.WriteLine();
        }

    }

    public class MyListNode<T>
    {
        public T Value { get; set; }
        public MyListNode<T> Next { get; set; }

        public MyListNode() { }

        public MyListNode(T newItem)
        {
            Value = newItem;
        }

        public bool IsLast()
        {
            return Next == null;
        }
    }

}
