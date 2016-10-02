using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P145LeftHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 100000;
            var numA = GetNums(size);
            var numB = GetNums(size);
            var num = MargeNums(numA, numB);
            var heap = GetHeap(numA);
            heap.MargeWith(GetHeap(numB));
            Array.Sort(num);

            bool isRight = true;
            foreach (var item in num)
            {
                if (item != heap.RemoveMin())
                {
                    isRight = false;
                }
            }
            Console.WriteLine(isRight);


        }


        // 生成[lower,uper]的随机数组,不重复
        static int[] GetNums(int lower, int uper)
        {
            var nums = new int[uper - lower + 1];
            var random = new Random();
            for (int i = 0; i < nums.Count(); i++)
            {
                nums[i] = i + lower;
            }
            for (int i = 1; i < nums.Count(); i++)
            {
                Swap(ref nums[i], ref nums[random.Next(0, i + 1)]);
            }
            return nums;
        }

        // 生成随机数组，可能重复
        static int[] GetNums(int size)
        {
            var nums = new int[size];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                nums[i] = random.Next(100);
            }
            return nums;
        }

        private static void Swap(ref int v1, ref int v2)
        {
            var temp = v1;
            v1 = v2;
            v2 = temp;
        }

        static int[] MargeNums(int[] A, int[] B)
        {
            var ans = new int[A.Count() + B.Count()];
            for (int i = 0; i < A.Count(); i++)
            {
                ans[i] = A[i];
            }

            for (int i = 0; i < B.Count(); i++)
            {
                ans[A.Count() + i] = B[i];
            }

            return ans;
        }

        static LeftHeap<int> GetHeap(int[] A)
        {
            var heap = new LeftHeap<int>();
            foreach (var item in A)
            {
                heap.Insert(item);
            }
            return heap;
        }
    }

    public class HeapNode<T>
        where T : IComparable
    {
        public T Element { get; set; }
        public HeapNode<T> LeftChild { get; set; }
        public HeapNode<T> RightChild { get; set; }
        public int NPL { get; set; }

        public HeapNode(T element)
        {
            Element = element;
            NPL = 0;
        }

        private static void SwapNode(ref HeapNode<T> node1, ref HeapNode<T> node2)
        {
            var temp = node1;
            node1 = node2;
            node2 = temp;
        }

        public static HeapNode<T> Marge(HeapNode<T> H1, HeapNode<T> H2)
        {
            if (H1 == null)
            {
                return H2;
            }

            if (H2 == null)
            {
                return H1;
            }

            if (H1.Element.CompareTo(H2.Element) > 0)
            {
                SwapNode(ref H1, ref H2);
            }

            if (H1.LeftChild == null)
            {
                H1.LeftChild = H2;
            }
            else
            {
                H1.RightChild = Marge(H1.RightChild, H2);
                if (H1.LeftChild.NPL < H1.RightChild.NPL)
                {
                    SwapChild(H1);
                }
                H1.NPL = H1.RightChild.NPL + 1;
            }


            return H1;


        }

        private static void SwapChild(HeapNode<T> H)
        {
            var temp = H.LeftChild;
            H.LeftChild = H.RightChild;
            H.RightChild = temp;
        }
    }

    public class LeftHeap<T>
        where T : IComparable
    {
        public HeapNode<T> Root { get; set; }
        public bool IsEmpty
        {
            get { return Root == null; }
        }

        public void MargeWith(LeftHeap<T> other)
        {
            Root = HeapNode<T>.Marge(Root, other.Root);
        }

        public void Insert(T element)
        {
            var node = new HeapNode<T>(element);
            Root = HeapNode<T>.Marge(Root, node);
        }

        public T RemoveMin()
        {
            if (IsEmpty)
            {
                throw new Exception("heap is empty.");
            }
            var min = Root.Element;
            Root = HeapNode<T>.Marge(Root.LeftChild, Root.RightChild);
            return min;
        }
    }


}
