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

        }

        static List<Parenthes> PopulateList()
        {
            var list = new List<Parenthes>();
            list.Add(new Parenthes("(", ")"));
            list.Add(new Parenthes("[", "]"));
            list.Add(new Parenthes("{", "}"));
            list.Add(new Parenthes("/*", "*/"));
            list.Add(new Parenthes("begin", "end"));
            return list;
        }

        static List<Parenthes> PopulateRight()
        {
            var list = new List<Parenthes>();
            list.Add(new Parenthes(")", "("));
            list.Add(new Parenthes("]", "["));
            list.Add(new Parenthes("}", "{"));
            list.Add(new Parenthes("*/", "/*"));
            list.Add(new Parenthes("end", "begin"));
            return list;
        }
    }

    class Parenthes
    {
        public string Value { get; set; }
        public string Match { get; set; }

        public Parenthes(string value, string match)
        {
            Value = value;
            Match = match;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
