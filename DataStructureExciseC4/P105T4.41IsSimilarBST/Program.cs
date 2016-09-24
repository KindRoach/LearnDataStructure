using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._41IsSimilarBST
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree1 = Tools.MakeRandomTree(2);
            var tree2 = Tools.MakeRandomTree(2);
            Console.WriteLine(IsSimilar(tree1.Root, tree2.Root));
        }

        static bool IsSimilar(BSTNode<int> first, BSTNode<int> second)
        {
            if (first == null || second == null)
            {
                return (first == null && second == null);
            }
            else
            {
                return (IsSimilar(first.LeftChild, second.LeftChild) &&
                        IsSimilar(first.RightChild, second.RightChild));
            }
        }
    }

    public static class Tools
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
