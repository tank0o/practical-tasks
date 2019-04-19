using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task5.Classes
{
    public class Filter
    {
        double[,] matrix;
        double offset = 0;


        public double[,] Matrix
        {
            get { return matrix; }
            set { SetMatrix(value); }
        }

        public double Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public double Div
        {
            get
            {
                double div = 0;

                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        div += matrix[i, j];

                return div;
            }
        }

        public Filter(double[,] newMatrix, string name = "default")
        {
            SetMatrix(newMatrix);
        }

        public Filter(Filter newFilter)
        {
            SetMatrix(newFilter.Matrix);
        }

        public Filter()
        {
            SetDefault();
        }

        public int GetN
        {
            get { return Matrix.GetLength(0); }
        }

        public int GetM
        {
            get { return Matrix.GetLength(1); }
        }

        public void SetMatrix(double[,] newMatrix)
        {
            matrix = new double[
                newMatrix.GetLength(0),
                newMatrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = newMatrix[i, j];
        }

        public void SetMatrix(int n, int m)
        {
            if (n <= 0 || m <= 0)
                throw new Exception("Incorrect matrix power! ");

            matrix = new double[n, m];
        }

        public void SetDefault()
        {
            matrix = new double[3, 3];
        }

        public Color[,] EditImage(Picture picture, ParallelOptions ops, out ParallelLoopResult parResult)
        {
            Color[,] colorMap = picture.ColorMap;
            Color[,] expandedColorMap = picture.ExtendedColorMap;

            double div = Div;

            Color[,] result = new Color[
                colorMap.GetLength(0),
                colorMap.GetLength(1)];

            parResult = Parallel.For(0, colorMap.GetLength(0), ops, i =>
            {
                if (ops.CancellationToken.IsCancellationRequested)
                    return;

                for (int j = 0; j < colorMap.GetLength(1); j++)
                {
                    double sumR = 0, sumG = 0, sumB = 0;

                    for (int n = 0; n < matrix.GetLength(0); n++)
                        for (int m = 0; m < matrix.GetLength(1); m++)
                        {
                            sumR += matrix[n, m] * expandedColorMap[n + i, m + j].R;
                            sumG += matrix[n, m] * expandedColorMap[n + i, m + j].G;
                            sumB += matrix[n, m] * expandedColorMap[n + i, m + j].B;
                        }

                    sumR /= div;
                    sumG /= div;
                    sumB /= div;

                    sumR += offset;
                    sumG += offset;
                    sumB += offset;

                    sumR = sumR > 255 ? 255 : sumR;
                    sumR = sumR < 0 ? 0 : sumR;

                    sumG = sumG > 255 ? 255 : sumG;
                    sumG = sumG < 0 ? 0 : sumG;

                    sumB = sumB > 255 ? 255 : sumB;
                    sumB = sumB < 0 ? 0 : sumB;

                    result[i, j] = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                }
            });

            return result;
        }

        public double this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

    }
}
