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

        public Filter(double[,] newMatrix)
        {
            SetMatrix(newMatrix);
        }

        public Filter(Filter f)
        {
            SetMatrix(f.Matrix);
        }

        public Filter()
        {
            matrix = new double[3, 3];
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
            matrix = new double[n, m];
        }

        public Color[,] EditImage(Picture picture, ParallelOptions po, out ParallelLoopResult parResult)
        {
            Color[,] colorMap = picture.ColorMap;
            Color[,] expandedColorMap = picture.ExtendedColorMap;

            double div = Div;
            if (div == 0)
                div = 1;
            Color[,] res = new Color[
                colorMap.GetLength(0),
                colorMap.GetLength(1)];

            parResult = Parallel.For(0, colorMap.GetLength(0), po, i =>
            {
                if (po.CancellationToken.IsCancellationRequested)
                    return;

                for (int j = 0; j < colorMap.GetLength(1); j++)
                {
                    double R = 0, G = 0, B = 0;

                    for (int n = 0; n < matrix.GetLength(0); n++)
                        for (int m = 0; m < matrix.GetLength(1); m++)
                        {
                            R += matrix[n, m] * expandedColorMap[n + i, m + j].R;
                            G += matrix[n, m] * expandedColorMap[n + i, m + j].G;
                            B += matrix[n, m] * expandedColorMap[n + i, m + j].B;
                        }

                    R /= div;
                    G /= div;
                    B /= div;

                    R += offset;
                    G += offset;
                    B += offset;

                    if (R > 255)
                        R = 255;
                    if (B > 255)
                        B = 255;
                    if (G > 255)
                        G = 255;

                    if (R < 0)
                        R = 0;
                    if (B < 0)
                        B = 0;
                    if (G < 0)
                        G = 0;

                    res[i, j] = Color.FromArgb((int)R, (int)G, (int)B);
                }
            });
            return res;
        }

        public double this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

    }
}
