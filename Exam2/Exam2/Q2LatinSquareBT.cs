using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam2
{
    public class Q2LatinSquareBT : Processor
    {
        public Q2LatinSquareBT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public bool check(int dim, int?[,] square, List<List<int>> table)
        {
            bool checkBack = false;
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (table[i*dim+j].Count == 0)
                    {
                        return false;
                    }
                    else if (table[i*dim+j].Count == 1 && !square[i,j].HasValue )
                    {
                        square[i, j] = table[i * dim + j][0];
                        checkBack = true;

                        for (int k = 0; k < dim; k++)
                        {
                            if (k != j)
                            {
                                table[i * dim + k].Remove(square[i, j].Value);
                            }
                            if (k != i)
                            {
                                table[k * dim + j].Remove(square[i, j].Value);
                            }
                        }
                    }
                }
            }
            if (checkBack)
            {
                return check(dim, square, table);
            }
            else
            {
                return true;
            }

        }
        public List<List<int>> func(int dim, int?[,] square , List<List<int>> table)
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (square[i, j].HasValue)
                    {
                        table[i * dim + j] = new List<int> { square[i, j].Value };
                        for (int k = 0; k < dim; k++)
                        {
                            if (k != j)
                            {
                                table[i * dim + k].Remove(square[i, j].Value);
                            }
                            if (k != i)
                            {
                                table[k * dim + j].Remove(square[i, j].Value);
                            }
                        }
                    }


                }

            }
            return table;
        }
        public string Solve(int dim, int?[,] square)
        {
            List<List<int>> table = new List<List<int>>();
            
            for (int i= 0; i<dim*dim; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < dim; j++)
                {
                    row.Add(j);
                }
                table.Add(row);

            }
            table = func(dim, square, table);
          
            bool result = check(dim, square, table);
            if (result)
                return "SATISFIABLE";
            else
                return "UNSATISFIABLE";

        }
    }
}
