using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P230CriticalPath
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
                int weight = Convert.ToInt32(input[2]);
                G.Vertexes[v2].InPathes.Add(new Path(G.Vertexes[v1], weight));
                G.Vertexes[v1].OutPathes.Add(new Path(G.Vertexes[v2], weight));
            }
            G.GetStack();
            foreach (var v in G.Vertexes)
            {
                foreach (var path in v.OutPathes)
                {
                    if (path.Weight != 0)
                        Console.WriteLine($"{v.ID + 1}->{path.Vertex.ID + 1}:{path.Stack}");
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

        public Queue<Vertex> TopSort()
        {
            int[] indegree = new int[VerNum];
            foreach (var v in Vertexes)
            {
                foreach (var path in v.OutPathes)
                {
                    indegree[path.Vertex.ID]++;
                }
            }

            var ans = new Queue<Vertex>();
            var queue = new Queue<Vertex>();
            for (int i = 0; i < VerNum; i++)
            {
                if (indegree[i] == 0)
                {
                    queue.Enqueue(Vertexes[i]);
                }
            }
            Vertex V;
            while (queue.Count != 0)
            {
                V = queue.Dequeue();
                foreach (var path in V.OutPathes)
                {
                    indegree[path.Vertex.ID]--;
                    if (indegree[path.Vertex.ID] == 0)
                    {
                        queue.Enqueue(path.Vertex);
                    }
                }
                ans.Enqueue(V);
            }
            if (ans.Count != VerNum)
            {
                throw new Exception("Graph has a circle");
            }
            return ans;
        }

        public void GetEC(Queue<Vertex> order)
        {
            foreach (var v in Vertexes)
            {
                v.EC = int.MinValue;
            }
            Vertex V;
            order.Peek().EC = 0;
            while (order.Count != 0)
            {
                V = order.Dequeue();
                foreach (var path in V.OutPathes)
                {
                    if (path.Vertex.EC < V.EC + path.Weight)
                    {
                        path.Vertex.EC = V.EC + path.Weight;
                    }
                }
            }
        }

        public void GetLC(Queue<Vertex> order)
        {
            foreach (var v in Vertexes)
            {
                v.LC = int.MaxValue;
            }
            Vertex V;
            order.Peek().LC = order.Peek().EC;
            while (order.Count != 0)
            {
                V = order.Dequeue();
                foreach (var path in V.InPathes)
                {
                    if (path.Vertex.LC > V.LC - path.Weight)
                    {
                        path.Vertex.LC = V.LC - path.Weight;
                    }
                }
            }
        }

        public void GetStack()
        {
            var positiveOrder = TopSort();
            var negativeOrder = new Queue<Vertex>(positiveOrder.Reverse());

            GetEC(positiveOrder);
            GetLC(negativeOrder);

            foreach (var v in Vertexes)
            {
                foreach (var path in v.OutPathes)
                {
                    path.Stack = path.Vertex.LC - v.EC - path.Weight;
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
        public List<Path> InPathes { get; set; }
        public List<Path> OutPathes { get; set; }
        /// <summary>
        /// 最早完成时间
        /// </summary>
        public int EC { get; set; }
        /// <summary>
        /// 最晚完成时间
        /// </summary>
        public int LC { get; set; }

        public Vertex(int id)
        {
            ID = id;
            InPathes = new List<Path>();
            OutPathes = new List<Path>();
        }
    }

    public class Path
    {
        public Vertex Vertex { get; set; }
        public int Weight { get; set; }
        /// <summary>
        /// 松弛时间
        /// </summary>
        public int Stack { get; set; }
        public Path(Vertex vertex, int weight)
        {
            Vertex = vertex;
            Weight = weight;
        }

        public Path()
        {
        }
    }
}
