using System.Net.Sockets;

namespace InputScanner.WebSocket
{
    public class WebSocketSession
    {
        internal WebSocketSession(Socket client)
        {
            Socket = client;
        }

        internal Socket Socket { get; private set; }
    }
}
