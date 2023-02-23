using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BroadcastingServer
{
    internal class BroadcastServer : IBroadcastServer
    {
        public readonly Socket Socket;
        public IPAddress BroadcastAddress { get; private set; }
        public int Port { get; private set; }

        public BroadcastServer(string ipAddress, int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            BroadcastAddress = IPAddress.Parse(ipAddress);
            Port = port;
        }

        public void Broadcast(string? fileName)
        {
            byte[] message = Encoding.UTF8.GetBytes(fileName == null ? "" : fileName);
            IPEndPoint endpoint = new IPEndPoint(BroadcastAddress, Port);

            Socket.SendTo(message, endpoint);

            Console.Clear();
            Console.WriteLine($"[{DateTime.UtcNow}] Message send to the broadcast address");
        }
    }
}
