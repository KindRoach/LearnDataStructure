﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P73BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BSTree<int>();
            tree.Insert(3);
            tree.Insert(2);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(6);
            tree.Insert(10);
            Console.WriteLine(tree.FindKth(2).Element);
        }
    }

    public class BSTNode<T>
        where T : IComparable
    {
        public T Element { get; set; }
        /// <summary>
        /// 根的双亲是null
        /// </summary>
        public BSTNode<T> Parent { get; set; }
        public BSTNode<T> LeftChild { get; set; }
        public BSTNode<T> RightChild { get; set; }
        /// <summary>
        /// 这个是用来搞O(logN)FindKth的
        /// </summary>
        public int Size { get; set; }

        public BSTNode(T element, BSTNode<T> parent)
        {
            Element = element;
            Parent = parent;
        }

    }

    public class BSTree<T>
        where T : IComparable
    {
        public BSTNode<T> Root { get; set; }

        public enum PrintType
        {
            /// <summary>
            /// 前序遍历（Data_Left_Right）
            /// </summary>
            DLR,
            /// <summary>
            /// 中序遍历（Left_Data_Right）
            /// </summary>
            LDR,
            /// <summary>
            /// 后序遍历（Left_Right_Data）
            /// </summary>
            LRD
        }

        /// <summary>
        /// no change for element already existed
        /// </summary>
        /// <param name="element"></param>
        public virtual void Insert(T element)
        {
            if (Root != null)
            {
                var p = Root;
                while (p != null)
                {
                    if (element.CompareTo(p.Element) < 0)
                    {
                        if (p.LeftChild == null)
                        {
                            p.LeftChild = new BSTNode<T>(element, p);
                        }
                        p = p.LeftChild;

                    }
                    else if (element.CompareTo(p.Element) > 0)
                    {
                        if (p.RightChild == null)
                        {
                            p.RightChild = new BSTNode<T>(element, p);
                        }
                        p = p.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                Root = new BSTNode<T>(element, null);
            }
        }

        /// <summary>
        /// null for not found
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public BSTNode<T> Find(T element)
        {
            var p = Root;
            while (p != null)
            {
                if (element.CompareTo(p.Element) < 0)
                {
                    p = p.LeftChild;
                }
                else if (element.CompareTo(p.Element) > 0)
                {
                    p = p.RightChild;
                }
                else
                {
                    return p;
                }
            }
            return null;
        }



        /// <summary>
        /// null for no element in the tree
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public BSTNode<T> FindMin()
        {
            return FindMin(Root);
        }

        private BSTNode<T> FindMin(BSTNode<T> root)
        {
            var p = root;
            while (p != null)
            {
                if (p.LeftChild != null)
                {
                    p = p.LeftChild;
                }
                else
                {
                    return p;
                }
            }
            return null;
        }

        /// <summary>
        /// null for no element in the tree
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public BSTNode<T> FindMax()
        {
            return FindMax(Root);
        }

        private BSTNode<T> FindMax(BSTNode<T> root)
        {
            var p = root;
            while (p != null)
            {
                if (p.RightChild != null)
                {
                    p = p.RightChild;
                }
                else
                {
                    return p;
                }
            }
            return null;
        }

        public void Print(PrintType type)
        {
            switch (type)
            {
                case PrintType.DLR:
                    PrintDLR(Root);
                    break;
                case PrintType.LDR:
                    PrintLDR(Root);
                    break;
                case PrintType.LRD:
                    PrintLRD(Root);
                    break;
                default:
                    break;
            }
            Console.WriteLine();
        }

        private void PrintDLR(BSTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write($"{node.Element} ");
            PrintDLR(node.LeftChild);
            PrintDLR(node.RightChild);
        }

        private void PrintLDR(BSTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            PrintLDR(node.LeftChild);
            Console.Write($"{node.Element} ");
            PrintLDR(node.RightChild);
        }

        private void PrintLRD(BSTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            PrintLRD(node.LeftChild);
            PrintLRD(node.RightChild);
            Console.Write($"{node.Element} ");
        }

        /// <summary>
        /// no change for element not found
        /// </summary>
        /// <param name="element"></param>
        public virtual void Delete(T element)
        {
            var p = Find(element);
            DeleteNode(p);
        }

        /// <summary>
        /// no change if node is null
        /// </summary>
        /// <param name="node"></param>
        private void DeleteNode(BSTNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftChild != null && node.RightChild != null)
            {
                var replace = FindMin(node.RightChild);
                node.Element = replace.Element;
                DeleteNode(replace);
            }
            else if (node.LeftChild != null)
            {
                node.Element = node.LeftChild.Element;
                node.LeftChild = null;
            }
            else if (node.RightChild != null)
            {
                node.Element = node.RightChild.Element;
                node.RightChild = null;
            }
            else
            {
                if (node.Parent.Element.CompareTo(node.Element) < 0)
                {
                    node.Parent.RightChild = null;
                }
                else
                {
                    node.Parent.LeftChild = null;
                }
            }
        }

        public BSTNode<T> FindKth(int k)
        {
            GetSize(Root);
            return FindKth(Root, k);
        }

        private BSTNode<T> FindKth(BSTNode<T> root, int k)
        {
            if (k > root.Size)
            {
                throw new Exception("K is bigger than elements num");
            }

            if (root.LeftChild.Size + 1 == k)
            {
                return root;
            }

            if (root.LeftChild.Size >= k)
            {
                return FindKth(root.LeftChild, k);
            }
            else
            {
                return FindKth(root.RightChild, k - root.LeftChild.Size - 1);
            }
        }

        private int GetSize(BSTNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            root.Size = 1 + GetSize(root.LeftChild) + GetSize(root.RightChild);
            return root.Size;
        }
    }
}
