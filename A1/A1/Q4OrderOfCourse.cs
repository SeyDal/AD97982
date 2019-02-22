using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace A12
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
        {
            List<Vertex> graph = new List<Vertex>();
            List<long> result = new List<long>();
            for (int i = 0; i <= (int)nodeCount; i++)
            {
                graph.Add(new Vertex());
                graph[i].index = i;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].neighbors.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][1]].neighbors_in.Add(graph[(int)edges[i][0]]);


            }
            while (graph.Count > 1 )
            {
                for (int i = 1; i<graph.Count(); i++)
                {
                    if (graph[i].neighbors_in.Count()==0)
                    {
                        result.Add(graph[i].index);
                        for (int j = 0; j < graph[i].neighbors.Count(); j++)
                            graph[i].neighbors[j].neighbors_in.Remove(graph[i]);
                        graph.RemoveAt(i);
                        break;
                    }
                }
            }



            return result.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        /// <summary>
        /// کد شما با متد زیر راست آزمایی میشود
        /// این کد نباید تغییر کند
        /// داده آزمایشی فقط یک جواب درست است
        /// تنها جواب درست نیست
        /// </summary>
        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
