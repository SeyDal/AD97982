using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName)
        { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public bool touchIt(Stock chart1 , Stock chart2 , long pointCount)
        {
            for (int i=0; i < pointCount; i++)
            {
                if (chart1.points[i] >= chart2.points[i])
                    return true;
            }

            return false;
        }
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
        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            List<Stock> stockes = new List<Stock>();
            for (int i =0; i<stockCount; i++)
            {
                stockes.Add(new Stock());
                for (int j=0; j < pointCount; j++)
                {
                    stockes[i].points.Add(matrix[i][j]);
                }
            }


            List<List<long>> graph = new List<List<long>>();
            List<List<long>> residualGraph = new List<List<long>>();
            long nodeCount = stockCount*2+2;
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
            for (int i = 1; i <= stockCount; i++)
            {
                graph[0][i] = 1;
                residualGraph[0][i] = 1;
            }
            for (long i = stockCount + 1; i < nodeCount; i++)
            {
                graph[(int)i][(int)nodeCount - 1] = 1;
                residualGraph[(int)i][(int)nodeCount - 1] = 1;
            }
            for (int i = 0; i < stockCount; i++)
            {

                for (int j = 0; j < stockCount; j++)
                {
                    if (!touchIt(stockes[i], stockes[j], pointCount))
                    {
                        graph[i + 1][j + 1 + (int)stockCount] = 1;
                        residualGraph[i + 1][j + 1 + (int)stockCount] = 1;
                    }
                    
                }
            }
            int maxFlow = 0;
            var parent = bfs(residualGraph, 0, (int)nodeCount - 1);
            while (parent[(int)nodeCount - 1] != -2)
            {
                int flow = int.MaxValue;
                for (long i = nodeCount - 1; i != 0; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    flow = Math.Min(flow, (int)residualGraph[(int)node][(int)i]);



                }
                for (long i = nodeCount - 1; i != 0; i = parent[(int)i])
                {
                    long node = parent[(int)i];
                    residualGraph[(int)node][(int)i] -= flow;
                    residualGraph[(int)i][(int)node] += flow;


                }
                maxFlow += flow;
                parent = bfs(residualGraph, 0, (int)nodeCount - 1);

            }

            return stockCount-maxFlow;
        }
    }
    public class Stock
    {
        public List<long> points = new List<long>();
    }
}
