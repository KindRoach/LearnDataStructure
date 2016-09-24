using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P105T4._39ChildSiblingsTree
{
    // 将 普通树 转换成 双亲/兄弟姊妹树
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new NTNode<int>();
            p1.Element = 1;
            p1.Childs = new List<NTNode<int>>();

            var p2 = new NTNode<int>();
            p2.Element = 2;
            p2.Childs = new List<NTNode<int>>();

            var p3 = new NTNode<int>();
            p3.Element = 3;
            p3.Childs = new List<NTNode<int>>();

            var p4 = new NTNode<int>();
            p4.Element = 4;
            p4.Childs = new List<NTNode<int>>();

            var p5 = new NTNode<int>();
            p5.Element = 5;
            p5.Childs = new List<NTNode<int>>();

            var p6 = new NTNode<int>();
            p6.Element = 6;
            p6.Childs = new List<NTNode<int>>();

            p3.Childs.Add(p5);
            p3.Childs.Add(p6);

            p1.Childs.Add(p2);
            p1.Childs.Add(p3);
            p1.Childs.Add(p4);

            var tree = Convert(p1);
            return;
        }

        static CSTNode<int> Convert(NTNode<int> node)
        {
            if (node == null)
            {
                return null;
            }

            var ans = new CSTNode<int>();
            ans.Element = node.Element;
            if (node.Childs.Count > 0)
            {
                ans.FirstChild = Convert(node.Childs[0]);
            }
            var p = ans.FirstChild;
            for (int i = 1; i < node.Childs.Count; i++)
            {
                p.NextSibling = Convert(node.Childs[i]);
                p = p.NextSibling;
            }

            return ans;
        }
    }
}
