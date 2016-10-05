using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P224FindShortestPath_Dijkstra
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
                G.Vertexes[v1].Pathes.Add(new Path(G.Vertexes[v2], weight));
            }
            G.FSP_WithN(0);
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

        // 没有负权重
        public void FSP_NoN(int startV)
        {
            var Known = new bool[VerNum];
            for (int i = 0; i < VerNum; i++)
            {
                Known[i] = false;
            }
            foreach (var item in Vertexes)
            {
                item.Dis.Value = int.MaxValue;
            }

            Vertex V = Vertexes[startV];
            V.Dis.Value = 0;
            V.LastVertex = null;
            var queue = new PriorityQueue();
            queue.Insert(V);
            while (queue.Count != 0)
            {
                V = queue.RemoveMin();
                Known[V.ID] = true;
                foreach (var path in V.Pathes)
                {
                    if (!Known[path.Next.ID])
                    {
                        if (V.Dis.Value + path.Weight < path.Next.Dis.Value)
                        {
                            queue.SetDis(path.Next, V.Dis.Value + path.Weight);
                            path.Next.LastVertex = V;
                        }
                    }
                }
            }
            PrintPath(startV);
        }

        private void PrintPath(int startV)
        {
            Console.WriteLine($"Start vertex{startV + 1}");
            var stack = new Stack<int>();
            Vertex lastV;
            foreach (var v in Vertexes)
            {
                if (v.LastVertex == null)
                {
                    continue;
                }

                if (v.Dis.Value == int.MaxValue)
                {
                    Console.WriteLine($"{v.ID + 1} is unreachable.");
                    continue;
                }

                Console.Write($"Vertex{v.ID + 1} Weight:{v.Dis.Value} ");
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

        // 有负权重
        public void FSP_WithN(int startV)
        {
            var enqueue = new int[VerNum];

            foreach (var item in Vertexes)
            {
                item.Dis.Value = int.MaxValue;
            }

            Vertex V = Vertexes[startV];
            V.Dis.Value = 0;
            V.LastVertex = null;
            var queue = new Queue<Vertex>();
            queue.Enqueue(V);
            while (queue.Count != 0)
            {
                V = queue.Dequeue();
                enqueue[V.ID]++;
                if (enqueue[V.ID] > VerNum + 1)
                {
                    throw new Exception("Graph has a negative circle");
                }
                foreach (var path in V.Pathes)
                {
                    if (V.Dis.Value + path.Weight < path.Next.Dis.Value)
                    {
                        path.Next.Dis.Value = V.Dis.Value + path.Weight;
                        path.Next.LastVertex = V;
                        queue.Enqueue(path.Next);
                    }
                }
            }
            PrintPath(startV);
        }
    }

    // 一个用SortedDictionary模拟的优先队列
    public class PriorityQueue
    {
        public int Count { get { return Dictionary.Count; } }
        public SortedDictionary<Distance, Vertex> Dictionary { get; set; }

        public PriorityQueue()
        {
            Dictionary = new SortedDictionary<Distance, Vertex>();
        }

        public void Insert(Vertex v)
        {
            Dictionary.Add(v.Dis, v);
        }

        public Vertex RemoveMin()
        {
            if (Count == 0)
            {
                throw new Exception("No vertex in queue");
            }
            Vertex v = null;
            foreach (var item in Dictionary)
            {
                v = item.Value;
                break;
            }
            Dictionary.Remove(v.Dis);
            return v;
        }

        public void SetDis(Vertex v, int newDis)
        {
            if (Dictionary.Values.Contains(v))
            {
                Dictionary.Remove(v.Dis);
            }
            v.Dis.Value = newDis;
            Dictionary.Add(v.Dis, v);
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
        public Distance Dis { get; set; }

        public Vertex(int id)
        {
            ID = id;
            Pathes = new List<Path>();
            Dis = new Distance(ID);
        }
    }

    public class Distance : IComparable
    {
        public int Value { get; set; }
        public int ID { get; set; }

        public Distance(int id)
        {
            ID = id;
        }

        public int CompareTo(object obj)
        {
            var other = (Distance)obj;
            if (Value != other.Value)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return ID.CompareTo(other.ID);
            }
        }
    }

    public class Path
    {
        public Vertex Next { get; set; }
        public int Weight { get; set; }
        public Path(Vertex vertex, int weight)
        {
            Next = vertex;
            Weight = weight;
        }
    }
}
