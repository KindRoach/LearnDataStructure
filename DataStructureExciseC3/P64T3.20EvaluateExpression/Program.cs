using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P64T3._20EvaluateExpression
{
    // 中缀表达式求值，支持+、-、*、/、（、）、^
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Evaluate("2+3^2^(3-1)"));
        }

        static int Evaluate(string expression)
        {
            
            var legalOperator = new HashSet<char>()
                { '+', '-', '*', '/', '(', ')', '^' };
            var legalNumber = new HashSet<char>()
                { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var numberStack = new Stack<int>();
            var operStack = new Stack<char>();

            int number = 0;
            for (int index = 0; index < expression.Length; index++)
            {
                var indexChar = expression[index];

                if (legalNumber.Contains(indexChar))
                {
                    number = number * 10 + indexChar - '0';
                }
                else if (legalOperator.Contains(indexChar))
                {
                    if (legalNumber.Contains(expression[index - 1]))
                    {
                        numberStack.Push(number);
                        number = 0;
                    }

                    if (indexChar == '(')
                    {
                        operStack.Push(indexChar);
                        continue;
                    }

                    while (operStack.Count != 0 && !CanPush(indexChar, operStack.Peek()))
                    {
                        int a = numberStack.Pop();
                        int b = numberStack.Pop();
                        char c = operStack.Pop();
                        numberStack.Push(Caculate(b, a, c));
                    }

                    if (indexChar == ')')
                    {
                        operStack.Pop();
                        continue;
                    }

                    operStack.Push(indexChar);
                }
                else
                {
                    throw new ArgumentException("illegal expression.");
                }
            }
            if (legalNumber.Contains(expression[expression.Length - 1]))
            {
                numberStack.Push(number);
            }

            while (operStack.Count > 0)
            {
                int a = numberStack.Pop();
                int b = numberStack.Pop();
                char c = operStack.Pop();
                numberStack.Push(Caculate(b, a, c));
            }

            return numberStack.Peek();
        }

        static int Caculate(int First, int Second, char Operator)
        {
            switch (Operator)
            {
                case '+':
                    return First + Second;
                case '-':
                    return First - Second;
                case '*':
                    return First * Second;
                case '/':
                    return First / Second;
                case '^':
                    return (int)Math.Pow(First, Second);
                default:
                    throw new ArgumentException("nuknown operator");
            }
        }

        static bool CanPush(char indexChar, char stackPeek)
        {
            var classes = new Dictionary<char, int>();
            classes.Add('+', 1);
            classes.Add('-', 1);
            classes.Add('*', 2);
            classes.Add('/', 2);
            classes.Add('(', 0);
            classes.Add(')', 1);
            classes.Add('^', 3);

            if (indexChar == '^' && stackPeek == '^')
            {
                return true;
            }

            return classes[indexChar] > classes[stackPeek];
        }
    }
}
