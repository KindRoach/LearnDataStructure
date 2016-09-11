using P62T3._3SwapListNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P63T3._10JosephusProblem
{
    // n个人（编号0~(n-1))，从0开始报数，报到(m-1)的退出
    // 剩下的人继续从0开始报数
    // 求胜利者的编号
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetVictor_n(5, 2));
            Console.WriteLine();
            Console.WriteLine(GetVictor_mn(5, 2));
        }


        // O(n)算法，利用递推公式
        // 只能推出最后胜利者
        static int GetVictor_n(int n, int m)
        {
            int victor = 0;
            for (int i = 2; i <= n; i++)
            {
                victor = (victor + m) % i;
            }
            return victor;
        }

        static int GetVictor_mn(int n, int m)
        {
            // 建立循环链表
            MyList<int> list = new MyList<int>();
            var p = list.Head;
            for (int i = 0; i < n; i++)
            {
                p = list.InsertAfter(p, i);
            }
            p.Next = list.Head.Next;

            int j = 0;
            p = list.Head.Next;
            while (list.Count > 1)
            {
                j++;
                if (j == m)
                {
                    var temp = p;
                    p = p.Next;
                    Console.Write($"{temp.Value} ");
                    list.Delete(temp);
                    j = 0;
                }
                else
                {
                    p = p.Next;
                }
            }
            Console.WriteLine();
            return p.Value;
        }
    }
}
