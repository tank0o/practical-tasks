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
//using Task7FormClient.Classes;
using Task7Dll;


namespace Task7FormClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const int port = 8888;
        private const string server = "127.0.0.1";
        string key = "123";
        static Files files;
        static BinaryFormatter formatter = new BinaryFormatter();

        void ClientTask()
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024 * 1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry(server);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);

            string message = "dasdads";

            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            MemoryStream memoryStream = new MemoryStream(bytes);
            files = (Files)formatter.Deserialize(memoryStream);
            DGFiles();

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        Task clientTask;
        static CancellationTokenSource tokenSource;
        static CancellationToken token;
        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            if (ButtonStartStop.Text == "Start")
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;
                clientTask = new Task(() => ClientTask(), token);
                clientTask.Start();
                ButtonStartStop.Text = "Stop";
            }
            else
            {
                //server.Stop();
                tokenSource.Cancel();
                ButtonStartStop.Text = "Start";
            }
        }

        void DGFiles()
        {
            //formatter.Serialize(filesBit, files);

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




        //------------------------------------------------------------------
        static string userName;
        private const string host = "127.0.0.1";
        static TcpClient client;
        static NetworkStream stream;

        void SatrtClient()
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                //SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        // отправка сообщений
        static void SendMessage()
        {
            Console.WriteLine("Введите сообщение: ");

            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        // получение сообщений
        void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024 * 1024]; // буфер для получаемых данных
                                                         //StringBuilder builder = new StringBuilder();
                    int i = 0;
                    do
                    {
                        data[i] = (byte)stream.ReadByte();
                        i++;
                        //bytes = stream.Read(data, 0, data.Length);
                        //builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    MemoryStream memoryStream = new MemoryStream(data);
                    files = (Files)formatter.Deserialize(memoryStream);
                    DGFiles();
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Console.ReadLine();
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
            SatrtClient();
        }
    }
}
