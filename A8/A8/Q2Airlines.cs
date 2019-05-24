using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);
        public List<long> bfs(List<List<long>> graph, int s, int t)
        {
            List<bool> visited = new List<bool>();
            List<long> parent = new List<long>();
            for (int i = 0; i < graph.Count; i++)
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
                for (int i = 0; i < graph.Count; i++)
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
        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            List<List<long>> graph = new List<List<long>>();
            List<List<long>> residualGraph = new List<List<long>>();
            List<long> result = new List<long>();
            long nodeCount = flightCount + crewCount + 2;
            for (int i = 0; i < nodeCount; i++)
            {
                List<long> row = new List<long>();
                List<long> row2 = new List<long>();
                for (int j = 0; j < nodeCount; j++)
                {
                    row.Add(0);
                    row2.Add(0);
                }
                graph.Add(row);
                residualGraph.Add(row2);

            }

            for (int i=1;i<=flightCount; i++)
            {
                result.Add(-1);
                graph[0][i] = 1;
                residualGraph[0][i] = 1;
            }
            result.Add(-1);
            for (long i = flightCount +1; i < nodeCount; i++)
            {
                graph[(int)i][(int)nodeCount-1] = 1;
                residualGraph[(int)i][(int)nodeCount - 1] = 1;
            }
            for (int i = 0; i < flightCount; i++)
            {
              
                for (int j = 0; j < crewCount; j++)
                {
                    graph[i+1][j+1+(int)flightCount] = info[i][j];
                    residualGraph[i + 1][j + 1 + (int)flightCount] = info[i][j];
                }
            }
            int maxFlow = 0;
            var parent = bfs(residualGraph, 0, (int)nodeCount-1);
            while (parent[(int)nodeCount-1] != -2)
            {
                int flow = int.MaxValue;
                for (long i = nodeCount-1; i != 0; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    flow = Math.Min(flow, (int)residualGraph[(int)node][(int)i]);
                   


                }
                for (long i = nodeCount-1; i != 0; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    residualGraph[(int)node][(int)i] -= flow;
                    residualGraph[(int)i][(int)node] += flow;


                }
                maxFlow += flow;
                parent = bfs(residualGraph, 0, (int)nodeCount-1);

            }

         
            
            for (int i = 0; i<nodeCount; i++)
            {
                if (residualGraph[(int)nodeCount-1][i] == 1)
                {
                    for (int j=1; j <=flightCount; j++)
                    {
                        if (residualGraph[i][j] == 1)
                        {
                            result[j] = i - flightCount;
                        }
                    }
                }
            }
            result.RemoveAt(0);
            return result.ToArray();
        }

  }
}
