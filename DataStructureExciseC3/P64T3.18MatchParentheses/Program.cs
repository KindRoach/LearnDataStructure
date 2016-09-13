using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P64T3._18MatchParentheses
{
    // 匹配括号、begin & end、/* & */、{ & }
    class Program
    {
        static void Main(string[] args)
        {
            var input = "{begin end}";
            Console.WriteLine(IsMatch(input));
        }

        static bool IsMatch(string input)
        {

            var parenthes = new List<string>(10);
            parenthes.Add("(");
            parenthes.Add(")");
            parenthes.Add("[");
            parenthes.Add("]");
            parenthes.Add("{");
            parenthes.Add("}");
            parenthes.Add("/*");
            parenthes.Add("*/");
            parenthes.Add("begin");
            parenthes.Add("end");

            var leftParenthes = new HashSet<string>();
            leftParenthes.Add("(");
            leftParenthes.Add("[");
            leftParenthes.Add("{");
            leftParenthes.Add("/*");
            leftParenthes.Add("begin");

            var match = new Dictionary<string, string>(5);
            match.Add(")", "(");
            match.Add("]", "[");
            match.Add("}", "{");
            match.Add("*/", "/*");
            match.Add("end", "begin");

            var locations = new List<Tuple<string, int>>();
            foreach (var item in parenthes)
            {
                GetLocation(item, input, locations);
            }

            var order = locations.OrderBy(x => x.Item2).Select(x => x.Item1);

            var stack = new Stack<string>();

            foreach (var item in order)
            {
                if (leftParenthes.Contains(item))
                {
                    stack.Push(item);
                }
                else
                {
                    if (stack.Peek() == match[item])
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (stack.Count > 0)
            {
                return false;
            }

            return true;
        }

        private static void GetLocation(
            string target, string input, List<Tuple<string, int>> locations)
        {
            int index = input.IndexOf(target);
            while (index >= 0)
            {
                locations.Add(new Tuple<string, int>(target, index));
                index = input.IndexOf(target, index + target.Length);
            }
        }
    }
}
