using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q5StronglyConnected : Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<Vertex> graph = new List<Vertex>();
            List<bool> BFS_check_list = new List<bool>();
            List<long> test = new List<long>();

            List<List<long>> result = new List<List<long>>();

            for (int i = 0; i <= (int)nodeCount; i++)
            {
                test.Add(i);
                BFS_check_list.Add(false);
                graph.Add(new Vertex());
                graph[i].index = i;
                result.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].neighbors.Add(graph[(int)edges[i][1]]);

            }
            

            for (int i = 1;  i<= nodeCount; i++)
            {
                for (int j = 0; j <= (int)nodeCount; j++)
                {
                    BFS_check_list[j] = false; ;

                }
                Queue<Vertex> Q = new Queue<Vertex>();
                Q.Enqueue(graph[i]);
                BFS_check_list[i] = true;
                while (Q.Count > 0)
                {
                    Vertex vertex = Q.Dequeue();
                    foreach (var item in vertex.neighbors)
                    {
                        if (BFS_check_list[(int)item.index] == false)
                        {
                            Q.Enqueue(item);
                            BFS_check_list[(int)item.index] = true;
                            result[i].Add(item.index);
                        }
                     
                    }
                }
               

            }
            long count = 0;
            List<long> checked_list = new List<long>(); 
            for (int i= 1; i <= nodeCount; i++)
            {
                for (int j = 0; j< result[i].Count; j++)
                {
                    if (result[(int)result[i][j]].Contains(i) )
                    {
                        test[(int)result[i][j]] = test[i];
                    }
                }
            }
            for (int i = 1; i<= nodeCount; i++)
            {
                if (!checked_list.Contains(test[i]))
                {
                    checked_list.Add(test[i]);
                    count++;
                }
            }
            return count;

        }
    }
}
