using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace P62T3._7.bPolynomialMultiplication
{
    // 多项式乘法，链表储存
    class Program
    {
        static void Main(string[] args)
        {
            var M = GetList1();
            var N = GetList2();
            var ans = Multiply(M, N);
            PrintList(ans);
        }


        static void PrintList(LinkedList<Factor> list)
        {
            var p = list.First;
            if (p.Value.Coeff > 0)
            {
                Console.Write(p.Value.ToString().Remove(0, 1));
                p = p.Next;
            }

            while (p != null)
            {
                Console.Write(p.Value);
                p = p.Next;
            }
            Console.WriteLine();
        }

        // -7x^7+x^4+1
        static LinkedList<Factor> GetList1()
        {
            var list = new LinkedList<Factor>();
            list.AddLast(new Factor(-7, 7));
            list.AddLast(new Factor(1, 4));
            list.AddLast(new Factor(1, 0));
            return list;
        }

        // 3x^3+1
        static LinkedList<Factor> GetList2()
        {
            var list = new LinkedList<Factor>();
            list.AddLast(new Factor(3, 3));
            list.AddLast(new Factor(1, 0));
            return list;
        }

        static LinkedList<Factor> Multiply(LinkedList<Factor> M, LinkedList<Factor> N)
        {
            // M整式乘以N的每项再求和，时间复杂度为：O(MN*M)，故M项数较小更好
            if (M.Count > N.Count)
            {
                var temp = M;
                M = N;
                N = temp;
            }

            // 乘法总结果
            var ans = new LinkedList<Factor>();
            // 每次乘单项的结果
            var T = new LinkedList<Factor>();

            var pn = N.First;
            while (pn != null)
            {
                Multiply(M, pn.Value, T);
                Add(ans, T);
                pn = pn.Next;
            }

            return ans;
        }

        private static void Add(LinkedList<Factor> ans, LinkedList<Factor> T)
        {
            var p1 = ans.First;
            var p2 = T.First;
            while (p1 != null && p2 != null)
            {
                if (p1.Value.Pow < p2.Value.Pow)
                {
                    ans.AddBefore(p1, p2.Value);
                    p2 = p2.Next;
                }
                else if (p1.Value.Pow > p2.Value.Pow)
                {
                    p1 = p1.Next;
                }
                else
                {
                    p1.Value.Coeff += p2.Value.Coeff;
                    p1 = p1.Next;
                    p2 = p2.Next;
                }
            }

            while (p1 != null)
            {
                ans.AddLast(p1.Value);
                p1 = p1.Next;
            }
            while (p2 != null)
            {
                ans.AddLast(p2.Value);
                p2 = p2.Next;
            }
        }

        static void Multiply(LinkedList<Factor> M, Factor x, LinkedList<Factor> T)
        {
            T.Clear();
            var pm = M.First;
            while (pm != null)
            {
                var factor = new Factor();
                factor.Pow = pm.Value.Pow + x.Pow;
                factor.Coeff = pm.Value.Coeff * x.Coeff;
                T.AddLast(factor);
                pm = pm.Next;
            }
        }

    }


    /// <summary>
    /// 单项式
    /// </summary>
    class Factor
    {
        /// <summary>
        /// 指数
        /// </summary>
        public int Pow { get; set; }

        /// <summary>
        /// 系数
        /// </summary>
        public int Coeff { get; set; }

        public Factor() { }

        public Factor(int coeff, int pow)
        {
            Coeff = coeff;
            Pow = pow;
        }

        public override string ToString()
        {
            string ans;
            if (Abs(Pow) > 1 && Abs(Coeff) != 1)
            {
                ans = $"{Coeff}x^{Pow}";
            }
            else if (Abs(Pow) > 1 && Abs(Coeff) == 1)
            {
                ans = $"x^{Pow}";
            }
            else if (Abs(Pow) == 1 && Abs(Coeff) != 1)
            {
                ans = $"{Coeff}x";
            }
            else if (Abs(Pow) == 1 && Abs(Coeff) == 1)
            {
                ans = "x";
            }
            else
            {
                ans = Coeff.ToString();
            }

            if (Coeff > 0)
            {
                ans = "+" + ans;
            }
            else if (Coeff < -0 && !ans.StartsWith("-"))
            {
                ans = "-" + ans;
            }
            return ans;
        }
    }
}
