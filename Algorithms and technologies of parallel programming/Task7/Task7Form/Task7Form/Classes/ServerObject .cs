using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task7Form.Classes
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        ServerObject server; // объект сервера
        public string key = ""; //ключ безопастности

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
            Stream = client.GetStream();
        }

        public void Process()
        {
            try
            {
                // получаем имя пользователя
                string message = GetMessage();
                key = message;
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        // чтение входящего сообщения и преобразование в строку
        private string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }

    public class ServerObject
    {
        static TcpListener tcpListener; // сервер для прослушивания
        List<ClientObject> clients = new List<ClientObject>(); // все подключения

        public delegate void LOG(string text);
        LOG log;
        public LOG Log
        {
            set { log = value; }
            get { return log; }
        }

        public delegate void DGCLIENT(List<ClientObject> clients);
        DGCLIENT dGClient;
        public DGCLIENT DGClient
        {
            set { dGClient = value; }
            get { return dGClient; }
        }

        string key = "";
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        protected internal void AddConnection(ClientObject clientObject)
        {
            log("Клиент подключен");
            clients.Add(clientObject);
            DGClient(clients);
        }
        public void RemoveConnection(string id)
        {
            try
            {
                // получаем по id закрытое подключение
                ClientObject client = clients.FirstOrDefault(c => c.Id == id);
                // и удаляем его из списка подключений
                if (client != null)
                {
                    clients.Remove(client);
                    client.Stream.Write(new byte[] { 33, 33, 33 }, 0, 3);
                }
            }
            catch{}
            dGClient(clients);
        }

        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                log("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(byte[] data)
        {
            try
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].key == key)
                        clients[i].Stream.Write(data, 0, data.Length); //передача данных
                }
            }
            catch(Exception e)
            {
                
            }
        }
        // отключение всех клиентов
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
               RemoveConnection(clients[i].Id); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }
    }
}