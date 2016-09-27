using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAdressingHash
{
    // 散列（开放地址法）
    class Program
    {
        static void Main(string[] args)
        {
            int size = 6;
            var hash = new HashTable<int>(size, x => x % HashTable<int>.NextPrime(size));
            hash.Insert(3);
            hash.Insert(3);
            hash.Insert(10);
            hash.Insert(17);
            Console.WriteLine(hash.Find(17).Key);
            hash.Delete(17);
            Console.WriteLine(hash.Find(17) == null);
            return;


        }
    }

    public class HashNode<T>
    {
        public enum NodeState
        {
            Empty,
            Used,
            Deleted
        }
        public T Key { get; set; }
        public NodeState State { get; set; }
    }


    public class HashTable<T>
    {
        public int TableSize { get; set; }
        public int Count { get; set; }
        public List<HashNode<T>> Nodes { get; set; }
        public Func<T, int> Hash { get; }

        public static int NextPrime(int x)
        {
            while (!IsPrime(x))
            {
                x++;
            }
            return x;
        }

        private static bool IsPrime(int x)
        {
            if (x <= 0)
            {
                throw new ArgumentException("x must > 0");
            }

            if (x <= 2)
            {
                return true;
            }

            for (int i = 2; i < Math.Sqrt(x) + 1; i++)
            {
                if (x % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 探测函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int F(T x, int i)
        {
            return i;
        }

        public HashTable(int tableSize, Func<T, int> hashFunction)
        {
            TableSize = NextPrime(tableSize);
            Nodes = new List<HashNode<T>>(TableSize);
            for (int i = 0; i < TableSize; i++)
            {
                Nodes.Add(new HashNode<T>() { State = HashNode<T>.NodeState.Empty });
            }
            Hash = hashFunction;
            Count = 0;
        }

        public HashNode<T> Find(T key)
        {
            Count++;
            int hashcode = Hash(key);
            var p = Nodes[hashcode];
            int i = 1;
            while (!p.Key.Equals(key) && p.State != HashNode<T>.NodeState.Empty)
            {
                hashcode = (hashcode + F(key, i)) % TableSize;
                p = Nodes[hashcode];
                i++;
            }
            if (p.State != HashNode<T>.NodeState.Used)
            {
                return null;
            }
            return p;
        }

        public void Delete(T key)
        {
            var p = Find(key);
            if (p == null)
            {
                return;
            }
            p.State = HashNode<T>.NodeState.Deleted;
        }

        public void Insert(T key)
        {
            if (Find(key) != null)
            {
                return;
            }
            int hashcode = Hash(key);
            var p = Nodes[hashcode];
            int i = 1;
            while (p.State == HashNode<T>.NodeState.Used)
            {
                hashcode = (hashcode + F(key, i)) % TableSize;
                p = Nodes[hashcode];
                i++;
            }
            p.Key = key;
            p.State = HashNode<T>.NodeState.Used;
            return;
        }
    }


}
