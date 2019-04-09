using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            Node root = new Node();
            root.number = 0;
            int counter = 1;
            List<string> result = new List<string>();
            
            for (int i = 0; i < n; i++)
            {
                Node current = root;
                int index = 0;
                while (true)
                {
                    bool find = false;
                    for (int j = 0; j < current.nextNodes.Count; j++)
                    {
                        if (current.nextEdges[j] == patterns[i][index])
                        {
                            find = true;
                            index++;
                            current = current.nextNodes[j];
                            break;
                        }
                    }
                    if (find == false)
                    {
                        Node node = new Node();
                        node.number = counter;
                        counter++;
                        result.Add(current.number.ToString()+"->"+node.number.ToString() + ":" + patterns[i][index]);
                        current.nextNodes.Add(node);
                        current.nextEdges.Add(patterns[i][index]);
                        index++;
                        current = current.nextNodes[current.nextNodes.Count-1];



                    }
                    if (index == patterns[i].Length)
                        break;
                    
                }
            }
            return result.ToArray();
        }
    }
    public class Node 
    {
        public List<Node> nextNodes = new List<Node>();
        public List<char> nextEdges = new List<char>();
        public long number;
        public bool isFinal = false;
    }
}
