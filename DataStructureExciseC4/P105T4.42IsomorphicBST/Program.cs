using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._42IsomorphicBST
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree1 = BSTree<int>.MakeRandomTree(100);
            var tree2 = tree1.Copy();

            var p = tree1.Root;
            SwapChild(p);

            p = tree2.Root.LeftChild;
            SwapChild(p);

            Console.WriteLine(Isomoprphic(tree1.Root, tree2.Root));

        }

        private static void SwapChild(BSTNode<int> p)
        {
            if (p != null)
            {
                var temp = p.LeftChild;
                p.LeftChild = p.RightChild;
                p.RightChild = temp;
            }
        }

        static bool Isomoprphic(BSTNode<int> first, BSTNode<int> second)
        {
            if (first != null && second != null)
            {
                if (first.Element == second.Element)
                {
                    return (Isomoprphic(first.LeftChild, second.LeftChild) && Isomoprphic(first.RightChild, second.RightChild)) ||
                           (Isomoprphic(first.LeftChild, second.RightChild) && Isomoprphic(first.RightChild, second.LeftChild));
                }
                else
                {
                    return false;
                }
            }
            else if (first == null && second == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
