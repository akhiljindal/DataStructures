using DisjointSetsDS;
using System;

namespace DisjointSetsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            DisjointSets ds = new DisjointSets();
            ds.MakeSet(1);
            ds.MakeSet(2);
            ds.MakeSet(3);
            ds.MakeSet(4);
            ds.MakeSet(5);
            ds.MakeSet(6);
            ds.MakeSet(7);


            ds.Union(1, 2);
            ds.Union(2, 3);
            ds.Union(4, 5);
            ds.Union(6, 7);
            ds.Union(5, 6);
            ds.Union(3, 7);

            Console.WriteLine(ds.FindSet(1).Data);
            Console.WriteLine(ds.FindSet(2).Data);
            Console.WriteLine(ds.FindSet(3).Data);
            Console.WriteLine(ds.FindSet(4).Data);
            Console.WriteLine(ds.FindSet(5).Data);
            Console.WriteLine(ds.FindSet(6).Data);
            Console.WriteLine(ds.FindSet(7).Data);
        }
    }
}
