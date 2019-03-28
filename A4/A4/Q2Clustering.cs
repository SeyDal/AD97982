using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static A4.Q1BuildingRoads;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);


        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            MinHeap edges = new MinHeap(pointCount * pointCount);
            for (int i = 0; i < pointCount - 1; i++)
            {
                for (int j = i + 1; j < pointCount; j++)
                {
                    edge edge = new edge();
                    edge.u = i;
                    edge.v = j;
                    edge.cost = Math.Pow(Math.Pow((points[i][0] - points[j][0]), 2) + Math.Pow((points[i][1] - points[j][1]), 2), 0.5);
                    edges.Add(edge);
                }
            }

            List<long> sets = new List<long>();
            for (int i = 0; i < pointCount; i++)
            {
                sets.Add(i);
            }
            long setsNumber = pointCount;
            long size = edges._size;
           
            for (int i=0; i <size ; i++)
            {
                edge edge = edges.Pop();
                if (sets[edge.u] != sets[edge.v])
                {
                    setsNumber--;
                    long tmp = sets[edge.v];
                    for (int j = 0; j < pointCount; j++)
                    {
                        if (sets[j] == tmp)
                            sets[j] = sets[edge.u];
                    }
                    if (setsNumber == clusterCount)
                    {
                        break;
                    }
                }

            }
            size = edges._size;
            for (int i = 0; i < size; i++)
            {
                edge edge = edges.Pop();
                if (sets[edge.u] != sets[edge.v])
                {
                    return (double)((long)(edge.cost * 1000000 + 0.5)) / 1000000;
                }

            }
            return 0;

            
        }
    }
}
