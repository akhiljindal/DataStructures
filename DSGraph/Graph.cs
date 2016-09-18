using System.Collections.Generic;
using System.Text;

namespace DSGraph
{
    public class Graph<T>
    {
        public bool IsDirected { get; private set; }
        public List<Edge<T>> AllEdges { get; private set; }
        public Dictionary<long, Vertex<T>> AllVertex { get; private set; }
        public int NumberOfVertices { get; private set; }
        public int NumberOfEdges { get; private set; }



        public Graph(bool isDirected = false)
        {
            IsDirected = isDirected;
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            AllEdges = new List<Edge<T>>();
            AllVertex = new Dictionary<long, Vertex<T>>();

        }
        public void AddEdge(long id1, long id2)
        {
            this.AddEdge(id1, id2, 0);
        }
        public void AddEdge(long id1, long id2, int weight)
        {
            Vertex<T> v1 = null;
            Vertex<T> v2 = null;

            if (!AllVertex.ContainsKey(id1))
            {
                v1 = new Vertex<T>(id1);
                AddVertex(v1);
            }
            else
            {
                v1 = AllVertex[id1];
            }

            if (!AllVertex.ContainsKey(id2))
            {
                v2 = new Vertex<T>(id2);
                AddVertex(v2);
            }
            else
            {
                v2 = AllVertex[id2];
            }

            Edge<T> e = new Edge<T>(v1, v2, IsDirected, weight);

            AllEdges.Add(e);

            v1.AddAdjacentVertex(e, v2);
            if (!IsDirected)
                v2.AddAdjacentVertex(e, v1);

            NumberOfEdges++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(Vertex<T> vertex)
        {
            if (AllVertex.ContainsKey(vertex.Id))
            {
                return;
            }

            AllVertex.Add(vertex.Id, vertex);

            foreach (var e in vertex.GetAdjEdges())
            {
                AllEdges.Add(e);
                NumberOfEdges++;
            }

            NumberOfVertices++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AddVertex(long id)
        {
            AddVertex(id, default(T));
        }

        public void AddVertex(long id, T data)
        {
            if (AllVertex.ContainsKey(id))
            {
                return;
            }

            Vertex<T> v = new Vertex<T>(id, data);

            AllVertex.Add(id, v);

            NumberOfVertices++;

        }

        public Vertex<T> GetVertex(long id)
        {
            return AllVertex[id];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var edge in AllEdges)
            {
                sb.Append(edge.V1.Data + " " + edge.V2.Data + " " + edge.Weight);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }

    public class Edge<T>
    {
        public bool IsDirected { get; private set; }

        public Vertex<T> V1 { get; set; }
        public Vertex<T> V2 { get; set; }

        public int Weight { get; private set; }

        public Edge(Vertex<T> v1, Vertex<T> v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public Edge(Vertex<T> v1, Vertex<T> v2, bool isDirected)
        {
            V1 = v1;
            V2 = v2;
            IsDirected = isDirected;
        }

        public Edge(Vertex<T> v1, Vertex<T> v2, bool isDirected, int w)
        {
            V1 = v1;
            V2 = v2;
            IsDirected = isDirected;
            Weight = w;
        }


        public override string ToString()
        {
            return "Edge [isDirected=" + IsDirected + ", vertex1=" + V1.Id
                   + ", vertex2=" + V2.Id + ", weight=" + Weight + "]";
        }

    }

    public class Vertex<T>
    {
        public long Id { get; private set; }
        public T Data { get; set; }

        private int inDegree;
        private int outDegree;

        private List<Edge<T>> AdjEdges;
        private List<Vertex<T>> AdjVertex;

        public Vertex(long id)
        {
            Id = id;
            AdjEdges = new List<Edge<T>>();
            AdjVertex = new List<Vertex<T>>();
            inDegree = 0;
            outDegree = 0;
        }

        public Vertex(long id, T data) : this(id)
        {
            Data = data;
        }

        public List<Edge<T>> GetAdjEdges()
        {
            return AdjEdges;
        }

        public List<Vertex<T>> GetAdjVertex()
        {
            return AdjVertex;
        }

        public int GetDegree()
        {
            return AdjEdges.Count;
        }

        public int GetInDegree()
        {
            return inDegree;
        }

        public int GetOutDegree()
        {
            return outDegree;
        }
        public void AddAdjacentVertex(Edge<T> e, Vertex<T> v)
        {
            AdjEdges.Add(e);
            AdjVertex.Add(v);
            outDegree++;
            v.inDegree++;

        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null)
                return false;

            if (obj.GetType() != this.GetType())
                return false;

            Vertex<T> other = (Vertex<T>)obj;
            if (other.Id != this.Id)
                return false;

            return true;


        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class EdgeComparer<T> : Comparer<Edge<T>>
    {
        public override int Compare(Edge<T> x, Edge<T> y)
        {
            if (x.Weight <= y.Weight)
                return -1;

            return 1;
        }
    }
}
