using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using Akka;
using Akka.Routing;
using Akka.Configuration;

using Task5.Classes;


namespace Task5
{
    public partial class Form1 : Form
    {
        Filter currFilter;
        string imagesPath = "";
        string editedPath = "";
        Picture testImage = new Picture();

        Picture[] pictures;

        Stopwatch timer = new Stopwatch();

        Filter eroz = new Filter(new double[,]
                 {
                    { 1, 1, 0, 1, 1 },
                    { 1, 0, 0, 0, 1 },
                    { 0, 0, 0, 0, 0 },
                    { 1, 0, 0, 0, 1 },
                    { 1, 1, 0, 1, 1 }
                 });
        Filter gauss = new Filter(new double[,]
                   {
                    { 0.000789, 0.006581, 0.013347, 0.006581, 0.000789 },
                    { 0.006581, 0.054901, 0.111345, 0.054901, 0.006581 },
                    { 0.013347, 0.111345, 0.225821, 0.111345, 0.013347 },
                    { 0.006581, 0.054901, 0.111345, 0.054901, 0.006581 },
                    { 0.000789, 0.006581, 0.013347, 0.006581, 0.000789 }
                   });
        Filter sharp = new Filter(new double[,]
       {
                    {-1, -1, -1 },
                    {-1,  9, -1 },
                    {-1, -1, -1 }
       });


        public Form1()
        {
            InitializeComponent();
        }

        ParallelOptions ops = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 4
        };

        private void FiltersPath_Bttn_Click(object sender, EventArgs e)
        {

        }

        CancellationTokenSource source = new CancellationTokenSource();
        private void Form1_Load(object sender, EventArgs e)
        {
            testImage.NewPicture("TestImage.jpg");
            FilterPreviewPictureBefore.Image = Picture.GetImage(testImage.ColorMap);

            ops.CancellationToken = source.Token;
            FilterFiltersComboBox.SelectedIndex = 1;

            TabControl.Controls[1].Enabled = false;
            TabControl.Controls[2].Enabled = false;
        }

        void FillMatrixDataGrid(int newN, int newM)
        {
            int rows = newN, cols = newM;



            int oldN = currFilter.GetN;
            int oldM = currFilter.GetM;

            Filter copy = new Filter(currFilter);

            currFilter.SetMatrix(newN, newM);

            newN = newN > oldN ? oldN : newN;
            newM = newM > oldM ? oldM : newM;

            for (int i = 0; i < newN; i++)
                for (int j = 0; j < newM; j++)
                    currFilter[i, j] = copy[i, j];


            FilterDivTextBox.Text = currFilter.Div.ToString();

            FilterMatrixDataGrid.Rows.Clear();

            FilterMatrixDataGrid.ColumnCount = cols;

            for (int i = 0; i < rows; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(FilterMatrixDataGrid);

                for (int j = 0; j < cols; j++)
                    row.Cells[j].Value = currFilter[i, j];

                FilterMatrixDataGrid.Rows.Add(row);
            }
            DrawPreview();
        }

        void DrawPreview()
        {
            ParallelOptions ops2 = new ParallelOptions()
            { MaxDegreeOfParallelism = 8 };
            ParallelLoopResult parResult;
            testImage.Filter = currFilter;
            testImage.FilterColorMap = currFilter.EditImage(testImage, ops2, out parResult);
            FilterPreviewPictureAfter.Image = Picture.GetImage(testImage.FilterColorMap);
        }

        bool readNUD = false;
        private void FilterPowerNNUD_ValueChanged(object sender, EventArgs e)
        {
            if (readNUD) return;
            FillMatrixDataGrid((int)FilterPowerNNUD.Value, (int)FilterPowerMNUP.Value);
        }

        private void FilterMatrixDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double newValue = 0;

