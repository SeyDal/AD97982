using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace Exam1
{
    public class Q1Betweenness : Processor
    {
        public Q1Betweenness(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(15, 36);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);


        public long[] Solve(long NodeCount, long[][] edges)
        {
            List<Node> graph = new List<Node>();
            for(int i=0; i<= NodeCount; i++)
            {
                Node node = new Node();
                node.number = i;
                graph.Add(node);
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
            }
            for (int i = 1; i <= NodeCount; i++)
            {
                Node temp ;

                for (int write = 0; write < graph[i].edges.Count; write++)
                {
                    for (int sort = 0; sort < graph[i].edges.Count -1 ; sort++)
                    {
                        if (graph[i].edges[sort].number < graph[i].edges[sort + 1].number)
                        {
                            temp = graph[i].edges[sort + 1];
                            graph[i].edges[sort + 1] = graph[i].edges[sort];
                            graph[i].edges[sort] = temp;
                        }
                    }
                }
            }

            for (int i=1; i<= NodeCount; i++)
            {
               for (int j=1; j<= NodeCount; j++)
                {
                    for(int m=1; m <= NodeCount; m++)
                    {
                        graph[m].parent = null;
                    }
                    Queue<Node> Q = new Queue<Node>();
                    long[] isChecked = new long[NodeCount + 1];
                    if (i != j)
                    {
                        Q.Enqueue(graph[i]);
                        isChecked[i] = 1;
                    }
                   
                    bool find = false;
                    while (Q.Count != 0 && find==false)
                    {
                        Node node = Q.Dequeue();
                        for (int k = 0; k < node.edges.Count; k++)
                        {
                            if (isChecked[(int)node.edges[k].number] != 1)
                            {
                                Q.Enqueue(node.edges[k]);
                                node.edges[k].parent = node;
                                isChecked[(int)node.edges[k].number] = 1;
                            }
                            if (node.edges[k].number == j)
                            {
                                find = true;
                                break;
                               
                            }
                        }
                        
                    }
                    if (find == true)
                    {
                        Node node1 = graph[j];
                        while (true)
                        {
                            node1 = node1.parent;
                            if (node1.number == i)
                                break;
                            node1.betweenness++;
                        }
                    }
                    
                }

            }
            List<long> result = new List<long>();
            for (int i=1; i<= NodeCount; i++)
            {
                result.Add(graph[i].betweenness);
            }
            return result.ToArray();
        }
    }
    public class Node
    {
        public List<Node> edges = new List<Node>();
        public long betweenness = 0;
        public Node parent = null;
        public long number;
        public List<long> pathesFind = new List<long>();
    }
}
