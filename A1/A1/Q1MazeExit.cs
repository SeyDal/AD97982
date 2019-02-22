using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            List<Vertex> graph = new List<Vertex>();
            List<bool> BFS_check_list = new List<bool>();
            for (int i=0; i<=(int)nodeCount; i++)
            {
                BFS_check_list.Add(false);
                graph.Add(new Vertex());
                graph[i].index = i;
            }
            for (int i=0; i< edges.Length; i++)
            {
                graph[(int)edges[i][0]].neighbors.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][1]].neighbors.Add(graph[(int)edges[i][0]]);

            }
            Queue<Vertex> Q = new Queue<Vertex>();
            Q.Enqueue(graph[(int)StartNode]);
            BFS_check_list[(int)StartNode ] = true;
            while (Q.Count > 0)
            {
                Vertex vertex = Q.Dequeue();
                foreach (var item in vertex.neighbors)
                {
                    if (BFS_check_list[(int)item.index] == false)
                    {
                        Q.Enqueue(item);
                        BFS_check_list[(int)item.index] = true;
                    }
                    if(item.index == EndNode)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }    
     }
    public class Vertex
    {
        public long index;
        public long start_dfs_time = -1;
        public long finish_dfs_time = -1;
        public List<Vertex> neighbors = new List<Vertex>();
        public List<Vertex> neighbors_in = new List<Vertex>();

    }
}
