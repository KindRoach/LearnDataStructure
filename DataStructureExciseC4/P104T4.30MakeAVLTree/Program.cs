using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P104T4._30MakeAVLTree
{
    // 生成具有最少节点、高度为H的AVL树
    // 生成高度为H的满AVL树
    class Program
    {
        static void Main(string[] args)
        {
            var before = DateTime.Now;
            var tree = Tree.MakeFullAVLTree(2);
            tree.Print(AVLTree<int>.PrintType.DLR);
            var after = DateTime.Now;
            Console.WriteLine(after - before);
        }
    }

    public static class Tree
    {
        public static int[] Elements { get; set; } = new int[46];
        public static int LastNode { get; set; }

        public static AVLTree<int> MakeMinAVLTree(int height)
        {
            if (height > 45 || height < 0)
            {
                throw new ArgumentException("height should be in [0,45]");
            }

            var tree = new AVLTree<int>();

            if (Elements[1] == 0)
            {
                Elements[0] = 1;
                Elements[1] = 2;
                for (int i = 2; i <= 45; i++)
                {
                    Elements[i] = Elements[i - 1] + Elements[i - 2];
                }
            }

            tree.Root = MakeMinAVLTree(0, height, null);
            return tree;
        }

        private static AVLNode<int> MakeMinAVLTree(int startIndex, int height, AVLNode<int> parent)
        {
            if (height < 0)
            {
                return null;
            }
            int element = Elements[height] + startIndex;
            var node = new AVLNode<int>(element, parent);
            node.Height = (short)height;
            node.LeftChild = MakeMinAVLTree(startIndex, height - 1, node);
            node.RightChild = MakeMinAVLTree(element, height - 2, node);
            return node;
        }

        public static AVLTree<int> MakeFullAVLTree(int height)
        {
            var tree = new AVLTree<int>();
            LastNode = 0;
            tree.Root = MakeFullAVLTree(height, null);
            return tree;
        }

        private static AVLNode<int> MakeFullAVLTree(int height, AVLNode<int> parent)
        {
            if (height < 0)
            {
                return null;
            }
            var node = new AVLNode<int>(0, parent);
            node.LeftChild = MakeFullAVLTree(height - 1, node);
            LastNode++;
            node.Element = LastNode;
            node.Height = (short)height;
            node.RightChild = MakeFullAVLTree(height - 1, node);
            return node;
        }
    }
}
