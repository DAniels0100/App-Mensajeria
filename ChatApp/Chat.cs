using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Chat
{
    //declaracion de objetos
    private TcpClient client;
    private TcpListener listener;
    private NetworkStream stream;
    private Thread listenThread;
    private List<NetworkStream> clientStreams = new List<NetworkStream>();
    private List<Thread> clientThreads = new List<Thread>();

    //metodo que activa el servidor de comunicacion
    public void StartListening(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"listening on port {port}");
        listenThread = new Thread(ListenForClients);
        listenThread.Start();
    }

    //metodo que acepta conexiones de otras instancias
    private void ListenForClients()
    {
        while (true)
        {
            TcpClient newClient = listener.AcceptTcpClient();
            Console.WriteLine("Someone connected.");
            NetworkStream clientStream = newClient.GetStream();
            clientStreams.Add(clientStream);
            Thread clientThread = new Thread(ClientMessages);
            clientThread.Start(newClient);
            clientThreads.Add(clientThread);
        }
    }

    //metodo que lee y codifica mensajes
    private void ClientMessages(object client_obj)
    {
        TcpClient tcpClient = (TcpClient)client_obj;
        NetworkStream stream = tcpClient.GetStream();
        byte[] message = new byte[4096];
        int bytesRead;

        while (true)
        {
            bytesRead = 0;

            try
            {
                bytesRead = stream.Read(message, 0, 4096);
            }
            catch
            {
                break;
            }

            if (bytesRead == 0)
            {
                break;
            }

            ASCIIEncoding encoder = new ASCIIEncoding();
            string clientMessage = encoder.GetString(message, 0, bytesRead);
            Console.WriteLine("Message: " + clientMessage);

            //llamado para comunicar el mensaje a los clientes
            BroadcastMessage(clientMessage, stream);
        }

        tcpClient.Close();
    }

    //metodo que comunica las distintos clientes
    private void BroadcastMessage(string message, NetworkStream excludeStream = null)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(message);

        foreach (NetworkStream clientStream in clientStreams)
        {
            if (clientStream != excludeStream)
            {
                try
                {
                    clientStream.Write(buffer, 0, buffer.Length);
                }
                catch
                {
                }
            }
        }
    }

    //metodo que permite la conexion de clientes
    public void Connect(string ipAddress, int port)
    {
        client = new TcpClient();
        client.Connect(IPAddress.Parse(ipAddress), port);
        stream = client.GetStream();

        // hilo para leer mensajes del server
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    //metodo que lee mensajes enviados
    private void ReceiveMessages()
    {
        byte[] buffer = new byte[4096];
        int bytesRead;

        while (true)
        {
            bytesRead = 0;

            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
            }
            catch
            {
                break;
            }

            if (bytesRead == 0)
            {
                break;
            }

            ASCIIEncoding encoder = new ASCIIEncoding();
            string serverMessage = encoder.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Message: " + serverMessage);
        }
    }

    //metodo que envia los mensajes
    public void SendMessage(string message)
    {
        if (stream != null)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }
        BroadcastMessage(message);
    }
}