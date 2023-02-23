using BroadcastingServer;

var broadcastServer = new BroadcastServer("127.0.0.1", 11000);

while (true)
{
    broadcastServer.Broadcast(Console.ReadLine());
    Thread.Sleep(1000);
}