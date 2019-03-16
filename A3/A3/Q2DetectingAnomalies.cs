using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges)
        {
            List<Node> graph = new List<Node>();

            for (int i = 0; i <= (int)nodeCount; i++)
            {
                graph.Add(new Node());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][0]].edgeCosts.Add(edges[i][2]);
            }
            for (int k = 1; k < nodeCount + 1; k++)
            {
                for (int i = 0; i <= (int)nodeCount; i++)
                {
                    graph[i].value = int.MaxValue;
                }
                graph[k].value = 0;
                for (int j = 0; j < (int)nodeCount - 1; j++)
                {
                    for (int i = 0; i < edges.Length; i++)
                    {
                        if (graph[(int)edges[i][0]].value != int.MaxValue)
                            if (graph[(int)edges[i][0]].value + edges[i][2] < graph[(int)edges[i][1]].value)
                            {
                                graph[(int)edges[i][1]].value = graph[(int)edges[i][0]].value + edges[i][2];
                            }
                    }
                }
                for (int i = 0; i < edges.Length; i++)
                {
                    if (graph[(int)edges[i][0]].value != int.MaxValue)
                        if (graph[(int)edges[i][0]].value + edges[i][2] < graph[(int)edges[i][1]].value)
                        {
                            return 1;
                        }
                }
            }
           

            return 0;
        }
    }
}
