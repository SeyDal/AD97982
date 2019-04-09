using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);
        public bool check(int index, string text, Node root)
        {
            Node current = root;
            int indexTMP = index;
            while (true)
            {
                bool find = false;
                for (int i = 0; i < current.nextEdges.Count; i++)
                {
                    if (current.nextEdges[i] == text[indexTMP])
                    {
                        current = current.nextNodes[i];
                        indexTMP++;
                        find = true;
                        break;
                    }
                }
                if (find == false)
                    return false;
                else if (current.isFinal == true)
                {
                    return true;
                }
                if (indexTMP == text.Length)
                    return false;

            }
            return false;
        }

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();
            Node root = new Node();
            root.number = 0;
            int counter = 1;
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
                        current.nextNodes.Add(node);
                        current.nextEdges.Add(patterns[i][index]);
                        index++;
                        current = current.nextNodes[current.nextNodes.Count - 1];



                    }
                    if (index == patterns[i].Length)
                    {
                        current.isFinal = true;
                        break;
                    }


                }
            }
            for (int i = 0; i < text.Length; i++)
            {
                bool checkResult = check(i, text, root);
                if (checkResult == true)
                    result.Add(i);
            }
            if (result.Count > 0)
                return result.ToArray();
            else
            {
                result.Add(-1);
                return result.ToArray();
            }

        }
    }
}
