using System;
using System.Collections.Generic;

namespace DisjointSetsDS
{
    public class DisjointSets
    {
        private Dictionary<long, DSNode> map = new Dictionary<long, DSNode>();

        public void MakeSet(long data)
        {
            DSNode node = new DSNode(data);
            map.Add(data, node);
        }

        public DSNode FindSet(long data)
        {
            DSNode node = map[data];

            DSNode parent = node.Parent;

            if (node.Data == parent.Data)
                return parent;

            node.Parent = FindSet(node.Parent.Data);

            return node.Parent;
        }

        public bool Union(long d1, long d2)
        {
            DSNode n1 = map[d1];
            DSNode n2 = map[d2];

            DSNode p1 = FindSet(d1);
            DSNode p2 = FindSet(d2);

            if (p1.Data == p2.Data)
                return false;

            if (p1.Rank >= p2.Rank)
            {
                p1.Rank = (p1.Rank == p2.Rank) ? p1.Rank + 1 : p1.Rank;
                p2.Parent = p1;
            }
            else
            {
                p1.Parent = p2;
            }

            Console.WriteLine(string.Format("Parent of {0} is {1}", n1.Data, FindSet(n1.Data).Data));
            Console.WriteLine(string.Format("Parent of {0} is {1}", n2.Data, FindSet(n2.Data).Data));
            return true;

        }
    }

    public class DSNode
    {
        public long Data { get; set; }
        public DSNode Parent { get; set; }
        public int Rank { get; set; }

        public DSNode(long data, int rank = 0)
        {
            Data = data;
            Rank = rank;
            Parent = this;
        }
    }
}
