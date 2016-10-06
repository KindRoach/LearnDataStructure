using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P237MinSpanningTree_Prim
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
                G.Vertexes[v2].Pathes.Add(new Path(G.Vertexes[v1], weight));
            }
            Console.WriteLine(G.Prim(0));
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

        public int Prim(int startV)
        {
            int ans = 0;
            var Known = new bool[VerNum];
            var queue = new PriorityQueue();

            for (int i = 0; i < VerNum; i++)
            {
                Known[i] = false;
            }
            foreach (var item in Vertexes)
            {
                item.Dis.Value = int.MaxValue;
                queue.Insert(item);
            }

            Vertex V = Vertexes[startV];
            V.LastVertex = null;
            queue.SetDis(V, 0);
            while (queue.Count != 0)
            {
                V = queue.RemoveMin();
                Known[V.ID] = true;
                ans = ans + V.Dis.Value;
                foreach (var path in V.Pathes)
                {
                    if (!Known[path.Next.ID])
                    {
                        if (path.Weight < path.Next.Dis.Value)
                        {
                            queue.SetDis(path.Next, path.Weight);
                            path.Next.LastVertex = V;
                        }
                    }
                }
            }
            return ans;
            // PrintPath(startV);
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
