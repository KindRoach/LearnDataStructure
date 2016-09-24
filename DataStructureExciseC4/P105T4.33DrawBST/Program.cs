using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._33DrawBST
{
    // 为每个二叉树的节点分配二维坐标
    class Program
    {
        static void Main(string[] args)
        {
            var tree = MakeRandomTree(10);
            int index = 0;
            DrawBST(tree.Root, 0, ref index);
            return;
        }

        static void DrawBST(BSTNode<int> root, int depth, ref int index)
        {
            if (root == null)
            {
                return;
            }

            DrawBST(root.LeftChild, depth + 1, ref index);
            index++;
            root.X = index;
            root.Y = -depth;
            DrawCricle(root);
            DrawBST(root.RightChild, depth + 1, ref index);
            DrawLine(root, root.LeftChild);
            DrawLine(root, root.RightChild);

        }

        private static void DrawCricle(BSTNode<int> root)
        {
            // 画图
        }

        private static void DrawLine(BSTNode<int> node1, BSTNode<int> node2)
        {
            if (node1 != null && node2 != null)
            {
                // 画图
            }
        }

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
