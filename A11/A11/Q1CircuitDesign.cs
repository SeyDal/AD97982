using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using Microsoft.SolverFoundation.Solvers;

namespace A11
{
    public class Q1CircuitDesign : Processor
    {
        public Q1CircuitDesign(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(10,10);
            this.ExcludeTestCaseRangeInclusive(3, 3);
            this.ExcludeTestCaseRangeInclusive(9, 9);





        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], Tuple<bool, long[]>>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatAssignmentVerifier;

        public virtual Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {

            List<Vertex> graph = new List<Vertex>();
            List<bool> BFS_check_list = new List<bool>();
            List<long> test = new List<long>();
            Tuple<bool,List <long>> output = Tuple.Create(false, new List <long>());
            List<List<long>> result = new List<List<long>>();

            long nodeCount = v * 2;
            for (int i = 0; i <= (int)nodeCount; i++)
            {
                test.Add(i);
                BFS_check_list.Add(false);
                graph.Add(new Vertex());
                graph[i].index = i;
                result.Add(new List<long>());
            }
            for (int i = 0; i < cnf.Length; i++)
            {
                long m1,m2,n1, n2;
                if (cnf[i][0] > 0)
                {
                    m1 = cnf[i][0];
                    m2 = m1 + v;
                }
                else
                {
                    m2 = -cnf[i][0];
                    m1 = m2 + v;
                }
                if (cnf[i][1] > 0)
                {
                    n1 = cnf[i][1];
                    n2 = n1 + v;
                }
                else
                {
                    n2 = -cnf[i][1];
                    n1 = n2 + v;
                }
                graph[(int)m2].neighbors.Add(graph[(int)n1]);
                graph[(int)n2].neighbors.Add(graph[(int)m1]);


            }


            for (int i = 1; i <= nodeCount; i++)
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
            
            List<long> checked_list = new List<long>();
            for (int i = 1; i <= nodeCount; i++)
            {
                for (int j = 0; j < result[i].Count; j++)
                {
                    if (result[(int)result[i][j]].Contains(i))
                    {
                        test[(int)result[i][j]] = test[i];
                    }
                }
            }
            for (int i=1; i<= v; i++)
            {
                if (test[i] == test[i + (int)v])
                    return Tuple.Create(output.Item1 , output.Item2.ToArray()) ;
            }
            List<int> checkList = new List<int>();
         
            for (int i=1; i <= v; i++)
            {
                bool t = false;
                for (int j=1; j <= nodeCount; j++)
                {
                    if (t == true)
                        break;
                    if (test[i] == test[j])
                    {
                        for (int k=1; k<= nodeCount; k++)
                        {
                            if (test[i+(int)v] == test[k])
                            {
                                if (result[j].Contains(k))
                                {
                                    output.Item2.Add(-i);
                                    t = true;

                                }
                                else if (result[k].Contains(j))
                                {
                                    output.Item2.Add(i);
                                    t = true;

                                }
                            }
                            if (t == true)
                                break;
                        }
                    }
                }
               
             if (t == false)
                {
                    for (int m =1 ; m <= v; m++)
                    {
                        if (test[m] == test[i])
                        {
                            if (output.Item2.Contains(m))
                            {
                                output.Item2.Add(i);
                                break;
                                t = true;
                            }
                            else if (output.Item2.Contains(-m))
                            {
                                output.Item2.Add(-i);
                                break;
                                t = true;


                            }
                        }
                        else if (test[m] == test[i+(int)v])
                        {
                            if (output.Item2.Contains(m))
                            {
                                output.Item2.Add(-i);
                                break;
                                t = true;


                            }
                            else if (output.Item2.Contains(-m))
                            {
                                output.Item2.Add(i);
                                break;
                                t = true;


                            }
                        }
                    }
                }
                if (t == false)
                {
                    output.Item2.Add(i);

                }
            }
            
            return Tuple.Create(true, output.Item2.ToArray());


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
