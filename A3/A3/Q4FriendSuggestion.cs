using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);
        public long extraxtMin(List<Node> stack)
        {
            int min = int.MaxValue;
            long minV = int.MaxValue;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].value1 < minV)
                {
                    min = i;
                    minV = stack[i].value1;
                }
                    
            }
            return min;
        }
        public long extraxtMin2(List<Node> stack)
        {
            int min = int.MaxValue;
            long minV = int.MaxValue;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].value2 < minV)
                {
                    minV = stack[i].value2;
                    min = i;
                }
            }
            return min;
        }
        public long[] Solve(long NodeCount, long EdgeCount, 
                              long[][] edges, long QueriesCount, 
                              long[][]Queries)
        {
            List<List<long>> costs = new List<List<long>>();
            List<long> result = new List<long>();
            List<Node> graph = new List<Node>();

            for (int i = 0; i <= (int)NodeCount; i++)
            {
                graph.Add(new Node());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][0]].edgeCosts.Add(edges[i][2]);

                graph[(int)edges[i][1]].edges_in.Add(graph[(int)edges[i][0]]);
                graph[(int)edges[i][1]].edge_inCosts.Add(edges[i][2]);
            }
            for (int k = 0; k < QueriesCount; k++)
            {
                for (int i = 0;  i<=(int)NodeCount; i++)
                {
                    graph[i].value1 = int.MaxValue;
                    graph[i].value2 = int.MaxValue;
                }
              
                List<Node> stack = new List<Node>();
                List<Node> stack2 = new List<Node>();
                List<Node> pro = new List<Node>();
                List<Node> pro2 = new List<Node>();
                graph[(int)Queries[k][1]].value2 = 0;
                graph[(int)Queries[k][0]].value1 = 0;
               
                stack.Add(graph[(int)Queries[k][0]]);
                stack2.Add(graph[(int)Queries[k][1]]);
                Node theNode = null;
                long minDistance = int.MaxValue;
                long counter = 0;
                while (stack.Count != 0 && stack2.Count !=0)
                {
                    counter++;
                    long min = extraxtMin(stack);
                    long min2 = extraxtMin2(stack2);
                    Node node2 = stack2[(int)min2];
                    Node node = stack[(int)min];
                    stack2.RemoveAt((int)min2);
                    stack.RemoveAt((int)min);
                    pro.Add(node);
                    pro2.Add(node2);
                   
                    for (int i = 0; i < node2.edges_in.Count; i++)
                    {
                        if (node2.value2 + node2.edge_inCosts[i] < node2.edges_in[i].value2)
                        {
                            node2.edges_in[i].value2 = node2.value2 + node2.edge_inCosts[i];
                            stack2.Add(node2.edges_in[i]);
                            
                        }

                    }
                 

                    for (int i = 0; i < node.edges.Count; i++)
                    {
                        if (node.value1 + node.edgeCosts[i] < node.edges[i].value1)
                        {
                            node.edges[i].value1 = node.value1 + node.edgeCosts[i];
                            stack.Add(node.edges[i]);
                           
                        }
                        
                    }

                        if (node.value2 != int.MaxValue)
                    {
                        if (node.value2 + node.value1 < minDistance)
                            minDistance = node.value2 + node.value1;
                    }
                    if (node2.value1 != int.MaxValue)
                    {
                        if (node2.value2 + node2.value1 < minDistance)
                            minDistance = node2.value2 + node2.value1;
                    }

                    if (pro.Contains(node) && pro2.Contains(node) && node.value1+node.value2 <= minDistance)
                        {
                                theNode = node;
                             break;
                        }
                        if (pro.Contains(node2) && pro2.Contains(node2) && node2.value2+node2.value1<=minDistance)
                        {

                                theNode = node2;
                        break;

                        }
                        



                }


                if (theNode != null)
                {   
                    long distance = theNode.value1 + theNode.value2;
                    if (graph[(int)Queries[k][0]].value2 < distance)
                        distance = graph[(int)Queries[k][0]].value2;
                    if (graph[(int)Queries[k][1]].value1 < distance)
                        distance = graph[(int)Queries[k][1]].value1;
                    result.Add(distance);
                }
                else
                    result.Add(-1);
               
               
            }
           
          

            return result.ToArray();
        }
    }
    public class minHeap
    {
        public List<Node> heap = new List<Node>();
        public void add(Node node)
        {
            heap.Add(node);
            sUp(heap.Count - 1);
        }
        public Node pop()
        {
            Node tmp = heap[0];
            sDown(0);
            
            return tmp;
        }
        public void sDown(int index)
        {
            if (heap.Count > (index * 2) + 2)
            {
                swap(index, (index * 2) + 2);
                sDown((index * 2) + 2);
            }
            else if (heap.Count > (index * 2) + 1)
            {
                swap(index, (index * 2) + 1);
                sDown((index * 2) + 1);
            }
            else
                heap.RemoveAt(index);
        }
        public void sUp(int index)
        {
            if (index != 0)
            {
                int parent = getParent(index);
                if (heap[parent].value1 > heap[index].value1)
                {
                    swap(index, parent);
                    sUp(parent);
                }
            }
        }
        public int getParent(int index)
        {
            if (index % 2 == 1)
                return (index - 1) / 2;
            else
                return (index - 2) / 2;
        }
        public void swap(int a , int b)
        {
            Node tmp = heap[a];
            heap[a] = heap[b];
            heap[b] = tmp ;
        }

    }
}
