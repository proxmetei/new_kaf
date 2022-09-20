using System;
using System.Collections.Generic;

namespace OnlineChat.Models.CategoryTable
{
    public static class AnalizService
    {
        static double[][] mat;

        static double[,] m = {
            {1, 4, 1/5}, // стаж работы
            {1/4, 1, 1/7}, // наличие аспирантуры
            {5, 7, 1} // занятость курсовиками

        };
        static double[,] mCat = {
            {1, 4, 1/5, 1/6}, // стаж работы
            {1/4, 1, 1/7, 1/8}, // наличие аспирантуры
            {5, 7, 1, 1/2}, // занятость курсовиками
            {6, 8, 2, 1} // близость тематики

        };
        static double[] weights;
        static double[] getWieghts(double[][] m) {
            int length = m[0].Length;
            double[] sums = new double[length];
            double[] result = new double[length];
            for (int i = 0; i < length; i++) {
                sums[i] = 0;
                result[i] = 0;
            }
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    sums[j] += m[i][j];
                }
            }
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    m[i][j] /= sums[j];
                }
            }
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    result[i] += m[i][j];
                }
                result[i] /= length;
            }
            return result;
        }
        public static double[] init(double [,] m)
        {
            int length = (int)Math.Sqrt(m.Length);
            mat = new double[length][];
            for (int i = 0; i < length; i++) {
                mat[i] = new double[length];
            }
            for (int i = 0; i < length; i++) {
                for (int j = 0; j < length; j++) {
                    mat[i][j] = m[i, j];
                }
            }
            weights = getWieghts(mat);
            return weights;
        }
        public static double[] getWeight(double[] counts)
        {
            double[][] matrix = new double[counts.Length][];
            for (int i = 0; i < counts.Length; i++) {
                matrix[i] = new double[counts.Length];
            }
            double maxDiffer = 0;
            for (int i = 0; i < counts.Length-1; i++) {
                for (int j = i + 1; j < counts.Length; j++) {
                    if (Math.Abs(counts[i] - counts[j]) > maxDiffer)
                    {
                        maxDiffer = Math.Abs(counts[i] - counts[j]);
                    }
                }
            }
            for (int i = 0; i < counts.Length; i++) {
                for (int j = 0; j < counts.Length; j++) {
                    if (counts[i] >= counts[j])
                    {
                        matrix[i][j] = 1 + (int)(8 * (counts[i] - counts[j]) / maxDiffer);
                    }
                    else {
                         matrix[i][j] =(double)1.0/( 1 + (int)(8 * (counts[j] - counts[i]) / maxDiffer));
                    }
                }
            }
            return getWieghts(matrix);
        }

        public static double[] proizvWithoutcat(List<double[]> matrix, int l) {
            double[] result = new double[l];
            init(m);
            for (int i = 0; i < l; i++) {
                for (int j = 0; j < weights.Length; j++)
                {
                    result[i] += matrix[j][i]*weights[j];
                }
            }
            return result;
        }
        public static double[] proizvWithcat(List<double[]> matrix, int l, int id)
        {
            double[] result = new double[l];
            init(mCat);
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < weights.Length; j++)
                {
                    if (j == 3)
                    {
                        result[i] += matrix[id][i] * weights[j];
                    }
                    else
                    {
                        result[i] += matrix[j][i] * weights[j];
                    }
                }
            }
            return result;
        }
    }
}
