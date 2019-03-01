using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long NodeCount, long[][] edges)
        {
            List<Node> graph = new List<Node>();
            for (int i = 0; i <= NodeCount; i++)
            {
                graph.Add(new Node());
                graph[i].vertex = i;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][1]].edges.Add(graph[(int)edges[i][0]]);
            }
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(graph[1]);
            graph[1].part = 0;
            graph[1].vertex_is_checked = true;
            while (Q.Count != 0)
            {
                Node vertex = Q.Dequeue();
                
                for (int i = 0; i < vertex.edges.Count; i++)
                {
                    if (vertex.edges[i].vertex_is_checked == false)
                    {
                        vertex.edges[i].vertex_is_checked = true;
                        if (vertex.part == 0)
                            vertex.edges[i].part = 1;
                        else
                            vertex.edges[i].part = 0;
                        Q.Enqueue(vertex.edges[i]);
                    }
                }
            }
            for (int i = 1; i <= NodeCount; i++)
            {
                for (int j = 0; j < graph[i].edges.Count; j++)
                {
                    if (graph[i].edges[j].part == graph[i].part && graph[i].part!=-1)
                        return 0;
                }
            }
            return 1;
        }
    }

}
