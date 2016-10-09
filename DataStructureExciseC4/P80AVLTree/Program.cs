using System;

namespace P80AVLTree
{
    internal class Program
    {
        // 3, 2, 1 ,4 ,5 ,6 ,7 ,16 ,15 ,14 ,13 ,12 ,11, 10, 8, 9
        private static void Main(string[] args)
        {
            var A = new int[] { 3, 2, 1, 4, 5, 6, 7, 16, 15, 14, 13, 12, 11, 10, 8, 9 };
            var tree = new AVLTree<int>();
            for (int i = 0; i < 100; i++)
            {
                tree.Insert(i);
            }

            //tree.Delete(7);
            //tree.Delete(6);

            tree.Print(AVLTree<int>.PrintType.DLR);
            tree.Print(AVLTree<int>.PrintType.LDR);
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
            if (element == null)
            {
                throw new ArgumentNullException();
            }
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

        /// <summary>
        /// no change if element already existed
        /// </summary>
        /// <param name="element"></param>
        public void Insert(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }
            Root = Insert(Root, null, element);
            //Root = Insert(Root, element);
        }

        private short GetHeight(AVLNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }
            return node.Height;
        }

        private short CalculateHeight(AVLNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node is null");
            }
            return (short)(Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1);
        }

        /// <summary>
        /// recursion version 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="parent"></param>
        /// <param name="element"></param>
        /// <returns></returns>
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
                    root = LeftBlance(root);
                }
            }
            else if (element.CompareTo(root.Element) > 0)
            {
                root.RightChild = Insert(root.RightChild, root, element);
                if (GetHeight(root.RightChild) - GetHeight(root.LeftChild) == 2)
                {
                    root = RightBlance(root);
                }
            }
            //else element is already existed in the tree, we do nothing.
            root.Height = CalculateHeight(root);
            return root;
        }

        private AVLNode<T> SingleRotateLeft(AVLNode<T> root)
        {
            var oldRoot = root;
            var newRoot = root.LeftChild;
            newRoot.Parent = oldRoot.Parent;

            oldRoot.LeftChild = newRoot.RightChild;
            if (newRoot.RightChild != null) newRoot.RightChild.Parent = oldRoot;

            newRoot.Parent = oldRoot.Parent;
            oldRoot.Parent = newRoot;
            oldRoot.Height = CalculateHeight(oldRoot);
            newRoot.Height = CalculateHeight(newRoot);
            return newRoot;
        }

        private AVLNode<T> SingleRotateRight(AVLNode<T> root)
        {
            var oldRoot = root;
            var newRoot = root.RightChild;
            newRoot.Parent = oldRoot.Parent;

            oldRoot.RightChild = newRoot.LeftChild;
            if (newRoot.LeftChild != null) newRoot.LeftChild.Parent = oldRoot;

            newRoot.LeftChild = oldRoot;
            oldRoot.Parent = newRoot;
            oldRoot.Height = CalculateHeight(oldRoot);
            newRoot.Height = CalculateHeight(newRoot);
            return newRoot;
        }

        private AVLNode<T> DoubleRotateLeft(AVLNode<T> root)
        {
            root.LeftChild = SingleRotateRight(root.LeftChild);
            root = SingleRotateLeft(root);
            return root;
        }

        private AVLNode<T> DoubleRotateRight(AVLNode<T> root)
        {
            root.RightChild = SingleRotateLeft(root.RightChild);
            root = SingleRotateRight(root);
            return root;
        }

        private AVLNode<T> LeftBlance(AVLNode<T> root)
        {
            var p = root.LeftChild;
            if (GetHeight(p.LeftChild) > GetHeight(p.RightChild))
            {
                root = SingleRotateLeft(root);
            }
            else
            {
                root = DoubleRotateLeft(root);
            }
            return root;
        }

        private AVLNode<T> RightBlance(AVLNode<T> root)
        {
            var p = root.RightChild;
            if (GetHeight(p.LeftChild) < GetHeight(p.RightChild))
            {
                root = SingleRotateRight(root);
            }
            else
            {
                root = DoubleRotateRight(root);
            }
            return root;
        }

        /// <summary>
        /// no change if element not found
        /// </summary>
        /// <param name="element"></param>
        public void Delete(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }
            Root = Delete(Root, element);
        }

        private AVLNode<T> Delete(AVLNode<T> root, T element)
        {
            if (root == null)
            {
                return null;
            }
            if (element.CompareTo(root.Element) < 0)
            {
                root.LeftChild = Delete(root.LeftChild, element);
                if (GetHeight(root.RightChild) - GetHeight(root.LeftChild) == 2)
                {
                    root = RightBlance(root);
                }
            }
            else if (element.CompareTo(root.Element) > 0)
            {
                root.RightChild = Delete(root.RightChild, element);
                if (GetHeight(root.LeftChild) - GetHeight(root.RightChild) == 2)
                {
                    root = LeftBlance(root);
                }
            }
            else
            {
                if (root.LeftChild == null)
                {
                    return root.RightChild;
                }
                else if (root.RightChild == null)
                {
                    return root.LeftChild;
                }
                else
                {
                    var p = FindMax(root.LeftChild);
                    root.Element = p.Element;
                    root.LeftChild = Delete(root.LeftChild, p.Element);
                }
            }
            root.Height = CalculateHeight(root);
            return root;
        }

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

        private void PrintDLR(AVLNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write($"{node.Element} ");
            PrintDLR(node.LeftChild);
            PrintDLR(node.RightChild);
        }

        private void PrintLDR(AVLNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            PrintLDR(node.LeftChild);
            Console.Write($"{node.Element} ");
            PrintLDR(node.RightChild);
        }

        private void PrintLRD(AVLNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            PrintLRD(node.LeftChild);
            PrintLRD(node.RightChild);
            Console.Write($"{node.Element} ");
        }
    }
}