            try
            {
                newValue = Double.Parse(FilterMatrixDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch
            {

            }

            currFilter[e.RowIndex, e.ColumnIndex] = newValue;
            FilterDivTextBox.Text = currFilter.Div.ToString();
            FillMatrixDataGrid((int)FilterPowerNNUD.Value, (int)FilterPowerMNUP.Value);
        }

        private void FilterOffsetNUD_ValueChanged(object sender, EventArgs e)
        {
            currFilter.Offset = (double)FilterOffsetNUD.Value;
            DrawPreview();
        }

        private void FilterFiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilterFiltersComboBox.SelectedIndex == 0)
            {
                currFilter = gauss;
            }
            else if (FilterFiltersComboBox.SelectedIndex == 1)
            {
                currFilter = sharp;
            }
            else if (FilterFiltersComboBox.SelectedIndex == 2)
            {
                currFilter = eroz;
            }
            readNUD = true;
            FilterPowerMNUP.Value = currFilter.GetM;
            FilterPowerNNUD.Value = currFilter.GetN;
            readNUD = false;
            FillMatrixDataGrid((int)FilterPowerNNUD.Value, (int)FilterPowerMNUP.Value);
        }

        private void SettingsThreadCountNUD_ValueChanged(object sender, EventArgs e)
        {
            ops.MaxDegreeOfParallelism = (int)SettingsThreadCountNUD.Value;
        }

        private void SettingsImagesPathBttn_Click(object sender, EventArgs e)
        {
            string newPath = "";

            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                ofd.SelectedPath = imagesPath;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        newPath = ofd.SelectedPath;
                    }
                    catch
                    {
                        MessageBox.Show("Incorrect Data! ");
                    }
                }
            }

            if (sender == SettingsImagesPathBttn)
            {
                imagesPath = newPath;
                SettingsImagesPathTB.Text = imagesPath;

                source = new CancellationTokenSource();
                BackgroundWorker backgroundWorkerFiles = new BackgroundWorker();
                backgroundWorkerFiles.WorkerSupportsCancellation = true;
                backgroundWorkerFiles.DoWork += readPicturesInTheFolder_DoWork;

                backgroundWorkerFiles.RunWorkerAsync();
            }
            else if (sender == SettingsEditedPathBttn)
            {
                editedPath = newPath;
                SettingsEditedPathTB.Text = editedPath;
            }
        }

        private void readPicturesInTheFolder_DoWork(object sender, DoWorkEventArgs e)
        {
            SettingsBackBTN.Invoke((MethodInvoker)(() => SettingsBackBTN.Enabled = false));
            SettingsNextBTN.Invoke((MethodInvoker)(() => SettingsNextBTN.Enabled = false));

            var files = Directory.GetFiles(imagesPath);
            pictures = Picture.NewPictures(files, currFilter);

            DrawSettingPreview();

            SettingsBackBTN.Invoke((MethodInvoker)(() => SettingsBackBTN.Enabled = true));
            SettingsNextBTN.Invoke((MethodInvoker)(() => SettingsNextBTN.Enabled = true));
        }
        void DrawSettingPreview()
        {
            panel1.Controls.Clear();
            foreach (Control item in panel1.Controls.OfType<PictureBox>())
            {
                panel1.Controls.Remove(item);
            }
            if (pictures != null)
                for (int i = 0; i < pictures.Length; i++)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackColor = Color.Gray;
                    pb.Size = new Size(220, 220);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Location = new Point(5 + 230 * i, 10);
                    pb.Image = Picture.GetImage(pictures[i].ColorMap);
                    panel1.Invoke((MethodInvoker)(() => panel1.Controls.Add(pb)));
                }
        }

        ActorSystem MyActorSystem;
        IActorRef routerActor;
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            Props statsActorProps = Props.Create(() => new StatsActor(this, pictures.Length));
            IActorRef statsActor = MyActorSystem.ActorOf(statsActorProps, "statsActor");

            Props routerActorProps = Props.Create<RouterActor>();
            routerActor = MyActorSystem.ActorOf(routerActorProps, "routerActor");
            routerActor.Tell(new RouterActor.StartWorker(pictures,currFilter,editedPath,statsActor));

            ProcessingStartStopBTN.Invoke((MethodInvoker)(() => ProcessingStartStopBTN.Text = "Start"));
            ProcessingBackBTN.Invoke((MethodInvoker)(() => ProcessingBackBTN.Enabled = true));
        }

        public void UpdateInfo(string fileName, int imagesEdited, double totalTime, double avgTime)
        {
            ProcessingEditingImageTB.Invoke((MethodInvoker)(() => ProcessingEditingImageTB.Text = fileName));
            ProcessingImagesEditedTB.Invoke((MethodInvoker)(() => ProcessingImagesEditedTB.Text = (imagesEdited + 1).ToString()));
            ProcessingImagesLeftTB.Invoke((MethodInvoker)(() => ProcessingImagesLeftTB.Text = (pictures.Length - imagesEdited-1).ToString()));
            ProcessingTotalEditingTimeTB.Invoke((MethodInvoker)(() => ProcessingTotalEditingTimeTB.Text = (totalTime / 1000).ToString()));
            ProcessingAvgEditingTimeTB.Invoke((MethodInvoker)(() => ProcessingAvgEditingTimeTB.Text = (avgTime / 1000).ToString()));
        }

        private void FilterNextButton_Click(object sender, EventArgs e)
        {
            TabControl.Controls[0].Enabled = false;
            TabControl.Controls[1].Enabled = true;
            TabControl.SelectedIndex = 1;
        }

        private void SettingsBackBTN_Click(object sender, EventArgs e)
        {
            TabControl.Controls[1].Enabled = false;
            TabControl.Controls[0].Enabled = true;
            TabControl.SelectedIndex = 0;

            pictures = null;
            SettingsImagesPathTB.Text = "";
            editedPath = "";
            imagesPath = "";
            SettingsEditedPathTB.Text = "";
            DrawSettingPreview();
        }

        private void SettingsNextBTN_Click(object sender, EventArgs e)
        {
            if (editedPath?.Length != 0 && imagesPath?.Length != 0)
            {
                TabControl.Controls[1].Enabled = false;
                TabControl.Controls[2].Enabled = true;
                TabControl.SelectedIndex = 2;
            }
        }

        BackgroundWorker backgroundWorker;
        private void ProcesingStartStopBTN_Click(object sender, EventArgs e)
        {
            if (ProcessingStartStopBTN.Text == "Start")
            {
                source = new CancellationTokenSource();
                ops.CancellationToken = source.Token;

                ProcessingStartStopBTN.Text = "Cancel";

                ProcessingBackBTN.Enabled = false;

                backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerSupportsCancellation = true;
                if (MyActorSystem != null)
                    MyActorSystem.Terminate();
                backgroundWorker.DoWork += backgroundWorker_DoWork;

                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                ProcessingStartStopBTN.Text = "Start";

                ProcessingBackBTN.Enabled = true;

                if (backgroundWorker.IsBusy)
                {
                    source.Cancel();
                    backgroundWorker.CancelAsync();
                }

            }
        }

        private void ProcessingBackBTN_Click(object sender, EventArgs e)
        {
            TabControl.Controls[1].Enabled = true;
            TabControl.Controls[2].Enabled = false;
            TabControl.SelectedIndex = 1;
        }
    }
}
