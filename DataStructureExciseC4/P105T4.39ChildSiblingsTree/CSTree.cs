using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P105T4._39ChildSiblingsTree
{
    public class CSTNode<T>
        where T : IComparable
    {
        public T Element { get; set; }
        public CSTNode<T> FirstChild { get; set; }
        public CSTNode<T> NextSibling { get; set; }
    }

    public class NTNode<T>
        where T : IComparable
    {
        public T Element { get; set; }
        public List<NTNode<T>> Childs { get; set; }
    }
}