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
            var node = new BTreeNode<int>(1000, null);
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
        public BTreeNode<T> Parent;
        public BTreeNode<T>[] Indexes { get; set; }

        public BTreeNode(int capacity, BTreeNode<T> parent)
        {
            KeyNum = 0;
            Capacity = capacity;
            Parent = parent;
            Keys = new T[capacity + 1];
            Indexes = new BTreeNode<T>[capacity + 1];
        }

        /// <summary>
        /// 找到返回index，找不到返回最后一个小于key的索引的补码，如果key小于所有的关键字，返回-2
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int FindIndex(T key)
        {
            if (key.CompareTo(Keys[0]) < 0)
            {
                return -2;
            }
            else if (key.CompareTo(Keys[KeyNum - 1]) > 0)
            {
                return ~(KeyNum - 1);
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
            return ~low;
        }

        public void Insert(int index, T key)
        {
            KeyNum++;
            for (int i = KeyNum - 1; i >= index; i--)
            {
                Keys[i] = Keys[i - 1];
            }
            Keys[index] = key;
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
            if (index > 0)
            {
                return root.Keys[index];
            }
            else
            {
                return Find(root.Indexes[~index + 1], key);
            }
        }

        //public void Insert(T key)
        //{
        //    Root = Insert(Root, key, null);
        //}

        //private BTreeNode<T> Insert(BTreeNode<T> root, T key, BTreeNode<T> parent)
        //{
        //    if (root == null)
        //    {
        //        var node = new BTreeNode<T>(Order - 1, parent);
        //        node.KeyNum++;
        //        node.Keys[0] = key;
        //        return root;
        //    }

        //    int index = root.FindIndex(key);
        //    if (index > 0)      //找到了
        //    {
        //        return root;
        //    }
        //    else
        //    {
        //        index = ~index;   //没找到，获取第一个小于等于key的元素索引
        //    }

        //    // 没有装满
        //    if (root.KeyNum < root.Capacity)
        //    {
        //        // 没有适合的子树
        //        if (root.Indexes[index + 1] == null)
        //        {
        //            root.Insert(index + 1, key);
        //        }
        //        else
        //        {
        //            root.Indexes[index + 1] = Insert(root.Indexes[index + 1], key, root);
        //        }
        //    }

        //    // 装满了，分裂
        //    root.Insert(index + 1, key);
        //    int mid = Order / 2;
        //    var newRoot = new BTreeNode<T>(Order - 1);
        //    newRoot.KeyNum = 1;
        //    newRoot.Keys[0] =

        //}
    }
}
