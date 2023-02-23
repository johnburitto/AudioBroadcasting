using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class BroadcastClient : IBroadcastClient
    {
        public IPAddress BroadcastAddress { get; private set; }
        public int Port { get; private set; }

        public BroadcastClient(string ipAddress, int port)
        {
            BroadcastAddress = IPAddress.Parse(ipAddress);
            Port = port;
        }

        public void Listen()
        {
            UdpClient client = new UdpClient(Port);
            IPEndPoint endpoint = new IPEndPoint(BroadcastAddress, Port);

            Console.WriteLine($"[{DateTime.UtcNow}] Wait for broadcast");

            try
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref endpoint);
                    string message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                    Console.WriteLine($"[{DateTime.UtcNow}] Client receive message => \"{message}\"");
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Exceptions has occured: \n {e}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}
