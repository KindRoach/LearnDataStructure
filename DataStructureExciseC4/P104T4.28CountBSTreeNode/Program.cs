using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P104T4._28CountBSTreeNode
{
    // 统计一颗二叉树中：
    // 1、节点的个数
    // 2、叶的个数
    // 3、满节点的个数
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BSTree<int>();
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(4);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(12);
            tree.Insert(-9);
            Console.WriteLine(CountNode<int>.AllNode(tree.Root));
            Console.WriteLine(CountNode<int>.AllLeaf(tree.Root));
            Console.WriteLine(CountNode<int>.AllFullNode(tree.Root));
        }
    }

    public static class CountNode<T>
        where T : IComparable
    {
        public static int AllNode(BSTNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + AllNode(root.LeftChild) + AllNode(root.RightChild);
        }

        public static int AllLeaf(BSTNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.LeftChild == null && root.RightChild == null)
            {
                return 1;
            }
            else
            {
                return AllLeaf(root.LeftChild) + AllLeaf(root.RightChild);
            }
        }

        public static int AllFullNode(BSTNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.LeftChild == null || root.RightChild == null)
            {
                return AllFullNode(root.LeftChild) + AllFullNode(root.RightChild);
            }

            return 1 + AllFullNode(root.LeftChild) + AllFullNode(root.RightChild);
        }
    }

}
