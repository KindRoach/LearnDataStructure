﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeparateChainingHash
{
    // 散列（分离链接法）
    class Program
    {
        static void Main(string[] args)
        {
            int size = 6;
            var hash = new HashTable<int>(size);
            hash.Insert(45);
            hash.Insert(3);
            hash.Insert(13);
            hash.Insert(10);
            hash.Insert(17);
            hash.Insert(12);
            hash.Insert(9);
            Console.WriteLine(hash.Find(3).Key);
            hash.Delete(3);
            Console.WriteLine(hash.Find(3) == null);
            return;

        }
    }

    public class HashNode<T>
    {
        public T Key { get; set; }
        public HashNode<T> Next { get; set; }

        public HashNode(T key)
        {
            Key = key;
        }
    }

    public class HashHead<T>
    {
        public HashNode<T> Next { get; set; }
    }

    public class HashTable<T>
    {
        public int TableSize { get; set; }
        public List<HashHead<T>> Heads { get; set; }
        public int Count { get; set; }

        private int Hash(T key)
        {
            return key.GetHashCode() % TableSize;
        }

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

        public HashTable(int tableSize)
        {
            TableSize = NextPrime(tableSize);
            Heads = new List<SeparateChainingHash.HashHead<T>>(TableSize);
            for (int i = 0; i < TableSize; i++)
            {
                Heads.Add(new HashHead<T>());
            }
            Count = 0;
        }

        /// <summary>
        /// null for key not found
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HashNode<T> Find(T key)
        {
            int hashcode = Hash(key);
            var p = Heads[hashcode].Next;
            while (p != null && !p.Key.Equals(key))
            {
                p = p.Next;
            }
            return p;
        }

        /// <summary>
        /// return null for key not found, pre is null for the first node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pre"></param>
        /// <returns></returns>
        public HashNode<T> FindWithPre(T key, out HashNode<T> pre)
        {
            int hashcode = Hash(key);
            var p = Heads[hashcode].Next;
            pre = null;
            while (p != null && !p.Key.Equals(key))
            {
                pre = p;
                p = p.Next;
            }
            return p;
        }

        /// <summary>
        /// no change if key not found
        /// </summary>
        /// <param name="key"></param>
        public void Delete(T key)
        {
            HashNode<T> pre;
            var p = FindWithPre(key, out pre);
            if (p == null)
            {
                return;
            }

            // p is not the first node
            if (pre != null)
            {
                pre.Next = p.Next;
            }
            else
            {
                int hashcode = Hash(p.Key);
                Heads[hashcode].Next = p.Next;
            }
            Count--;
            if (Count < TableSize * 3 / 4)
            {
                Rehash(false);
            }
            return;
        }

        /// <summary>
        /// return true for success , return false if key already existed and this method make no change
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Insert(T key)
        {
            if (Find(key) != null)
            {
                return false;
            }

            int hashcode = Hash(key);
            var node = new HashNode<T>(key);
            var head = Heads[hashcode];
            node.Next = head.Next;
            head.Next = node;
            Count++;
            if (Count > TableSize * 2)
            {
                Rehash(true);
            }
            return true;
        }

        private void Rehash(bool IsBigger)
        {
            if (IsBigger)
            {
                TableSize = NextPrime(TableSize * 2);
            }
            else
            {
                TableSize = NextPrime(TableSize / 2);
            }
            var newList = new List<HashHead<T>>(TableSize);
            for (int i = 0; i < TableSize; i++)
            {
                newList.Add(new HashHead<T>());
            }
            foreach (var item in Heads)
            {
                var p = item.Next;
                while (p != null)
                {
                    var hashcode = Hash(p.Key);
                    var node = new HashNode<T>(p.Key);
                    node.Next = newList[hashcode].Next;
                    newList[hashcode].Next = node;
                    p = p.Next;
                }
            }
            Heads = newList;
        }
    }
}
