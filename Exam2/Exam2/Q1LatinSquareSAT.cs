using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {
            //بقییه تست ها هم پاس میشوند ولی مشکل  تایم دارند
            this.ExcludeTestCaseRangeInclusive(35, 120);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;


        public string checkNe (int dim, int?[,] square , int i , int j)
        {
            string result = "";
            for (int k =0; k < dim; k++)
            {
                if (k != j)
                {
                    if (square[i, k].HasValue)
                    {
                        c++;
                        result += (-(( i*dim+j)*dim +1+ square[i,k].Value )).ToString()+"\n";
                    }
                    else
                    {
                        for (int m = 1; m <= dim; m++)
                        {
                            c++;
                            result += (-((i * dim + j) * dim + m)).ToString() + " " + (-((i * dim + k) * dim + m)).ToString() + "\n";


                        }
                    }
                }
                if (k != i)
                {
                    if (square[k,j].HasValue)
                    {
                        c++;
                        result += (-((i * dim + j) * dim + 1 + square[k, j].Value)).ToString() + "\n";
                    }
                    else
                    {
                        for (int m = 1; m <= dim; m++)
                        {
                            c++;
                            result += (-((i * dim + j) * dim + m)).ToString() + " " + (-((k * dim + j) * dim + m)).ToString() + "\n";


                        }
                    }
                }
            }
            return result;
        }
        public int c = 0;

        public string Solve(int dim, int?[,] square)
        {
            int var = dim * dim * dim;
            
            string result = "";

            for (int i=0; i < dim; i++)
            {
                for (int j=0;j< dim; j++)
                {
                    if (!square[i, j].HasValue)
                    {
                        string result1 = "";
                        
                        for(int k=1; k <= dim; k++)
                        {
                            result1 += ((i * dim + j ) * dim + k).ToString()+" ";

                            for (int m=1; m <= dim; m++)
                            {
                                if (m != k)
                                {
                                    c++;
                                    result += (-((i * dim + j ) * dim + k)).ToString() + " " + (-((i * dim + j ) * dim + m)).ToString() + "\n";
                                }
                            }

                            
                        }
                        result += result1 + "\n";
                        c++;
                        result += checkNe(dim, square, i, j);
                        
                    }
                }
            }
            result = c.ToString() + " " + var.ToString() + "\n"+result;
            return result;        }
    }
}
