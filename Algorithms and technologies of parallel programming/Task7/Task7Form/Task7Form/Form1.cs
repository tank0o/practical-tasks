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
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task7Dll;
using Task7Form.Classes;


namespace Task7Form
{
    public partial class Form1 : Form
    {

        static Files files = new Files(@"C:\Users\Andrew\Documents\PracticaTasks\Algorithms and technologies of parallel programming\Task5");
        static FileSystemWatcher watcher = new FileSystemWatcher();

        Task serverTask;

        string ip = "127.0.0.1";
        int port = 8888;

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream filesBit = new MemoryStream();

        public Form1()
        {
            InitializeComponent();
        }

        string folderPath = "";

        private void ButtonPath_Click(object sender, EventArgs e)
        {
            string newPath = "";

            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                ofd.SelectedPath = folderPath;

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
                    finally
                    {
                        files = new Files(newPath);
                        watcher.Path = newPath;
                        watcher.EnableRaisingEvents = true;
                        DGFiles();
                    }
                }
            }
            folderPath = newPath;
            TextBoxPath.Text = folderPath;
        }

        private void OnChangedFile(object source, FileSystemEventArgs e)
        {
            files?.NewStatus(e.FullPath, FileStatus.Changed);
            DGFiles();
        }
        private void OnCreatedFile(object source, FileSystemEventArgs e)
        {
            files?.CreateFile(e.FullPath);
            DGFiles();
        }
        private void OnDeletedFile(object source, FileSystemEventArgs e)
        {
            files?.NewStatus(e.FullPath, FileStatus.Delete);
            DGFiles();
        }
        private void OnRenamedFile(object source, RenamedEventArgs e)
        {
            files?.NewStatus(e.OldFullPath, FileStatus.Rename, e.FullPath);
            DGFiles();
        }

        void DGFiles()
        {
            formatter.Serialize(filesBit, files);

            DataGridFile.Invoke((MethodInvoker)(() => DataGridFile.Rows.Clear()));


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

        private void Form1_Load(object sender, EventArgs e)
        {
            watcher.Filter = "*.txt";
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
            watcher.Changed += OnChangedFile;
            watcher.Created += OnCreatedFile;
            watcher.Deleted += OnDeletedFile;
            watcher.Renamed += OnRenamedFile;

            Log("Приложение запущенно");
        }

        StringBuilder LogSB = new StringBuilder("");
        void Log(string text)
        {
            LogSB.Append(text + "\r\n");
            TextBoxLog.Invoke((MethodInvoker)(() => TextBoxLog.Text = LogSB.ToString()));
        }

        Socket sListener = null;
        void SetverTask()
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry(ip);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // Создаем сокет Tcp/Ip
            sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Log("Ожидаем соединение через порт " + ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Log("Полученный текст: " + data + "\n\n");

                    // Отправляем ответ клиенту
                    //string reply = "Спасибо за запрос в " + data.Length.ToString()
                    //        + " символов";
                    byte[] msg = filesBit.ToArray();
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Log("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }




        static CancellationTokenSource tokenSource;
        static CancellationToken token;
        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            ServerStart();
            //if (ButtonStartStop.Text == "Start")
            //{
            //    tokenSource = new CancellationTokenSource();
            //    token = tokenSource.Token;
            //    serverTask = new Task(() => SetverTask(), token);
            //    serverTask.Start();
            //    ButtonStartStop.Text = "Stop";
            //}
            //else
            //{
            //    sListener.Close();
            //    tokenSource.Cancel();
            //    ButtonStartStop.Text = "Start";
            //    Log("Сервер остановлен");
            //}
        }

        private void DataGridFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        void ServerStart()
        {
            try
            {
                server = new ServerObject();
                server.Log = Log;
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                //Log(ex.Message);
            }
        }
    }
}
