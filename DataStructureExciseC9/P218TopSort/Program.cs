using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P218TopSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int verNum = Convert.ToInt32(Console.ReadLine());
            var G = new Graph(verNum);
            int edgeNum = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < edgeNum; i++)
            {
                string[] pair = Console.ReadLine().Split();
                int v1 = Convert.ToInt32(pair[0]) - 1;
                int v2 = Convert.ToInt32(pair[1]) - 1;
                G.Vertexes[v1].Nexts.Add(G.Vertexes[v2]);
            }
            Queue<Vertex> topAns;
            if (G.TopSort(out topAns))
            {
                while (topAns.Count != 0)
                {
                    Console.WriteLine(topAns.Dequeue().ID + 1);
                }
            }
        }
    }

    public class Graph
    {
        public int VerNum { get; }
        public Vertex[] Vertexes { get; set; }

        public Graph(int verNum)
        {
            VerNum = verNum;
            Vertexes = new Vertex[VerNum];
            for (int i = 0; i < verNum; i++)
            {
                Vertexes[i] = new Vertex(i);
            }
        }

        public bool TopSort(out Queue<Vertex> ans)
        {
            ans = new Queue<Vertex>();
            var queue = new Queue<Vertex>();
            var indegree = new int[VerNum];
            for (int i = 0; i < VerNum; i++)
            {
                indegree[i] = 0;
            }

            foreach (var v in Vertexes)
            {
                foreach (var item in v.Nexts)
                {
                    indegree[item.ID]++;
                }
            }

            foreach (var v in Vertexes)
            {
                if (indegree[v.ID] == 0)
                {
                    queue.Enqueue(v);
                }
            }

            Vertex topV;
            while (queue.Count != 0)
            {
                topV = queue.Dequeue();
                foreach (var item in topV.Nexts)
                {
                    indegree[item.ID]--;
                    if (indegree[item.ID] == 0)
                    {
                        queue.Enqueue(item);
                    }
                }
                ans.Enqueue(topV);
            }

            if (ans.Count != VerNum)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class Vertex
    {
        /// <summary>
        /// 此ID从0开始
        /// </summary>
        public int ID { get; set; }
        public List<Vertex> Nexts { get; set; }

        public Vertex(int id)
        {
            ID = id;
            Nexts = new List<Vertex>();
        }
    }
}
