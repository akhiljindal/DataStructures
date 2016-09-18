using DisjointSetsDS;
using HeapDS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSGraph
{
    public static class GraphExtensions
    {
        public static Stack<Vertex<T>> TopologicalSort<T>(this Graph<T> g)
        {
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            Stack<Vertex<T>> result = new Stack<Vertex<T>>();


            foreach (var currentVertex in g.AllVertex.Values)
            {
                if (visited.Contains(currentVertex))
                    continue;

                TopSort(currentVertex, visited, result);


            }


            return result;
        }

        public static Dictionary<long, Vertex<T>> UnweightedShortestPath<T>(this Graph<T> g,
            Dictionary<long, int> distance, long sourceId)
        {
            var source = g.GetVertex(sourceId);
            Dictionary<long, Vertex<T>> path = new Dictionary<long, Vertex<T>>();
            //Set Distance to -1 except source
            foreach (var v in g.AllVertex.Keys)
            {
                if (distance.ContainsKey(v))
                    continue;

                distance.Add(v, -1);
            }
            distance[sourceId] = 0;

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();

            queue.Enqueue(source);

            path.Add(sourceId, null);

            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                foreach (var v in currentVertex.GetAdjVertex())
                {
                    if (distance[v.Id] == -1)
                    {
                        distance[v.Id] = distance[currentVertex.Id] + 1;
                        path.Add(v.Id, currentVertex);
                        queue.Enqueue(v);
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// 
        /**https://en.wikipedia.org/wiki/Kruskal%27s_algorithm
         * KRUSKAL(G):
                1 A = ∅
                2 foreach v ∈ G.V:
                3    MAKE-SET(v)
                4 foreach (u, v) in G.E ordered by weight(u, v), increasing:
                5    if FIND-SET(u) ≠ FIND-SET(v):
                6       A = A ∪ {(u, v)}
                7       UNION(u, v)
                8 return A
         * **/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static List<Edge<T>> GetMSTUsingKruskal<T>(this Graph<T> g)
        {
            EdgeComparer<T> comparer = new EdgeComparer<T>();
            List<Edge<T>> result = new List<Edge<T>>();

            //First sort the edges
            g.AllEdges.Sort(comparer);

            Console.WriteLine("Edes in Sorted Order");
            foreach (var e in g.AllEdges)
            {
                Console.WriteLine(e.ToString());
            }


            DisjointSets disjointSet = new DisjointSets();
            //Create Set for each vertex
            foreach (var v in g.AllVertex.Values)
            {
                disjointSet.MakeSet(v.Id);
            }

            //For each edge, check if vertex is already in SET.
            //1. If yes, ignore
            //2. If not, Union and add it to result of edge
            foreach (var e in g.AllEdges)
            {
                var n1 = disjointSet.FindSet(e.V1.Id);
                var n2 = disjointSet.FindSet(e.V2.Id);

                if (n1 == n2)
                {
                    continue;
                }
                else
                {
                    result.Add(e);
                    disjointSet.Union(e.V1.Id, e.V2.Id);
                }

            }

            return result;
        }

        public static List<Edge<T>> GetMSTUsingPrims<T>(this Graph<T> g)
        {
            BinaryMinHeap<Vertex<T>> minHeap = new BinaryMinHeap<Vertex<T>>();
            //Set all nodes in hash map to infinity

            Dictionary<Vertex<T>, Edge<T>> vertexToEdgeMap = new Dictionary<Vertex<T>, Edge<T>>();

            //Final result
            var result = new List<Edge<T>>();

            //insert all vertices with infinite value initially.
            foreach (var v in g.AllVertex.Values)
            {
                minHeap.AddNode(Int32.MaxValue, v);
            }

            //Start from Random Vertex and decrease min heap to 0
            Vertex<T> startVertex = g.AllVertex.FirstOrDefault().Value;
            minHeap.Decrease(startVertex, 0);

            //iterate till heap + map has elements in it
            while (!minHeap.IsEmpty())
            {

                //Extract the min vertex from heap
                var minVertex = minHeap.extractMin().Data;

                //get the corresponding edge for this vertex if present and add it to final result.
                //This edge wont be present for first vertex.
                Edge<T> spanningTreeEdge = vertexToEdgeMap.ContainsKey(minVertex) ? vertexToEdgeMap[minVertex] : null;

                if (spanningTreeEdge != null)
                {
                    result.Add(spanningTreeEdge);
                }

                //Iterate through all the edges for the current minVertex
                foreach (var edge in minVertex.GetAdjEdges())
                {
                    Vertex<T> otherVertex = GetVertexForEdge(minVertex, edge);

                    //Check if the other vertex already exists in the map and weight attached is greater than weight of the edge. If yes, replace
                    if (minHeap.ContainsData(otherVertex) && minHeap.GetWeight(otherVertex) > edge.Weight)
                    {
                        minHeap.Decrease(otherVertex, edge.Weight);
                        if (vertexToEdgeMap.ContainsKey(otherVertex))
                        {
                            vertexToEdgeMap[otherVertex] = edge;
                        }
                        else
                        {
                            vertexToEdgeMap.Add(otherVertex, edge);
                        }
                    }
                }
            }

            return result;
        }

        public static Dictionary<Vertex<T>, Int32> DjkstrasAlgo<T>(this Graph<T> g, Vertex<T> source, Dictionary<Vertex<T>, Vertex<T>> pathMap)
        {
            BinaryMinHeap<Vertex<T>> minHeap = new BinaryMinHeap<Vertex<T>>();

            Dictionary<Vertex<T>, Int32> distanceMap = new Dictionary<Vertex<T>, int>();

            //Dictionary<Vertex<T>, Vertex<T>> pathMap = new Dictionary<Vertex<T>, Vertex<T>>();

            //Set all weights to infinity in minHeap
            foreach (var v in g.AllVertex.Values)
            {
                minHeap.AddNode(Int32.MaxValue, v);
            }

            //Decrease the weight of source to 0
            minHeap.Decrease(source, 0);


            pathMap.Add(source, null);

            while (!minHeap.IsEmpty())
            {
                //Extract the min
                int weight = minHeap.MinNode().Weight;
                var currentVertex = minHeap.extractMin().Data;
                distanceMap.AddOrUpdateDictionary(currentVertex, weight);

                foreach (var edge in currentVertex.GetAdjEdges())
                {
                    var otherVertex = GetVertexForEdge(currentVertex, edge);
                    if (minHeap.ContainsData(otherVertex) && minHeap.GetWeight(otherVertex) > (edge.Weight + weight))
                    {
                        minHeap.Decrease(otherVertex, (edge.Weight + weight));
                        pathMap.AddOrUpdateDictionary(otherVertex, currentVertex);
                    }
                }
            }

            return distanceMap;

        }

        public static bool DetectCycleinUndirectedGraphUsingDisjointSets<T>(this Graph<T> g, out long v1, out long v2)
        {
            DisjointSets ds = new DisjointSets();
            v1 = Int32.MaxValue;
            v2 = Int32.MaxValue;
            //Step 1: Make a set for all nodes in graph
            foreach (var v in g.AllVertex.Values)
            {
                ds.MakeSet(v.Id);
            }

            //For all edges, findset each vertex. 
            // If the findset does not match, do union else you have found a cycle
            foreach (var edge in g.AllEdges)
            {
                var n1 = ds.FindSet(edge.V1.Id);
                var n2 = ds.FindSet(edge.V2.Id);

                if (n1 == n2)
                {
                    v1 = edge.V1.Id;
                    v2 = edge.V2.Id;
                    return true;
                }

                ds.Union(edge.V1.Id, edge.V2.Id);

            }

            return false;
        }

        public static bool HasCycleUsingDFS<T>(this Graph<T> g)
        {
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();

            foreach (var v in g.AllVertex.Values)
            {
                if (visited.Contains(v))
                    continue;

                bool flag = HasCycleDFsUtil(v, visited, null);
                if (flag == true)
                    return true;
            }

            return false;


        }

        private static bool HasCycleDFsUtil<T>(Vertex<T> vertex, HashSet<Vertex<T>> visited, Vertex<T> parentVertex)
        {
            visited.Add(vertex);
            foreach (var adjVertex in vertex.GetAdjVertex())
            {
                if (adjVertex.Equals(parentVertex))
                    continue;

                if (visited.Contains(adjVertex))
                    return true;

                bool hasCycle = HasCycleDFsUtil<T>(adjVertex, visited, vertex);

                if (hasCycle == true)
                    return true;
            }

            return false;
        }

        private static void AddOrUpdateDictionary<T, T1>(this Dictionary<T, T1> d, T key, T1 value)
        {
            if (d.ContainsKey(key))
            {
                d[key] = value;
            }
            else
            {
                d.Add(key, value);
            }

        }
        private static Vertex<T> GetVertexForEdge<T>(Vertex<T> v, Edge<T> e)
        {
            return e.V1.Equals(v) ? e.V2 : e.V1;
        }

        private static void TopSort<T>(Vertex<T> currentVertex, HashSet<Vertex<T>> visited, Stack<Vertex<T>> result)
        {
            visited.Add(currentVertex);

            foreach (var v in currentVertex.GetAdjVertex())
            {
                if (visited.Contains(v))
                    continue;

                TopSort(v, visited, result);
            }

            result.Push(currentVertex);
        }
    }
}
