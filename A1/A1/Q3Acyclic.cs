using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<Vertex> graph = new List<Vertex>();
            List<bool> DFS_check_list = new List<bool>();
            for (int i = 0; i <= (int)nodeCount; i++)
            {
                DFS_check_list.Add(false);
                graph.Add(new Vertex());
                graph[i].index = i;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].neighbors.Add(graph[(int)edges[i][1]]);

            }

            Stack<Vertex> stack = new Stack<Vertex>();

            for (int i = 1; i <= nodeCount; i++)
            {
               
                stack.Push(graph[i]);
                DFS_check_list[i] = true;
             

                while (stack.Count > 0)
                {
                    Vertex vertex = stack.Pop();
                    foreach (var item in vertex.neighbors)
                    {
                        if (DFS_check_list[(int)item.index] == false)
                        {
                            stack.Push(item);
                            DFS_check_list[(int)item.index] = true;
                        }
                        else if (item.index == i)
                            return 1;
                    }

                }
                for (int j = 0; j <= nodeCount; j++)
                    DFS_check_list[j] = false;

            }



          
            return 0;
        }
    }
}