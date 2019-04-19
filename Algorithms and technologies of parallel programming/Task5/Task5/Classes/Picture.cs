using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task5.Classes
{
    public class Picture
    {
        Color[,] picture;
        Color[,] extendedPicture;
        Color[,] filterPicture;
        Filter filter = null;

        string path = "";

        static ParallelOptions ops = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 8
        };

        public string Path { get { return path; } }
        public Color[,] ColorMap
        {
            get { return picture; }
            set
            {
                picture = value;

            }
        }
        public Color[,] FilterColorMap
        {
            get { return filterPicture; }
            set
            {
                filterPicture = value;

            }
        }
        public Color[,] ExtendedColorMap
        {
            get { return extendedPicture; }
        }
        public Filter Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                GenerateExtendedPicture();
            }
        }

        public void NewPicture(string path_)
        {
            path = path_;

            Image imageFile = Image.FromFile(path_);

            Bitmap blank = new Bitmap(imageFile.Width, imageFile.Height);

            Graphics g = Graphics.FromImage(blank);
            g.Clear(Color.White);
            g.DrawImage(imageFile, 0, 0, blank.Width, blank.Height);

            Bitmap imgMap = new Bitmap(blank);

            picture = new Color[
                imgMap.Width,
                imgMap.Height];

            for (int n = 0; n < picture.GetLength(0); n++)
                for (int m = 0; m < picture.GetLength(1); m++)
                    picture[n, m] = imgMap.GetPixel(n, m);
            GenerateExtendedPicture();
        }

        void GenerateExtendedPicture()
        {
            if (filter == null) return;
            int expandRow = filter.GetN / 2;
            int expandCol = filter.GetM / 2;

            extendedPicture = new Color[
                picture.GetLength(0) + 2 * expandRow,
                picture.GetLength(1) + 2 * expandCol];

            for (int i = 0; i < picture.GetLength(0); i++)
                for (int j = 0; j < picture.GetLength(1); j++)
                    extendedPicture[i + expandRow, j + expandCol] = picture[i, j];
        }

        static public Picture[] NewPictures(string[] path_, Filter filter_)
        {

            Picture[] result = new Picture[path_.Length];
            Parallel.For(0, path_.Length, ops, i =>
            {
                result[i] = new Picture();
                result[i].NewPicture(path_[i]);
                if (filter_ != null)
                {
                    result[i].filter = filter_;
                    result[i].GenerateExtendedPicture();
                }
            });
            return result;
        }

        static public Image GetImage(Color[,] picture)
        {
            Bitmap bm = new Bitmap(picture.GetLength(0), picture.GetLength(1));

            for (int n = 0; n < picture.GetLength(0); n++)
                for (int m = 0; m < picture.GetLength(1); m++)
                    bm.SetPixel(n, m, picture[n, m]);
            return bm;
        }
    }
}
