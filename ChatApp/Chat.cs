using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;


class Chat
{
    //declaracion de objetos
    private TcpClient client;
    private TcpListener listener;
    private NetworkStream stream;
    private Thread listenThread;
    private List<NetworkStream> clientStreams = new List<NetworkStream>();
    private List<Thread> clientThreads = new List<Thread>();
    private ConcurrentDictionary<string, TcpClient> peers = new ConcurrentDictionary<string, TcpClient>();
    private int port;

    public Chat(int port)
    {
        this.port = port;
    }

    public async Task StartAsync()
    {
        listener= new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"listening on port {port}");
        _ = Task.Run(ListenForPeerAsync);
        _ = Task.Run(SendMessageAync);
        await Task.Delay(-1);
    }

    private async Task ListenForPeerAsync()
    {
        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            _ = HandlePeerAsync(client);
        }
    }

    private async Task SendMessageAync()
    {
        while (true) 
        {
            Console.Write("Enter a message: ");
            string input = Console.ReadLine();
            if (input.StartsWith("connect ", StringComparison.OrdinalIgnoreCase))
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 2)
                {
                    await ConnectToPeerAsync(parts[1]);
                }
            }
            else
            {
                await BroadcastMessageAsync(input);
            }
        }
    }
    private async Task ConnectToPeerAsync(string peerAddress)
    {
        try
        {
            string[] parts = peerAddress.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int peerPort))
            {
                Console.WriteLine("Invalid peer address format. Use 'IP:Port'.");
                return;
            }

            TcpClient peerClient = new TcpClient();
            await peerClient.ConnectAsync(parts[0], peerPort);
            peers.TryAdd(peerAddress, peerClient);
            Console.WriteLine($"Connected to peer: {peerAddress}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to peer: {ex.Message}");
        }
    }

    private async Task BroadcastMessageAsync(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        foreach (var peer in peers)
        {
            try
            {
                NetworkStream stream = peer.Value.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending to {peer.Key}: {ex.Message}");
                peers.TryRemove(peer.Key, out _);
            }
        }
    }

    private async Task HandlePeerAsync(TcpClient client)
    {
        try
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("message " + message);
                }
            } 
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            client.Close();
        }
    }
}