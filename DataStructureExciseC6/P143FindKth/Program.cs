﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P143FindKth
{
    class Program
    {
        // 找到第k小（升序排序的第k个）数
        static void Main(string[] args)
        {
            int size = 10000000;
            var nums = GetNums(size);
            var copyA = (int[])nums.Clone();
            var copyB = (int[])nums.Clone();
            var before = new DateTime();
            var after = new DateTime();

            before = DateTime.Now;
            Console.WriteLine(FindK_SQ(copyA, size / 2));
            after = DateTime.Now;
            Console.WriteLine(after - before);

            before = DateTime.Now;
            Console.WriteLine(FindK_Heap(copyA, size - (size / 2) + 1));
            after = DateTime.Now;
            Console.WriteLine(after - before);

        }

        static int[] GetNums(int size)
        {
            var nums = new int[size];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                nums[i] = random.Next();
            }
            return nums;
        }

        static int FindK_SQ(int[] A, int k)
        {
            Array.Sort(A);
            return A[k - 1];
        }

        // 大概是上面那个的十倍……说好的都是NlogN呢😐
        static int FindK_Heap(int[] A, int k)
        {
            var nums = new int[k];
            for (int i = 0; i < k; i++)
            {
                nums[i] = A[i];
            }
            var heap = new BinaryHeap<int>(k, int.MinValue, nums);
            for (int i = k; i < A.Count(); i++)
            {
                if (A[i] > heap.FindMin())
                {
                    heap.RemoveMin();
                    heap.Insert(A[i]);
                }
            }
            return heap.FindMin();
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

        public BinaryHeap(int size, T sentinel, T[] array)
        {
            if (size < array.Count())
            {
                throw new Exception("heap size is smaller than array.");
            }
            Size = size;
            Count = array.Count();
            Elements = new T[Size + 1];
            Elements[0] = sentinel;
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

}
