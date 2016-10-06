using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P237MinSpanningTree_Kruskal
{
    class Program
    {
        static void Main(string[] args)
        {
            int verNum = Convert.ToInt32(Console.ReadLine());
            int edgeNum = Convert.ToInt32(Console.ReadLine());
            var G = new Graph(verNum, edgeNum);
            for (int i = 0; i < edgeNum; i++)
            {
                string[] input = Console.ReadLine().Split();
                int v1 = Convert.ToInt32(input[0]) - 1;
                int v2 = Convert.ToInt32(input[1]) - 1;
                int weight = Convert.ToInt32(input[2]);
                G.Edges[i] = new Edge(G.Vertexes[v1], G.Vertexes[v2], weight);
            }
            Console.WriteLine(G.Kruskal());
        }
    }

    public class Graph
    {
        public int VerNum { get; }
        public int EdgeNum { get; }
        public Vertex[] Vertexes { get; }
        public Edge[] Edges { get; set; }

        public Graph(int verNum, int edgeNum)
        {
            VerNum = verNum;
            EdgeNum = edgeNum;
            Vertexes = new Vertex[VerNum];
            Edges = new Edge[EdgeNum];
            for (int i = 0; i < verNum; i++)
            {
                Vertexes[i] = new Vertex(i);
            }
        }

        public int Kruskal()
        {
            var heap = new BinaryHeap<Edge>(EdgeNum, new Edge() { Weight = int.MinValue });
            var set = new DisjSet(VerNum);
            foreach (var edge in Edges)
            {
                heap.Insert(edge);
            }

            int ans = 0;
            int knownEdges = 0;
            Edge E;
            int Vset, Uset;
            while (knownEdges != VerNum - 1)
            {
                try
                {
                    E = heap.RemoveMin();
                }
                catch (Exception exp)
                {
                    throw new Exception("Can't make MST. Is the Graph connected?", exp);
                }
                Vset = set.Find(E.From.ID);
                Uset = set.Find(E.To.ID);
                if (Vset != Uset)
                {
                    knownEdges++;
                    ans += E.Weight;
                    set.Union(Vset, Uset);
                }
            }
            return ans;
        }
    }

    public class Edge : IComparable
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Weight { get; set; }

        public int CompareTo(object obj)
        {
            var other = (Edge)obj;
            return Weight.CompareTo(other.Weight);
        }

        public Edge(Vertex from, Vertex to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class BinaryHeap<T>
        where T : IComparable
    {
        public int Count { get; set; }
        public int Size { get; set; }
        public T[] Elements { get; set; }
        public bool IsEmpty
        {
            get { return Count == 0; }
        }
        public bool IsFull
        {
            get { return Count == Size; }
        }

        public BinaryHeap(int size, T sentinel)
        {
            Size = size;
            Count = 0;
            Elements = new T[Size + 1];
            Elements[0] = sentinel;
        }

        /// <summary>
        /// 线性时间构造堆
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sentinel"></param>
        /// <param name="array"></param>
        public BinaryHeap(int size, T sentinel, T[] array) : this(size, sentinel)
        {
            if (size < array.Count())
            {
                throw new Exception("heap size is smaller than array.");
            }
            Count = array.Count();
            for (int i = 1; i < Count + 1; i++)
            {
                Elements[i] = array[i - 1];
            }
            for (int i = Count / 2; i > 0; i--)
            {
                PercolateDown(i);
            }
        }

        public T FindMin()
        {
            if (!IsEmpty)
            {
                return Elements[1];
            }
            else
            {
                throw new Exception("Heap is empty.");
            }
        }

        private void PercolateUp(int p)
        {
            T thisElement = Elements[p];
            while (Elements[p / 2].CompareTo(thisElement) > 0)
            {
                Elements[p] = Elements[p / 2];
                p = p / 2;
            }
            Elements[p] = thisElement;
        }

        private void PercolateDown(int p)
        {
            T thisElment = Elements[p];
            int minChild = 0;
            while (p * 2 <= Count)
            {
                minChild = p * 2;
                if (minChild != Count && Elements[minChild].CompareTo(Elements[minChild + 1]) > 0)
                {
                    minChild++;
                }
                if (Elements[minChild].CompareTo(thisElment) < 0)
                {
                    Elements[p] = Elements[minChild];
                    p = minChild;
                }
                else
                {
                    break;
                }
            }
            Elements[p] = thisElment;
        }

        public void Insert(T element)
        {
            if (IsFull)
            {
                throw new Exception("Heap is full.");
            }
            Count++;
            Elements[Count] = element;
            PercolateUp(Count);
        }

        public T RemoveMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }
            T minElement = Elements[1];
            Elements[1] = Elements[Count];
            Count--;
            PercolateDown(1);
            return minElement;
        }

    }

    public class Vertex
    {
        /// <summary>
        /// 此ID从0开始
        /// </summary>
        public int ID { get; set; }

        public Vertex(int id)
        {
            ID = id;
        }
    }

    public class DisjSet
    {
        public int[] Parent { get; set; }

        public DisjSet(int size)
        {
            Parent = new int[size];
            for (int i = 0; i < size; i++)
            {
                Parent[i] = 0;
            }
        }

        // 采用高度求并的方法
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX == rootY) return;

            if (Parent[rootX] < Parent[rootY])
            {
                Parent[rootY] = rootX;
            }
            else if (Parent[rootX] > Parent[rootY])
            {
                Parent[rootX] = rootY;
            }
            else
            {
                Parent[rootY] = rootX;
                Parent[rootX]--;
            }
        }

        // 包含路径压缩
        public int Find(int x)
        {
            if (Parent[x] <= 0)
            {
                return x;
            }
            else
            {
                return Parent[x] = Find(Parent[x]);
            }
        }

        public void Remove(int x)
        {
            if (Find(x) != x)
            {
                for (int i = 0; i < Parent.Count(); i++)
                    Find(i);
            }
            else
            {
                int newParent = 0;
                while (Find(newParent) != x || newParent == x)
                {
                    newParent++;
                }
                Parent[newParent] = -1;
                for (int i = newParent + 1; i < Parent.Count(); i++)
                {
                    if (Find(i) == x) Parent[i] = newParent;
                }
            }
            Parent[x] = 0;


        }
    }
}
