using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P9T1._2WordsPuzzle
{
    class Program
    {
        public static readonly int[] xAdjust = { -1, -1, 0, 1, 1, 1, 0, -1 };
        public static readonly int[] yAdjust = { 0, -1, -1, -1, 0, 1, 1, 1 };

        static void Main(string[] args)
        {
            var chars = new char[4, 4];

            for (int i = 0; i < 4; i++)
            {
                string input = Console.ReadLine();
                var inputs = input.Split(' ');
                for (int j = 0; j < 4; j++)
                {
                    chars[i, j] = Convert.ToChar(inputs[j]);
                }
            }

            List<string> words = new List<string>();
            words.Add("this");
            words.Add("fat");
            words.Add("two");
            words.Add("that");

            FindWords(chars, words);
        }

        public static void FindWords(char[,] chars, List<string> words)
        {
            int xLength = chars.GetLength(1);
            int yLength = chars.GetLength(0);
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        StringBuilder word = new StringBuilder(xLength + yLength);
                        int x = i, y = j;
                        while (x >= 0 && x < xLength && y >= 0 && y < yLength)
                        {
                            word.Append(chars[y, x]);
                            x = x + xAdjust[k];
                            y = y + yAdjust[k];
                        }
                        foreach (var item in words)
                        {
                            if (word.ToString().StartsWith(item))
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }
                }
            }
        }
    }
}

