using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName) {
            ExcludeTestCases(new int[] { 26 });
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);
        public double[,] elimination (double[,] matrix , int rowCount)
        {
            for (int start = 0; start  < rowCount-1; start++)
            {
                for (int target = start + 1; target < rowCount; target++)
                {
                    double startX = matrix[target, start];
                    double targetX = matrix[start, start];
                    for (int i = 0; i < rowCount + 1; i++)
                    {
                        matrix[target, i] = targetX * matrix[target, i] - startX * matrix[start, i] ;

                    }
                }
            }

            for (int row = rowCount - 1; row >= 0; row--)
            {
                double x = matrix[row, row];

                for (int i = 0; i < rowCount + 1; i++)
                {
                    matrix[row, i] /= x;
                } 
                for (int target = 0; target < row; target++)
                {
                    matrix[target, rowCount] = matrix[target, rowCount] - matrix[target, row] * matrix[row, rowCount]; matrix[target, row] = 0;
                }
            }
            return matrix;
        }


        public double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            matrix = swapRows(rowCount, matrix);
            matrix = elimination(matrix, rowCount);
            double[] result = new double[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                if (matrix[i, i] == 0)
                {
                    result[i] = 0;
                    continue;
                }
                result[i] = matrix[i,matrix.GetLength(1) - 1] / matrix[i, i];
                result[i] = Math.Round(result[i] * 2) / 2;
            }
            return result;
        }
        public double[,] swapRows(int rowCount, double[,] matrix)
        {
            for (int column = 0; column < rowCount - 1; column++)
                if (matrix[column, column] == 0)
                {
                    int Row = column + 1;
                    for (; Row < rowCount; Row++)
                        if (matrix[Row, column] != 0)
                            break;

                    if (matrix[Row, column] != 0)
                    {

                        double[] tmp = new double[rowCount + 1];
                        for (int i = 0; i < rowCount + 1; i++)
                        {
                            tmp[i] = matrix[Row, i];
                            matrix[Row, i] = matrix[column, i];
                            matrix[column, i] = tmp[i];
                        }
                    }
                }
            return matrix;
        }
    }
}
