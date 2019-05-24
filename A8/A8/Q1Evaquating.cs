using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName)
        {
            //this.ExcludeTestCaseRangeInclusive(1, 1);
            //this.ExcludeTestCaseRangeInclusive(11, 100);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public List<long> bfs (List<List<long>> graph , int s , int t)
        {
            List<bool> visited = new List<bool>();
            List<long> parent = new List<long>();
            for (int i= 0; i< graph.Count; i++)
            {
                parent.Add(-2);
                visited.Add(false);
            }
            Queue<long> Q = new Queue<long>();
            Q.Enqueue(s);
            visited[s] = true;
            parent[s] = -1;
            while (Q.Count != 0)
            {
                long node = Q.Dequeue();
                for (int i = 0; i<graph.Count; i++)
                {
                    if (visited[i] == false)
                    {
                        if (graph[(int)node][i] > 0)
                        {
                            parent[i] = node;
                            visited[i] = true;
                            Q.Enqueue(i);

                        }
                    }
                }
            }
            return parent;
        }
        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            List<List<long>> graph = new List<List<long>>();
            List<List<long>> residualGraph = new List<List<long>>();
            for (int i= 0; i<=nodeCount; i++)
            {
                List<long> row = new List<long>();
                List<long> row2 = new List<long>();
                for (int j = 0; j <= nodeCount; j++)
                {
                    row.Add(0);
                    row2.Add(0);
                }
                graph.Add(row);
                residualGraph.Add(row2);

            }
           
            for (int i= 0; i<edgeCount; i++)
            {
                if (graph[(int)edges[i][0]][(int)edges[i][1]] == 0)
                {
                    graph[(int)edges[i][0]][(int)edges[i][1]] = edges[i][2];
                    residualGraph[(int)edges[i][0]][(int)edges[i][1]] = edges[i][2];
                }
                else
                {
                    graph[(int)edges[i][0]][(int)edges[i][1]] += edges[i][2];
                    residualGraph[(int)edges[i][0]][(int)edges[i][1]] += edges[i][2];
                }
            }

            int maxFlow = 0;
            var parent = bfs(residualGraph, 1, (int)nodeCount);
            while (parent[(int)nodeCount] != -2)
            {
                int flow = int.MaxValue;
                for (long i = nodeCount; i != 1; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    flow = Math.Min(flow, (int)residualGraph[(int)node][(int)i]);


                }
                for (long i = nodeCount; i != 1; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    residualGraph[(int)node][(int)i] -= flow;
                    residualGraph[(int)i][(int)node] += flow;


                }
                maxFlow += flow;
                parent = bfs(residualGraph, 1, (int)nodeCount);

            }

            return maxFlow;
        }
    }
}
