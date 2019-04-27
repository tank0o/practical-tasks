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

        Files files;
        BinaryFormatter formatter = new BinaryFormatter();

        string folderPath = "";
        string folderPathOld;

        Task clientTask;
        CancellationTokenSource tokenSource;
        CancellationToken token;

        private const int port = 8888;
        TcpClient client;
        NetworkStream stream;
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
                Disconnect();
            }
        }

        void MessageBoxShow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (folderPathOld != files.FolderPath)
                {
                    MessageBox.Show("Изменена папка");
                }
                else
                {
                    MessageBox.Show("Изменение в файле");
                }
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

        void StartClient()
        {
            client = new TcpClient();
            try
            {
                client.Connect(textBoxIp.Text, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = textBoxKey.Text;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем метод для получения данных
                ReceiveMessage();
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
                    if (data[0] == 33 && data[1] == 33 && data[2] == 33)
                    {
                        throw new Exception("Сервер принудительно отключил пользователя");
                    }
                    MemoryStream memoryStream = new MemoryStream(data);
                    files = (Files)formatter.Deserialize(memoryStream);
                    folderPathOld = folderPath;
                    folderPath = files.FolderPath;
                    DGFiles();
                    MessageBoxShow();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    Disconnect();
                    break;
                }
            }
        }

        void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            if (tokenSource != null)
                tokenSource.Cancel();
            ButtonStartStop.Invoke((MethodInvoker)(()=>ButtonStartStop.Text = "Start"));
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
