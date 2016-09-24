using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P104T4._29MakeRandomTree
{
    public class BSTNode<T>
    where T : IComparable
    {
        public T Element { get; set; }
        /// <summary>
        /// 根的双亲是他自己
        /// </summary>
        public BSTNode<T> Parent { get; set; }
        public BSTNode<T> LeftChild { get; set; }
        public BSTNode<T> RightChild { get; set; }

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
    }
}
