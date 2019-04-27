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
        string folderPath = "";
        Files files = new Files(@"C:\Users\Andrew\Documents\PracticaTasks\Algorithms and technologies of parallel programming\Task5");
        FileSystemWatcher watcher = new FileSystemWatcher();

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream filesBit = new MemoryStream();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //watcher.Filter = "*.txt";
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
            Log(e.Name + " :файл изменён");
            DGFiles();
        }
        private void OnCreatedFile(object source, FileSystemEventArgs e)
        {
            files?.CreateFile(e.FullPath);
            Log(e.Name + " :файл создан");
            DGFiles();
        }
        private void OnDeletedFile(object source, FileSystemEventArgs e)
        {
            files?.NewStatus(e.FullPath, FileStatus.Delete);
            Log(e.Name + " :файл удален");
            DGFiles();
        }
        private void OnRenamedFile(object source, RenamedEventArgs e)
        {
            files?.NewStatus(e.OldFullPath, FileStatus.Rename, e.FullPath);
            Log(e.OldName + " :файл переименован на " + e.Name);
            DGFiles();
        }

        void DGFiles()
        {
            filesBit = new MemoryStream();
            formatter.Serialize(filesBit, files);
            byte[] bytes = filesBit.ToArray();
            if (server != null)
                server.BroadcastMessage(bytes);

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

        void DGClients(List<ClientObject> clients)
        {
            DataGridClient.Invoke((MethodInvoker)(() => DataGridClient.Rows.Clear()));

            for (int i = 0; i < clients.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DataGridClient);

                row.Cells[0].Value = clients[i].Id;
                DataGridClient.Invoke((MethodInvoker)(() => DataGridClient.Rows.Add(row)));
            }
        }

        StringBuilder LogSB = new StringBuilder("");
        void Log(string text)
        {
            LogSB.Append(text + "\r\n");
            TextBoxLog.Invoke((MethodInvoker)(() => TextBoxLog.Text = LogSB.ToString()));
        }

        CancellationTokenSource tokenSource;
        CancellationToken token;
        Task serverTask;
        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            if (ButtonStartStop.Text == "Start")
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                serverTask = new Task(() => ServerStart(), token);
                serverTask.Start();
                ButtonStartStop.Text = "Stop";
            }
            else
            {
                ServerStop();
            }
        }

        ServerObject server; // сервер
        Thread listenThread; // потока для прослушивания
        void ServerStart()
        {
            try
            {
                server = new ServerObject();
                server.Log = Log;
                server.DGClient = DGClients;
                int key = (new Random()).Next(100000, 999999);
                Log("Сгенерирован ключ: " + key);
                server.Key = key.ToString();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
            }
        }

        void ServerStop()
        {
            if (server != null)
                server.Disconnect();
            if (tokenSource != null)
                tokenSource.Cancel();
            ButtonStartStop.Text = "Start";
            Log("Сервер остановлен");
        }

        private void ButtonRemoveConnection_Click(object sender, EventArgs e)
        {
            if (server != null)
                server.RemoveConnection(DataGridClient.SelectedCells[0].Value.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerStop();
        }
    }
}
