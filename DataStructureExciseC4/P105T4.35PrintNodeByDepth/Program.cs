using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._35PrintNodeByDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = Tools.MakeRandomTree(100);
            Tools.Print(tree);
        }
    }

    public class NodeWithDepth
    {
        public BSTNode<int> Node { get; set; }
        public int Depth { get; set; }
        public NodeWithDepth(BSTNode<int> node, int depth)
        {
            Node = node;
            Depth = depth;
        }
    }

    public static class Tools
    {
        private static Queue<NodeWithDepth> NodeQueue { get; set; }
        private static int LastDepth { get; set; }

        public static void Print(BSTree<int> tree)
        {
            NodeQueue = new Queue<NodeWithDepth>();
            LastDepth = 0;
            NodeQueue.Enqueue(new NodeWithDepth(tree.Root, 0));
            while (NodeQueue.Count > 0)
            {
                var p = NodeQueue.Dequeue();
                if (p.Node == null)
                {
                    continue;
                }

                if (p.Depth == LastDepth)
                {
                    Console.Write($"{p.Node.Element} ");
                }
                else
                {
                    LastDepth = p.Depth;
                    Console.WriteLine();
                    Console.Write($"{p.Node.Element} ");
                }
                NodeQueue.Enqueue(new NodeWithDepth(p.Node.LeftChild, p.Depth + 1));
                NodeQueue.Enqueue(new NodeWithDepth(p.Node.RightChild, p.Depth + 1));
            }
        }

        private static Random Random { get; set; } = new Random();

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
