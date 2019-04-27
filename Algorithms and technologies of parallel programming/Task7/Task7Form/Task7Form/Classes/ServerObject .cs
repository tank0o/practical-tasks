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
        public string Id { get; private set; }
        public NetworkStream Stream { get; private set; }
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
        void Close()
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
        string key = "";
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public delegate void LOG(string text);
        public LOG Log;

        public delegate void DGCLIENT(List<ClientObject> clients);
        public DGCLIENT DGClient;


        public void AddConnection(ClientObject clientObject)
        {
            Log("Клиент подключен");
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
            catch { }
            DGClient(clients);
        }

        // прослушивание входящих подключений
        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Log("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    clientObject.Process();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
        // трансляция сообщения подключенным клиентам
        public void BroadcastMessage(byte[] data)
        {
            ClientObject client = null;
            try
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    client = clients[i];
                    if (clients[i].key == key)
                        clients[i].Stream.Write(data, 0, data.Length); //передача данных
                }
            }
            catch (Exception e)
            {
                if(client != null)
                    RemoveConnection(client.Id);
            }
        }
        // отключение всех клиентов
        public void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                RemoveConnection(clients[i].Id); //отключение клиента
            }
            //Environment.Exit(0); //завершение процесса
        }
    }
}