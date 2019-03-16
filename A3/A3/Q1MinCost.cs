using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost:Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long,long,long>)Solve);
        public long extraxtMin(List<Node> stack)
        {
            int min = int.MaxValue;
            for (int i = 0; i< stack.Count; i++)
            {
                if (stack[i].value < min)
                    min = i;
            }
            return min;
        }


        public long Solve(long nodeCount,long [][] edges, long startNode, long endNode)
        {
            List<Node> graph = new List<Node>();

            for (int i = 0; i <= (int)nodeCount; i++)
            {
                graph.Add(new Node());
            }
            for (int i = 0; i<edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][0]].edgeCosts.Add(edges[i][2]);
            }
            List<Node> stack = new List<Node>();
            graph[(int)startNode].value = 0;
            stack.Add(graph[(int)startNode]);
            while (stack.Count != 0)
            {
                long min = extraxtMin(stack);
                Node node = stack[(int)min];
                stack.RemoveAt((int)min);
                for (int i =0; i < node.edges.Count; i++)
                {
                    if (node.value+node.edgeCosts[i] < node.edges[i].value)
                    {
                        node.edges[i].value = node.value + node.edgeCosts[i];
                        stack.Add(node.edges[i]);
                    }
                }
            }
            //Write Your Code Here
            if (graph[(int)endNode].value != int.MaxValue)
                return graph[(int)endNode].value;
            else
                return -1;
        }
    }
    public class Node
    {
        public long value = int.MaxValue;
        public long value1 = int.MaxValue;
        public long value2 = int.MaxValue;
        public List<Node> edges = new List<Node>();
        public List<Node> edges_in = new List<Node>();
        public List<long> edgeCosts = new List<long>();
        public List<long> edge_inCosts = new List<long>();
        public Tuple<Node,long> parent;
        
    }

   
}
