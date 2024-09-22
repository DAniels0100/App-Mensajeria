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

        //Metodo que inicia la conexion
        public async Task StartAsync()
        {
            listener = new TcpListener(IPAddress.Any, port); //creacion tcpListener
            listener.Start();
            updateChat($"Listening on port {port}");
            _ = Task.Run(ListenForPeerAsync);
            await Task.Delay(-1);
        }
        //metodo que busca y escucha otros puertos
        private async Task ListenForPeerAsync()
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();// se espera a que se conecte otro client
                _ = HandlePeerAsync(client);
            }
        }
        //metodo que maneja la conexion con otra instancia
        public async Task ConnectToPeerAsync(string peerAddress)
        {
            try
            {
                string[] parts = peerAddress.Split(':');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int peerPort)) // verificar que el puerto fue bien ingresado
                {
                    updateChat("Invalid peer address format. Use 'IP:Port'.");
                    return;
                }

                TcpClient peerClient = new TcpClient();
                await peerClient.ConnectAsync(parts[0], peerPort);
                peers.TryAdd(peerAddress, peerClient); // se anade la conexion entre clients
                updateChat($"Connected to peer: {peerAddress}");
            }
            catch (Exception ex)
            {
                updateChat($"Error connecting to peer: {ex.Message}");
            }
        }
        //metodo que desconecta las instancias ya conectadas
        public async Task DisconnectPeerAsync(string peerAddress)
        {
            try
            {
                if (peers.TryRemove(peerAddress, out TcpClient peerClient)) // se remueve la conexion entre clients
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
        //metodo que envia mensaje
        public async Task BroadcastMessageAsync(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message); //guardar el mensaje en bytes
            foreach (var peer in peers)
            {
                try
                {
                    NetworkStream stream = peer.Value.GetStream();
                    await stream.WriteAsync(data, 0, data.Length);//enviar mensaje
                }
                catch (Exception ex)
                {
                    updateChat($"Error sending to {peer.Key}: {ex.Message}");
                    peers.TryRemove(peer.Key, out _);
                }
            }
        }
        //metodo del manejo entre clients
        private async Task HandlePeerAsync(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);//leer mensaje entrante
                        if (bytesRead == 0) break;
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);//convertirlo a string
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