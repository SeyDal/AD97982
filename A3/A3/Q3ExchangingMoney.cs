using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney:Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);
        public string findPath (List<Node> graph , Node target , long cost , long start)
        {
            Node targetTMP = target;
            int c = 0;
            while (true)
            {
                c++;
                if (c > 500)
                {
                    return "-";
                }
                if (targetTMP.parent == null  )
                    break;
                else
                {
                    cost += targetTMP.parent.Item2;
                    targetTMP = targetTMP.parent.Item1;
                }
            }
            return cost.ToString();

        }


        public string[] Solve(long nodeCount, long[][] edges,long startNode)
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
            graph[(int)startNode].value = 0;
            graph[(int)startNode].parent = null;

            for (int j = 0; j < (int)nodeCount - 1; j++)
            {
                for (int i = 0; i < edges.Length; i++)
                {
                    if (graph[(int)edges[i][0]].value != int.MaxValue)
                        if (graph[(int)edges[i][0]].value + edges[i][2] < graph[(int)edges[i][1]].value)
                        {
                            graph[(int)edges[i][1]].value = graph[(int)edges[i][0]].value + edges[i][2];
                            graph[(int)edges[i][1]].parent = Tuple.Create(graph[(int)edges[i][0]], edges[i][2]);
                        }
                }
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (graph[(int)edges[i][0]].value != int.MaxValue)
                    if (graph[(int)edges[i][0]].value + edges[i][2] < graph[(int)edges[i][1]].value)
                    {
                        graph[(int)edges[i][1]].value = int.MinValue;
                        graph[(int)edges[i][1]].parent = Tuple.Create(graph[(int)edges[i][0]], edges[i][2]);

                    }
            }

            List<string> result = new List<string>();
            for (int i = 1; i < nodeCount +1; i++)
            {
                if (graph[i].value == int.MaxValue)
                    result.Add("*");
                else if (graph[i].value == int.MinValue)
                    result.Add("-");
                else
                {
                    string pathCost = findPath(graph, graph[i] , 0 , startNode);
                    result.Add(pathCost);
                }
            }

            return result.ToArray();
        
        }
    }
}
