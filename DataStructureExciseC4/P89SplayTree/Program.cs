using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P89SplayTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new STree<int>();
            for (int i = 1; i < 5; i++)
            {
                tree.Insert(i);
            }
            Console.WriteLine(tree.Find(4).Key);
            Console.WriteLine(tree.Root.Key);
        }
    }

    class STreeNode<T>
        where T : IComparable
    {
        public T Key { get; set; }
        public STreeNode<T> LeftChild { get; set; }
        public STreeNode<T> RightChild { get; set; }
        public STreeNode<T> Parent { get; set; }

        public STreeNode(T key, STreeNode<T> parent)
        {
            Key = key;
            Parent = parent;
        }

    }

    class STree<T>
        where T : IComparable
    {
        public STreeNode<T> Root { get; set; }

        public STreeNode<T> Rotate(STreeNode<T> x)
        {
            if (x.Parent == null)
            {
                return x;
            }

            // x只有Parent，没有Grandparent
            if (x.Parent.Parent == null)
            {
                if (x.Key.CompareTo(x.Parent.Key) < 0)
                {
                    return SingleRotateLeft(x);
                }
                else
                {
                    return SingleRotateRight(x);
                }
            }

            // x有Grandparent
            if (x.Key.CompareTo(x.Parent.Parent.Key) < 0)
            {
                if (x.Key.CompareTo(x.Parent.Key) < 0)
                {
                    return ZigzigLeft(x);
                }
                else
                {
                    return ZigzagLeft(x);
                }
            }
            else
            {
                if (x.Key.CompareTo(x.Parent.Key) < 0)
                {
                    return ZigzagRight(x);
                }
                else
                {
                    return ZigzigRight(x);
                }
            }
        }

        private STreeNode<T> ZigzigRight(STreeNode<T> x)
        {
            var p = x.Parent;
            var g = p.Parent;
            p = SingleRotateRight(p);
            return SingleRotateRight(x);
        }

        private STreeNode<T> ZigzagRight(STreeNode<T> x)
        {
            var p = x.Parent;
            var g = p.Parent;
            x = SingleRotateLeft(x);
            return SingleRotateRight(x);
        }

        private STreeNode<T> ZigzagLeft(STreeNode<T> x)
        {
            var p = x.Parent;
            var g = p.Parent;
            x = SingleRotateRight(x);
            return SingleRotateLeft(x);
        }

        private STreeNode<T> ZigzigLeft(STreeNode<T> x)
        {
            var p = x.Parent;
            var g = p.Parent;
            p = SingleRotateLeft(p);
            return SingleRotateLeft(x);
        }

        private STreeNode<T> SingleRotateRight(STreeNode<T> x)
        {
            var p = x.Parent;

            p.RightChild = x.LeftChild;
            // 此行务必要有
            if (x.LeftChild != null) x.LeftChild.Parent = p;

            x.LeftChild = p;
            x.Parent = p.Parent;
            p.Parent = x;
            // 以下代码我觉得应该有，但是不加好像也没有关系
            // 作用是让旋转之后的x的parent指向它，而非原来的child
            // 不过因为x会停的旋转直到成为root
            // 所以parent没有指向它的bug会在后续的旋转中被自动修正，Σ(っ °Д °;)っ
            //if (x.Parent != null)
            //{
            //    if (x.Key.CompareTo(x.Parent.Key) < 0)
            //    {
            //        x.Parent.LeftChild = x;
            //    }
            //    else
            //    {
            //        x.Parent.RightChild = x;
            //    }
            //}
            return x;
        }

        private STreeNode<T> SingleRotateLeft(STreeNode<T> x)
        {
            var p = x.Parent;

            p.LeftChild = x.RightChild;
            if (x.RightChild != null) x.RightChild.Parent = p;

            x.RightChild = p;
            x.Parent = p.Parent;
            p.Parent = x;
            //if (x.Parent != null)
            //{
            //    if (x.Key.CompareTo(x.Parent.Key) < 0)
            //    {
            //        x.Parent.LeftChild = x;
            //    }
            //    else
            //    {
            //        x.Parent.RightChild = x;
            //    }
            //}
            return x;
        }

        private void RotateToRoot(STreeNode<T> node)
        {
            while (node.Parent != null)
            {
                node = Rotate(node);
            }
            Root = node;
        }

        public STreeNode<T> Find(T key)
        {
            return Find(Root, key);
        }

        private STreeNode<T> Find(STreeNode<T> root, T key)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Key.CompareTo(key) > 0)
            {
                return Find(root.LeftChild, key);
            }
            else if (root.Key.CompareTo(key) < 0)
            {
                return Find(root.RightChild, key);
            }
            else
            {
                RotateToRoot(root);
                return root;
            }
        }

        public void Insert(T key)
        {
            Root = Insert(Root, key, null);
        }

        /// <summary>
        /// 插入已存在的key，不做任何改变
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private STreeNode<T> Insert(STreeNode<T> root, T key, STreeNode<T> parent)
        {
            if (root == null)
            {
                return new STreeNode<T>(key, parent);
            }

            if (root.Key.CompareTo(key) > 0)
            {
                root.LeftChild = Insert(root.LeftChild, key, root);
            }
            else if (root.Key.CompareTo(key) < 0)
            {
                root.RightChild = Insert(root.RightChild, key, root);
            }
            return root;
        }

        public void Delete(T key)
        {
            var p = Find(key);
            var tl = p.LeftChild;
            var tr = p.RightChild;
            var newRoot = FindMax(tl);
            newRoot.RightChild = tr;
            if (tr!=null) tr.Parent = newRoot;
            Root = newRoot;
        }

        private STreeNode<T> FindMax(STreeNode<T> root)
        {
            while (root.RightChild != null)
            {
                root = root.LeftChild;
            }
            RotateToRoot(root);
            return root;
        }
    }
}
