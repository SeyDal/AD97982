using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        

        public long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        {
            List<Node> graph = new List<Node>();
            for (int i= 0; i <= NodeCount; i++)
            {
                graph.Add(new Node());
                graph[i].vertex = i;
            }
            for (int i=0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][1]].edges.Add(graph[(int)edges[i][0]]);
            }
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(graph[(int)StartNode]);
            graph[(int)StartNode].vertex_distance = 0;
            graph[(int)StartNode].vertex_is_checked = true;
            while (Q.Count != 0)
            {
                Node vertex = Q.Dequeue();
                if (vertex.vertex == EndNode)
                {
                    break;
                }
                for (int i= 0; i< vertex .edges .Count; i++)
                {
                    if (vertex.edges[i].vertex_is_checked == false)
                    {
                        vertex.edges[i].vertex_is_checked = true;
                        vertex.edges[i].vertex_distance = vertex.vertex_distance + 1;
                        Q.Enqueue(vertex.edges[i]);
                    }
                }

            }
            
            return graph[(int)EndNode].vertex_distance;
        }
    }
    public class Node
    {
        public List<Node> edges = new List<Node>();
        public long vertex;
        public bool vertex_is_checked = false;
        public long vertex_distance = -1;
        public long part = -1;
       
    }
}
