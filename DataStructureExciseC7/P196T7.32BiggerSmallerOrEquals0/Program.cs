using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P196T7._32BiggerSmallerOrEquals0
{
    // 扩展书上的原题：整理数组，使得小于0的元素排在前面，等于0的元素排在中间，大于零的元素排在后面（非零元素不需要排序）
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 0, 0, 0, 0, 0 };
            Reorg(A);
            foreach (var item in A)
            {
                Console.WriteLine(item);
            }
        }

        static void Reorg(int[] A)
        {
            int n = A.Count();
            // 保证low左边为<0，high右边为>0
            int low = 0, index = 0, high = n - 1;
            while (index <= high)
            {
                if (A[index] > 0)
                {
                    Swap(ref A[index], ref A[high]);
                    // 因为无法确定high那边所指的是0还是其他什么的
                    // 必须下次循环在判断，所以没有index++
                    high--;
                }
                else if (A[index] < 0)
                {
                    Swap(ref A[index], ref A[low]);
                    // 因为开始时index=low
                    // 所以如果交换时index =low，那么负数换自己后index要++
                    // 如果index>low，那必然时因为low指向的是0，所以交换后可以不用判断，直接++
                    // 不能不写index++，因为一开始负数自己后index保持不动
                    // 还会继续将这个负数和low交换，而这时候low已经跑到右边去了
                    low++;
                    index++;
                }
                else index++;
            }
        }

        private static void Swap(ref int v1, ref int v2)
        {
            var temp = v1;
            v1 = v2;
            v2 = temp;
        }
    }
}
