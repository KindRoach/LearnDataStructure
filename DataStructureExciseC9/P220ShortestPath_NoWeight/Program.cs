using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P220ShortestPath_NoWeight
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
                G.Vertexes[v1].Pathes.Add(new Path(G.Vertexes[v2]));
            }
            G.FindShortestPath(0);
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

        public void FindShortestPath(int startV)
        {
            foreach (var item in Vertexes)
            {
                item.Distance = int.MaxValue;
            }

            Vertex V = Vertexes[startV];
            V.Distance = 0;
            V.LastVertex = null;
            var queue = new Queue<Vertex>();
            queue.Enqueue(V);
            while (queue.Count != 0)
            {
                V = queue.Dequeue();
                foreach (var path in V.Pathes)
                {
                    if (path.Next.Distance == int.MaxValue)
                    {
                        path.Next.Distance = V.Distance + 1;
                        path.Next.LastVertex = V;
                        queue.Enqueue(path.Next);
                    }
                }
            }
            PrintPath(startV);
        }

        private void PrintPath(int startV)
        {
            Console.WriteLine($"Start vertex:{startV + 1}");
            var stack = new Stack<int>();
            Vertex lastV;
            foreach (var v in Vertexes)
            {
                if (v.LastVertex == null)
                {
                    continue;
                }

                if (v.Distance == int.MaxValue)
                {
                    Console.WriteLine($"{v.ID + 1} is unreachable.");
                    continue;
                }

                Console.Write($"Weight:{v.Distance} ");
                lastV = v.LastVertex;
                while (lastV != null)
                {
                    stack.Push(lastV.ID);
                    lastV = lastV.LastVertex;
                }
                Console.Write("Path:");
                while (stack.Count != 0)
                {
                    Console.Write($"{stack.Pop() + 1}->");
                }
                Console.WriteLine(v.ID + 1);

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
        public int Distance { get; set; }

        public Vertex(int id)
        {
            ID = id;
            Pathes = new List<Path>();
        }
    }

    public class Path
    {
        public Vertex Next { get; set; }
        public int Weight { get; set; }
        public Path(Vertex vertex)
        {
            Next = vertex;
        }
    }
}