using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P212T8._10LCA_OffLine_Trajan
{
    // 详细问题描述：
    // http://blog.csdn.net/u011721440/article/details/38542147
    // 详解：
    // http://www.mamicode.com/info-detail-1067269.html

    public class Ans
    {
        public int Value { get; set; }
    }

    class Program
    {
        // 顶点编号从1开始
		static List<int>[] Childs { get; set; }
        static List<int>[] Relative { get; set; }

        // 储存答案顺序的列表
        static List<Ans> Anses { get; set; }

        // 由查询到答案的字典
        // 比如（1，4）=> 1004 & 4001
        static Dictionary<int, Ans> Queses { get; set; }

        static bool[] Visted { get; set; }
        static int[] Parent { get; set; }

        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            Childs = new List<int>[n + 1];
            Relative = new List<int>[n + 1];
            Visted = new bool[n + 1];
            Parent = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                Childs[i] = new List<int>();
                Relative[i] = new List<int>();
                Visted[i] = false;
                Parent[i] = i;
            }

            string[] pair;
            int parent, child, node1, node2;
            for (int i = 0; i < n - 1; i++)
            {
                pair = Console.ReadLine().Split();
                parent = Convert.ToInt32(pair[0]);
                child = Convert.ToInt32(pair[1]);
                Childs[parent].Add(child);
            }

            int quesN = Convert.ToInt32(Console.ReadLine());
            Queses = new Dictionary<int, Ans>(quesN * 2);
            Anses = new List<Ans>(quesN);
            for (int i = 0; i < quesN; i++)
            {
                pair = Console.ReadLine().Split();
                node1 = Convert.ToInt32(pair[0]);
                node2 = Convert.ToInt32(pair[1]);
                Relative[node1].Add(node2);
                Relative[node2].Add(node1);
                var ans = new Ans();
                Anses.Add(ans);
                Queses.Add(node1 * 1000 + node2, ans);
                Queses.Add(node2 * 1000 + node1, ans);
            }

            for (int i = 1; i < n + 1; i++)
            {
                if (!Visted[i]) Trajan(i);
            }

            foreach (var item in Anses)
            {
                Console.WriteLine(item.Value);
            }
        }

        private static void Trajan(int x)
        {
            foreach (var item in Childs[x])
            {
                Trajan(item);
                Parent[item] = x;
            }

            int ans;
            foreach (var item in Relative[x])
            {
                if (Visted[item])
                {
                    ans = Find(item);
                    Queses[x * 1000 + item].Value = ans;
                }
            }

            Visted[x] = true;
        }

        private static int Find(int item)
        {
            if (Parent[item] != item)
            {
                return Find(Parent[item]);
            }
            else
            {
                return item;
            }
        }
    }

}
