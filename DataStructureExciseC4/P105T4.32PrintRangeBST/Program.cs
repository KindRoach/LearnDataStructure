using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._32PrintRangeBST
{
    // 输出查找二叉树中lower<=k<=uper的数----K+LogN
    class Program
    {
        static void Main(string[] args)
        {
            var tree = Tree.MakeRandomTree(100);
            PrintRangeBST(tree.Root, 3, 15);
            return;

        }

        static void PrintRangeBST(BSTNode<int> node, int lower, int uper)
        {
            if (node != null)
            {
                if (node.Element >= lower)
                {
                    PrintRangeBST(node.LeftChild, lower, uper);
                }
                if (lower <= node.Element && node.Element <= uper)
                {
                    Console.Write($"{node.Element} ");
                }
                if (node.Element <= uper)
                {
                    PrintRangeBST(node.RightChild, lower, uper);
                }

            }
        }
    }

    public static class Tree
    {
        public static Random Random { get; set; } = new Random();

        public static BSTree<int> MakeRandomTree(int nodeNum)
        {
            var tree = new BSTree<int>();
            tree.Root = MakeTree(1, nodeNum, null);
            return tree;
        }

        private static BSTNode<int> MakeTree(int lower, int uper, BSTNode<int> parent)
        {
            if (lower > uper)
            {
                return null;
            }
            int midle = Random.Next(lower, uper + 1);
            var node = new BSTNode<int>(midle, parent);
            node.LeftChild = MakeTree(lower, midle - 1, node);
            node.RightChild = MakeTree(midle + 1, uper, node);
            return node;
        }
    }


}
