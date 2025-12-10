using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Helper
{
    public static class MatrixHelpers
    {
        public static double[] GaussianElimination(double[,] A, double[] b)
        {
            int n = b.Length;
            int m = A.GetLength(1); // number of variables
            double[,] aug = new double[n, m + 1];

            // Build augmented matrix [A|b]
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    aug[i, j] = A[i, j];
                aug[i, m] = b[i];
            }

            // Forward elimination
            for (int col = 0, row = 0; col < m && row < n; col++, row++)
            {
                // Find pivot
                int pivot = row;
                for (int i = row + 1; i < n; i++)
                    if (Math.Abs(aug[i, col]) > Math.Abs(aug[pivot, col]))
                        pivot = i;

                // Swap rows
                if (pivot != row)
                {
                    for (int j = 0; j <= m; j++)
                    {
                        double tmp = aug[row, j];
                        aug[row, j] = aug[pivot, j];
                        aug[pivot, j] = tmp;
                    }
                }

                // Normalize pivot row
                double div = aug[row, col];
                if (Math.Abs(div) < 1e-9) continue; // skip if zero pivot
                for (int j = col; j <= m; j++)
                    aug[row, j] /= div;

                // Eliminate below
                for (int i = row + 1; i < n; i++)
                {
                    double factor = aug[i, col];
                    for (int j = col; j <= m; j++)
                        aug[i, j] -= factor * aug[row, j];
                }
            }

            // Back substitution
            double[] x = new double[m];
            bool[] isPivot = new bool[m];

            for (int i = n - 1; i >= 0; i--)
            {
                int lead = -1;
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(aug[i, j]) > 1e-9)
                    {
                        lead = j;
                        break;
                    }
                }
                if (lead == -1) continue;

                isPivot[lead] = true;
                x[lead] = aug[i, m];
                for (int j = lead + 1; j < m; j++)
                    x[lead] -= aug[i, j] * x[j];
            }

            // Set free variables explicitly to 0
            for (int j = 0; j < m; j++)
            {
                if (!isPivot[j]) 
                    x[j] = 0;
            }

            return x;
        }
    }
}