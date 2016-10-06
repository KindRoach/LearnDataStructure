using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P242ArticulationPoint
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
                string[] input = Console.ReadLine().Split();
                int v1 = Convert.ToInt32(input[0]) - 1;
                int v2 = Convert.ToInt32(input[1]) - 1;
                G.Vertexes[v1].Pathes.Add(new Path(G.Vertexes[v2]));
                G.Vertexes[v2].Pathes.Add(new Path(G.Vertexes[v1]));
            }
            G.FindAP();
        }
    }
    public class Graph
    {
        public int VerNum { get; }
        public Vertex[] Vertexes { get; set; }
        public bool[] Visted { get; set; }
        public int[] Num { get; set; }
        public int[] Low { get; set; }
        public int Counter { get; set; }

        public Graph(int verNum)
        {
            VerNum = verNum;
            Vertexes = new Vertex[VerNum];
            for (int i = 0; i < verNum; i++)
            {
                Vertexes[i] = new Vertex(i);
                Visted = new bool[VerNum];
                Num = new int[VerNum];
                Low = new int[VerNum];
            }
        }

        public void FindAP()
        {
            for (int i = 0; i < VerNum; i++)
            {
                Visted[i] = false;
            }
            Counter = 0;
            var ans = new HashSet<int>();
            AssinNL(Vertexes[0], ans);
            ans.Remove(0);
            foreach (var item in ans)
            {
                Console.WriteLine($"{item + 1} is Articulation Point");
            }
            int child = 0;
            foreach (var path in Vertexes[0].Pathes)
            {
                if (path.Next.LastVertex == Vertexes[0]) child++;
            }
            if (child > 1)
            {
                Console.WriteLine($"{1} is Articulation Point");
            }
        }

        private void AssinNL(Vertex V, HashSet<int> ans)
        {
            Visted[V.ID] = true;
            Counter++;
            Num[V.ID] = Counter;
            Low[V.ID] = Counter;
            foreach (var path in V.Pathes)
            {
                if (!Visted[path.Next.ID])
                {
                    path.Next.LastVertex = V;
                    AssinNL(path.Next, ans);
                    if (Low[path.Next.ID] >= Num[V.ID])
                        ans.Add(V.ID);
                    else
                        Low[V.ID] = Math.Min(Low[V.ID], Low[path.Next.ID]);
                }
                else if (V.LastVertex != path.Next)
                {
                    Low[V.ID] = Math.Min(Low[V.ID], Num[path.Next.ID]);
                }
            }
        }
    }

    public class Vertex
    {
        /// <summary>
        /// 此ID从0开始
        /// </summary>
        public int ID { get; set; }
        public Vertex LastVertex { get; set; }
        public List<Path> Pathes { get; set; }

        public Vertex(int id)
        {
            ID = id;
            Pathes = new List<Path>();
        }
    }

    public class Path
    {
        public Vertex Next { get; set; }
        public Path(Vertex vertex)
        {
            Next = vertex;
        }
    }
}
