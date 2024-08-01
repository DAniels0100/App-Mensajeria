using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class P2PChat
{
    private TcpClient client;
    private TcpListener listener;
    private NetworkStream stream;
    private Thread listenThread;
    private List<NetworkStream> clientStreams = new List<NetworkStream>();
    private List<Thread> clientThreads = new List<Thread>();

    public void StartListening(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        listenThread = new Thread(ListenForClients);
        listenThread.Start();
        Console.WriteLine($"Listening on port {port}...");
    }

    private void ListenForClients()
    {
        while (true)
        {
            TcpClient newClient = listener.AcceptTcpClient();
            Console.WriteLine("Client connected.");
            NetworkStream clientStream = newClient.GetStream();
            clientStreams.Add(clientStream);
            Thread clientThread = new Thread(HandleClientComm);
            clientThread.Start(newClient);
            clientThreads.Add(clientThread);
        }
    }

    private void HandleClientComm(object client_obj)
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
            Console.WriteLine("Client: " + clientMessage);

            // Broadcast message to all clients (including the server itself)
            BroadcastMessage(clientMessage, stream);
        }

        tcpClient.Close();
    }

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
                    // Handle or log exception if a client stream is not available
                }
            }
        }
    }

    public void Connect(string ipAddress, int port)
    {
        client = new TcpClient();
        client.Connect(IPAddress.Parse(ipAddress), port);
        stream = client.GetStream();
        Console.WriteLine($"Connected to {ipAddress}:{port}");

        // Start a thread to listen for messages from the server
        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

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

    public void SendMessage(string message)
    {
        if (stream != null)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}