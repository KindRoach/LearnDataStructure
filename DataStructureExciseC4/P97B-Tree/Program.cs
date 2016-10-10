using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P97B_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var node = new BTreeNode<int>(1000);
            node.KeyNum = 1000;
            for (int i = 0; i < 1000; i++)
            {
                node.Keys[i] = 10 * i + 5;
            }
            for (int i = 1; i < 1001; i++)
            {
                Console.WriteLine($"Find({10 * i + 3})=({node.FindIndex(10 * i + 3)})");
            }
        }
    }

    class BTreeNode<T>
        where T : IComparable
    {
        /// <summary>
        /// 当前拥有的关键字数量
        /// </summary>
        public int KeyNum { get; set; }
        /// <summary>
        /// 关键字的最大容量
        /// </summary>
        public int Capacity { get; set; }
        public T[] Keys { get; set; }
        public BTreeNode<T>[] Indexes { get; set; }

        public BTreeNode(int capacity)
        {
            KeyNum = 0;
            Capacity = capacity;
            Keys = new T[capacity];
            Indexes = new BTreeNode<T>[capacity + 1];
        }

        /// <summary>
        /// 找到小于等于的key的索引，如果key小于所有的关键字，返回-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int FindIndex(T key)
        {
            if (key.CompareTo(Keys[0]) < 0)
            {
                return -1;
            }
            else if (key.CompareTo(Keys[KeyNum - 1]) > 0)
            {
                return KeyNum - 1;
            }

            int low = 0;
            int uper = KeyNum - 1;
            int mid;
            while (low + 1 < uper)
            {
                mid = (low + uper) / 2;
                if (key.CompareTo(Keys[mid]) < 0)
                {
                    uper = mid;
                }
                else if (key.CompareTo(Keys[mid]) > 0)
                {
                    low = mid;
                }
                else
                {
                    return mid;
                }
            }
            return low;
        }
    }

    class BTree<T>
        where T : IComparable
    {
        public int Order { get; set; }
        public BTreeNode<T> Root { get; set; }

        public BTree(int order)
        {
            Order = order;
        }

        public T Find(T key)
        {
            return Find(Root, key);
        }

        private T Find(BTreeNode<T> root, T key)
        {
            int index = root.FindIndex(key);
            if (root.Keys[index].Equals(key))
            {
                return root.Keys[index];
            }
            else
            {
                return Find(root.Indexes[index + 1], key);
            }

        }
    }
}
