using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P201DisjointSet
{
    class Program
    {
        static void Main(string[] args)
        {
            var dis = new DisjSet(100);
            dis.Union(1, 2);
            dis.Union(3, 4);
            dis.Union(1, 4);
            dis.Remove(1);
            Console.WriteLine(dis.Find(1) == dis.Find(4));
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
