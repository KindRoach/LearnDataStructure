using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P80AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class AVLNode<T>
        where T : IComparable
    {
        public T Element { get; set; }
        public AVLNode<T> LeftChild { get; set; }
        public AVLNode<T> RightChild { get; set; }
        public AVLNode<T> Parent { get; set; }
        public short Height { get; set; }

        public AVLNode(T element, AVLNode<T> parent)
        {
            Element = element;
            Parent = parent;
            Height = 0;
        }

        public AVLNode()
        {
        }
    }

    public class AVLTree<T>
        where T : IComparable
    {
        public AVLNode<T> Root { get; set; }

        /// <summary>
        /// null for not found
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public AVLNode<T> Find(T element)
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
        public AVLNode<T> FindMin()
        {
            return FindMin(Root);
        }

        private AVLNode<T> FindMin(AVLNode<T> root)
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
        public AVLNode<T> FindMax()
        {
            return FindMax(Root);
        }

        private AVLNode<T> FindMax(AVLNode<T> root)
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

        public void Insert(T element)
        {
            Root = Insert(Root, null, element);

        }

        private short GetHeight(AVLNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }
            return node.Height;
        }

        private AVLNode<T> Insert(AVLNode<T> root, AVLNode<T> parent, T element)
        {
            if (root == null)
            {
                root = new AVLNode<T>(element, parent);
            }
            else if (element.CompareTo(root.Element) < 0)
            {
                root.LeftChild = Insert(root.LeftChild, root, element);
                if (GetHeight(root.LeftChild) - GetHeight(root.RightChild) == 2)
                {
                    if (element.CompareTo(root.LeftChild) < 0)
                    {
                        root = SingleRotateLeft(root);
                    }
                    else
                    {
                        root = DoubleRotateLeft(root);
                    }
                }

            }
            else if (element.CompareTo(root.Element) > 0)
            {
                root.RightChild = Insert(root.RightChild, root, element);
                if (GetHeight(root.RightChild) - GetHeight(root.LeftChild) == 2)
                {
                    if (element.CompareTo(root.RightChild) < 0)
                    {
                        root = SingleRotateRight(root);
                    }
                    else
                    {
                        root = DoubleRotateRight(root);
                    }
                }

            }
            //else element is already existed in the tree, we do nothing.
            root.Height = (short)(Math.Max(GetHeight(root.LeftChild), GetHeight(root.RightChild)) + 1);
            return root;
        }

        private AVLNode<T> DoubleRotateRight(AVLNode<T> root)
        {
            throw new NotImplementedException();
        }

        private AVLNode<T> SingleRotateRight(AVLNode<T> root)
        {
            throw new NotImplementedException();
        }

        private AVLNode<T> DoubleRotateLeft(AVLNode<T> root)
        {
            throw new NotImplementedException();
        }

        private AVLNode<T> SingleRotateLeft(AVLNode<T> root)
        {
            throw new NotImplementedException();
        }

    }
}
