using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P104T4._29MakeRandomTree
{
    // 生成一颗包含1-N节点随机查找二叉树
    class Program
    {
        static void Main(string[] args)
        {
            var tree = Tree.MakeRandomTree(10);
            tree.Print(BSTree<int>.PrintType.DLR);
            tree.Print(BSTree<int>.PrintType.LDR);
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
