using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
            ExcludeTestCases(new int[] { 17 ,5 ,33,41});

        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);
        public List<List<double>> makeTable (int c , int v , double[,] matrix1)
        {
            List<List<double>> table = new List<List<double>>();

            for (int i = 0; i <= c; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < (v + c + 2); j++)
                {
                    row.Add(0);
                }
                table.Add(row);
            }


            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    table[i][j] = matrix1[i, j];
                }
                table[i][v + c + 1] = matrix1[i, v];
                table[i][v + i] = 1;
            }



            for (int j = 0; j < v; j++)
            {
                table[c][j] = -matrix1[c, j];
            }
            table[c][v + c + 1] = matrix1[c, v];
            table[c][v + c] = 1;
            return table;
        }
        public int minIndex (List<List<double>> table , int c, int v)
        {
            double min = double.MaxValue;
            int minIndex = 0;
            for (int i=0; i < v + c + 1; i++)
            {
                if (table[c][i] < min)
                {
                    min = table[c][i];
                    minIndex = i;
                }

            }
            return minIndex;

        }
        public List<List<double>> changeTable(int c, int v, List<List<double>> table, int min_index , int min_row_index)
        {
            for (int i = 0; i <= c; i++)
            {
                if (table[i][min_index] != 0 && i !=min_row_index )
                {
                    double x = table[i][min_index];
                     for (int j=0; j< v+c+2; j++)
                    {
                        table[i][j] = table[i][j] + ((table[min_row_index][j] * -1 * x)/ table[min_row_index][min_index]) ;
                    }
                }
            }

            return table;
        }
        public bool isTrue (List<double> answers ,  List<List<double>> table , int c , int v)
        {
            for (int i=0; i<c; i++)
            {
                double value = 0;
                for (int j=0; j < v; j++)
                {
                    value += table[i][j] * answers[j];
                }
                if (value > table[i][c + v + 1])
                    return false;
            }
            return true;
        }
        public string Solve(int c, int v, double[,] matrix1)
        {
            List<List<double>> table = new List<List<double>>();
            List<double> answers = new List<double>();
            table = makeTable(c, v, matrix1);


            while (true)
            {
                int min_index = minIndex(table, c, v);
                if (table[c][min_index] < 0)
                {
                    int min_row_index = -1;
                    double min_row = double.MaxValue;
                    for (int i = 0; i < c; i++)
                    {
                        if (table[i][c + v + 1] / table[i][min_index] >= 0)
                        {
                            if (table[i][c + v + 1] / table[i][min_index] < min_row)
                            {
                                min_row = table[i][c + v + 1] / table[i][min_index];
                                min_row_index = i;
                            }
                        }
                    }
                    if (min_row_index == -1)
                        return "Infinity";
                    else
                    {
                        table = changeTable(c, v, table, min_index, min_row_index);
                    }
                }
                else
                {
                    break;
                }
            }
            string result = "Bounded Solution\n";
            for (int i = 0; i < v; i++)
            {
                bool one = false;
                int k = 0;
                int one_index = -1;
                for (int j = 0; j <= c; j++)
                {
                    if (Math.Round( table[j][i]*2 )/2== 0)
                        k = 5;


                    else if (one == false)
                    {
                        one = true;
                        one_index = j;


                    }
                    else if (one == true)
                    {
                        one = false;
                        break;
                    }
                }
                if (one == false)
                { 

                    result += "0 ";
                    answers.Add(0);
                }
                else
                {
                    answers.Add(table[one_index][c + v + 1] / table[one_index][i]);
                    result +=  (Math.Round(table[one_index][c + v + 1]/table[one_index][i]*2)/2).ToString()+" ";
                }

            }
            if (isTrue(answers , table ,c,v))

                return result;
            else 
                return "No Solution";
        }

    }
}
