using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P159T6._9PrintSmallerThanX
{
    class Program
    {
        // 输出二叉堆中小于X的项
        static void Main(string[] args)
        {
            var nums = GetNums(0, 1000);
            var heap = new BinaryHeap<int>(1001, int.MinValue, nums);
            Console.WriteLine(heap.Elements[FindX(heap, 1, 209)]);
        }

        // 生成[lower,uper]的随机数组,不重复
        static int[] GetNums(int lower, int uper)
        {
            var nums = new int[uper - lower + 1];
            var random = new Random();
            for (int i = 0; i < nums.Count(); i++)
            {
                nums[i] = i + lower;
            }
            for (int i = 1; i < nums.Count(); i++)
            {
                Swap(ref nums[i], ref nums[random.Next(0, i + 1)]);
            }
            return nums;
        }

        private static void Swap(ref int v1, ref int v2)
        {
            var temp = v1;
            v1 = v2;
            v2 = temp;
        }

        static void Print(BinaryHeap<int> heap, int p, int x)
        {
            if (p > heap.Count)
            {
                return;
            }
            if (heap.Elements[p] < x)
            {
                Console.Write($"{heap.Elements[p]} ");
                Print(heap, p * 2, x);
                Print(heap, p * 2 + 1, x);
            }
            else
            {
                return;
            }
        }

        // return -1 for not found.
        // if find more than one x,return the x in left subHeap first
        static int FindX(BinaryHeap<int> heap, int p, int x)
        {
            if (p > heap.Count)
            {
                return -1;
            }
            if (heap.Elements[p] == x)
            {
                return p;
            }
            else if (heap.Elements[p] > x)
            {
                return -1;
            }
            else
            {
                int left = FindX(heap, p * 2, x);
                if (left > 0)
                {
                    return left;
                }
                else
                {
                    return FindX(heap, p * 2 + 1, x);
                }
            }
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
