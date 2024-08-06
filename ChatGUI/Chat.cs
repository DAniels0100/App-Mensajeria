using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Chat
    {
        private TcpListener listener;
        private ConcurrentDictionary<string, TcpClient> peers = new ConcurrentDictionary<string, TcpClient>();
        private int port;
        private Action<string> updateChat;

        public Chat(int port, Action<string> updateChat)
        {
            this.port = port;
            this.updateChat = updateChat;
        }

        public async Task StartAsync()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            updateChat($"Listening on port {port}");
            _ = Task.Run(ListenForPeerAsync);
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

        public async Task ConnectToPeerAsync(string peerAddress)
        {
            try
            {
                string[] parts = peerAddress.Split(':');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int peerPort))
                {
                    updateChat("Invalid peer address format. Use 'IP:Port'.");
                    return;
                }

                TcpClient peerClient = new TcpClient();
                await peerClient.ConnectAsync(parts[0], peerPort);
                peers.TryAdd(peerAddress, peerClient);
                updateChat($"Connected to peer: {peerAddress}");
            }
            catch (Exception ex)
            {
                updateChat($"Error connecting to peer: {ex.Message}");
            }
        }

        public async Task DisconnectPeerAsync(string peerAddress)
        {
            try
            {
                if (peers.TryRemove(peerAddress, out TcpClient peerClient))
                {
                    peerClient.Close();
                    updateChat($"Disconnected from peer: {peerAddress}");
                }
                else
                {
                    updateChat($"Peer not found: {peerAddress}");
                }
            }
            catch (Exception ex)
            {
                updateChat($"Error disconnecting from peer: {ex.Message}");
            }
        }

        public async Task BroadcastMessageAsync(string message)
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
                    updateChat($"Error sending to {peer.Key}: {ex.Message}");
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
                        updateChat(message);
                    }
                }
            }
            catch (Exception ex)
            {
                updateChat(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }
    }
}