using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P152BinomialQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var H1 = new BinQueue<int>();
            for (int i = 0; i < 16; i++)
            {
                BinQueue<int>.Insert(H1, i);
            }

            var H2 = new BinQueue<int>();
            for (int i = 16; i < 64; i++)
            {
                BinQueue<int>.Insert(H2, i);
            }

            H1 = BinQueue<int>.Marge(H1, H2);
            while (H1.Count != 0)
            {
                Console.WriteLine(BinQueue<int>.DeleteMin(H1));
            }
        }
    }

    public class BinTree<T>
        where T : IComparable
    {
        public T Val { get; set; }
        public BinTree<T> FirstChild { get; set; }
        public BinTree<T> NextSibling { get; set; }
        /// <summary>
        /// make single node tree
        /// </summary>
        /// <param name="val"></param>
        public BinTree(T val)
        {
            Val = val;
        }
    }

    public class BinQueue<T>
        where T : IComparable
    {
        public static readonly int Capacity = int.MaxValue;
        public static readonly int MaxTrees = 30;
        public int Count { get; set; }
        public BinTree<T>[] Trees { get; set; }

        public BinQueue()
        {
            Trees = new BinTree<T>[MaxTrees];
        }

        private static BinTree<T> MargeNode(BinTree<T> T1, BinTree<T> T2)
        {
            if (T1.Val.CompareTo(T2.Val) > 0)
            {
                return MargeNode(T2, T1);
            }
            T2.NextSibling = T1.FirstChild;
            T1.FirstChild = T2;
            return T1;
        }

        /// <summary>
        /// H1 become the outcome,trees of H2 become null;
        /// </summary>
        /// <param name="H1"></param>
        /// <param name="H2"></param>
        /// <returns></returns>
        public static BinQueue<T> Marge(BinQueue<T> H1, BinQueue<T> H2)
        {
            BinTree<T> T1, T2, Carry = null;
            if (H1.Count + H2.Count > Capacity)
            {
                throw new Exception("Marge would exceed capacity.");
            }
            H1.Count += H2.Count;
            H2.Count = 0;

            int i, j;
            short f1, f2, fc;
            for (i = 0, j = 1; j <= H1.Count; i++, j *= 2)
            {
                T1 = H1.Trees[i];
                T2 = H2.Trees[i];
                if (T1 == null) f1 = 0; else f1 = 1;
                if (T2 == null) f2 = 0; else f2 = 1;
                if (Carry == null) fc = 0; else fc = 1;
                switch (f1 + 2 * f2 + 4 * fc)
                {
                    case 0: break;  // No Trees
                    case 1: break;  // Only T1
                    case 2:         // Only T2
                        H1.Trees[i] = T2;
                        H2.Trees[i] = null;
                        break;
                    case 4:         // Only Carry
                        H1.Trees[i] = Carry;
                        Carry = null;
                        break;
                    case 3:         // T1 & T2
                        Carry = MargeNode(T1, T2);
                        H1.Trees[i] = null;
                        H2.Trees[i] = null;
                        break;
                    case 5:         // T1 & Carry
                        Carry = MargeNode(T1, Carry);
                        H1.Trees[i] = null;
                        break;
                    case 6:         // T2 & Carry
                        Carry = MargeNode(T2, Carry);
                        H2.Trees[i] = null;
                        break;
                    case 7:         // T1 & T2 & Carry
                        H1.Trees[i] = Carry;
                        Carry = MargeNode(T1, T2);
                        H2.Trees[i] = null;
                        break;
                }
            }
            return H1;
        }

        public static void Insert(BinQueue<T> H, T val)
        {
            var tempH = new BinQueue<T>();
            var tempT = new BinTree<T>(val);
            tempH.Trees[0] = tempT;
            tempH.Count = 1;
            H = Marge(H, tempH);
        }

        public static T DeleteMin(BinQueue<T> H)
        {
            int i = 0, j = 0;
            int MinTree;
            BinQueue<T> DeletedQueue;
            BinTree<T> DeletedTree;

            if (H.Count == 0)
            {
                throw new Exception("BinQueue is empty.");
            }

            while (H.Trees[i] == null)
            {
                i++;
            }
            T minVal = H.Trees[i].Val;
            MinTree = i;

            for (i = i + 1; i < MaxTrees; i++)
            {
                if (H.Trees[i] != null && H.Trees[i].Val.CompareTo(minVal) < 0)
                {
                    minVal = H.Trees[i].Val;
                    MinTree = i;
                }
            }

            DeletedTree = H.Trees[MinTree].FirstChild;
            H.Trees[MinTree] = null;
            DeletedQueue = new BinQueue<T>();
            DeletedQueue.Count = (1 << MinTree) - 1;
            H.Count = H.Count - DeletedQueue.Count - 1;
            for (j = MinTree - 1; j >= 0; j--)
            {
                DeletedQueue.Trees[j] = DeletedTree;
                DeletedTree = DeletedTree.NextSibling;
                DeletedQueue.Trees[j].NextSibling = null;
            }

            H = Marge(H, DeletedQueue);
            return minVal;
        }
    }
}
