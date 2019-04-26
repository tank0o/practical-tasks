using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Task7Dll;


namespace Task7FormClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Files files;
        static BinaryFormatter formatter = new BinaryFormatter();

        string folderPath = "";
        Task clientTask;
        static CancellationTokenSource tokenSource;
        static CancellationToken token;
        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            if (ButtonStartStop.Text == "Start")
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                clientTask = new Task(() => StartClient(), token);
                clientTask.Start();
                ButtonStartStop.Text = "Stop";
            }
            else
            {
                stream.Close();
                tokenSource.Cancel();
                ButtonStartStop.Text = "Start";
            }
        }

        void DGFiles()
        {
            DataGridFile.Invoke((MethodInvoker)(() => DataGridFile.Rows.Clear()));

            textBoxFolderPath.Invoke((MethodInvoker)(() => textBoxFolderPath.Text = folderPath));

            Task7Dll.File[] f = files.GetFiles;

            for (int i = 0; i < f.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DataGridFile);

                row.Cells[0].Value = f[i].Path;
                row.Cells[1].Value = f[i].Statuses.ToString();
                DataGridFile.Invoke((MethodInvoker)(() => DataGridFile.Rows.Add(row)));
            }
        }

        //------------------------------------------------------------------
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        void StartClient()
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = textBoxKey.Text;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // получение сообщений
        void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024 * 512]; // буфер для получаемых данных
                    do
                    {
                        stream.Read(data, 0, data.Length);
                    }
                    while (stream.DataAvailable);
                    if(data[0] == 33 && data [1] == 33 && data[2] == 33)
                    {
                        throw new Exception("Сервер принудительно отключил пользователя");
                    }
                    MemoryStream memoryStream = new MemoryStream(data);
                    files = (Files)formatter.Deserialize(memoryStream);
                    folderPath = files.FolderPath;
                    DGFiles();
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // SatrtClient();
        }
    }
}
