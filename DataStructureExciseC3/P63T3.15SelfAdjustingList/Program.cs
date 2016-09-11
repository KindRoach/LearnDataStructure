using P62T3._3SwapListNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P63T3._15SelfAdjustingList
{
    // 自调整表，当一个元素被Find，则被移至表头
    class Program
    {
        static void Main(string[] args)
        {
            var list = GetList();
            list.FindFirst(6);
            list.Print();
            list.FindFirst(5);
            list.Print();
        }


        static MyList<int> GetList()
        {
            MyList<int> list = new MyList<int>();
            var p = list.Head;
            for (int i = 0; i < 5; i++)
            {
                p = list.InsertAfter(p, i + 1);
            }
            return list;
        }


    }
}
