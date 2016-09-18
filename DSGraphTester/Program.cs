using DSGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSGraphTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Topological Sort");
            Console.WriteLine("2. Unweighted Shortest Path");
            Console.WriteLine("3. MST - Kruskal");
            Console.WriteLine("4. MST - Prims");
            Console.WriteLine("5. Djkstra");
            Console.WriteLine("6. Detect Cycle - Disjoint Sets");
            Console.WriteLine("7. Detect Cycle - DFS");

            var key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    TopologicalSort();
                    break;
                case "2":
                    UnweightedShortestPath();
                    break;
                case "3":
                    KruskalMST();
                    break;
                case "4":
                    MSTPrims();
                    break;
                case "5":
                    Djkstras();
                    break;
                case "6":
                    DetectCycleDS();
                    break;
                case "7":
                    DetectCycleDFS();
                    break;
            }

            Console.ReadLine();
        }

        private static void DetectCycleDS()
        {
            Graph<Int32> graph = new Graph<Int32>(false);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 1);
            long v1;
            long v2;

            bool result = graph.DetectCycleinUndirectedGraphUsingDisjointSets(out v1, out v2);

            Console.WriteLine(result + " v1:" + v1 + "v2:" + v2);
        }

        private static void DetectCycleDFS()
        {
            Graph<Int32> graph = new Graph<Int32>(false);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 1);


            bool result = graph.HasCycleUsingDFS();

            Console.WriteLine(result);
        }

        private static void Djkstras()
        {
            Graph<Int32> graph = new Graph<Int32>(false);
            /*graph.AddEdge(0, 1, 4);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(2, 3, 7);
            graph.AddEdge(3, 4, 9);
            graph.AddEdge(4, 5, 10);
            graph.AddEdge(2, 5, 4);
            graph.AddEdge(1, 7, 11);
            graph.AddEdge(0, 7, 8);
            graph.AddEdge(2, 8, 2);
            graph.AddEdge(3, 5, 14);
            graph.AddEdge(5, 6, 2);
            graph.AddEdge(6, 8, 6);
            graph.AddEdge(6, 7, 1);
            graph.AddEdge(7, 8, 7);*/

            graph.AddEdge(1, 2, 5);
            graph.AddEdge(1, 7, 1);
            graph.AddEdge(7, 2, 0);

            graph.AddEdge(2, 3, 2);
            graph.AddEdge(1, 4, 9);
            graph.AddEdge(1, 5, 3);
            graph.AddEdge(5, 6, 2);
            graph.AddEdge(6, 4, 2);
            graph.AddEdge(3, 4, 3);


            Vertex<Int32> sourceVertex = graph.AllVertex.FirstOrDefault().Value;
            Dictionary<Vertex<Int32>, Vertex<Int32>> pathMap = new Dictionary<Vertex<int>, Vertex<int>>();
            var distance = graph.DjkstrasAlgo(sourceVertex, pathMap);

            Console.WriteLine("Source is :" + sourceVertex.Id);
            foreach (var d in distance)
            {
                Console.WriteLine(string.Format("Node:{0} :: {1}", d.Key.Id, d.Value));
            }


        }


        private static void MSTPrims()
        {
            Graph<Int32> graph = new Graph<Int32>(false);
            /* graph.AddEdge(0, 1, 4);
               graph.AddEdge(1, 2, 8);
               graph.AddEdge(2, 3, 7);
               graph.AddEdge(3, 4, 9);
               graph.AddEdge(4, 5, 10);
               graph.AddEdge(2, 5, 4);
               graph.AddEdge(1, 7, 11);
               graph.AddEdge(0, 7, 8);
               graph.AddEdge(2, 8, 2);
               graph.AddEdge(3, 5, 14);
               graph.AddEdge(5, 6, 2);
               graph.AddEdge(6, 8, 6);
               graph.AddEdge(6, 7, 1);
               graph.AddEdge(7, 8, 7);*/

            graph.AddEdge(1, 2, 3);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(3, 1, 1);
            graph.AddEdge(1, 4, 1);
            graph.AddEdge(2, 4, 3);
            graph.AddEdge(4, 5, 6);
            graph.AddEdge(5, 6, 2);
            graph.AddEdge(3, 5, 5);
            graph.AddEdge(3, 6, 4);


            List<Edge<Int32>> edges = graph.GetMSTUsingPrims();

            foreach (var e in edges)
            {
                Console.WriteLine(e.ToString());
            }

        }


        private static void KruskalMST()
        {
            Graph<Int32> graph = new Graph<Int32>(false);
            graph.AddEdge(1, 2, 4);
            graph.AddEdge(1, 3, 1);
            graph.AddEdge(2, 5, 1);
            graph.AddEdge(2, 6, 3);
            graph.AddEdge(2, 4, 2);
            graph.AddEdge(6, 5, 2);
            graph.AddEdge(6, 4, 3);
            graph.AddEdge(4, 7, 2);
            graph.AddEdge(3, 4, 5);
            graph.AddEdge(3, 7, 8);

            var edges = graph.GetMSTUsingKruskal();

            Console.WriteLine("MST");
            foreach (var e in edges)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static void UnweightedShortestPath()
        {
            var g = GetGraphofString();

            Dictionary<long, int> distance = new Dictionary<long, int>();
            var path = g.UnweightedShortestPath(distance, 1);
        }

        private static void TopologicalSort()
        {
            var g = GetGraphofString();
            var result = g.TopologicalSort();
            Console.WriteLine("Graph is-->\n" + g.ToString());

            Console.WriteLine("Top Sort:");
            foreach (var r in result)
            {
                Console.Write(r.Data + "->");
            }
        }

        private static Graph<string> GetGraphofString()
        {
            Graph<string> G = new Graph<string>(true);

            G.AddVertex(1, "A");
            G.AddVertex(2, "B");
            G.AddVertex(3, "C");
            G.AddVertex(4, "D");
            G.AddVertex(5, "E");
            G.AddVertex(6, "F");
            G.AddVertex(7, "G");
            G.AddVertex(8, "H");

            G.AddEdge(1, 3);
            G.AddEdge(2, 3);
            G.AddEdge(3, 5);
            G.AddEdge(2, 4);
            G.AddEdge(5, 6);
            G.AddEdge(5, 8);
            G.AddEdge(6, 7);
            G.AddEdge(4, 6);


            return G;
        }
    }
}
