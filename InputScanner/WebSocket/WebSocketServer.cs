using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace InputScanner.WebSocket
{
    public class WebSocketServer
    {
        // from qwebsocketprotocol_p.h
        const int opCodeContinue = 0x0;
        const int opCodeText = 0x1;
        const int opCodeBinary = 0x2;
        const int opCodeReserved3 = 0x3;
        const int opCodeReserved4 = 0x4;
        const int opCodeReserved5 = 0x5;
        const int opCodeReserved6 = 0x6;
        const int opCodeReserved7 = 0x7;
        const int opCodeClose = 0x8;
        const int opCodePing = 0x9;
        const int opCodePong = 0xA;
        const int opCodeReservedB = 0xB;
        const int opCodeReservedC = 0xC;
        const int opCodeReservedD = 0xD;
        const int opCodeReservedE = 0xE;
        const int opCodeReservedF = 0xF;

        private int port;
        private Socket server;
        private bool terminate;

        private List<WebSocketSession> sessions;

        public event WebSocketConnected Connected;
        public event WebSocketClosed Closed;

        public WebSocketServer(int port)
        {
            this.port = port;

            terminate = true;
        }

        public void Start()
        {
            if (server != null)
            {
                return;
            }

            terminate = false;

            sessions = new List<WebSocketSession>();

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Loopback, port));
            server.Listen(1);

            NewAccept();
        }

        public void Stop()
        {
        }

        public void Send(WebSocketSession session, string data)
        {
            byte[] payload = Encoding.UTF8.GetBytes(data);
            List<byte> header;

            header = new List<byte>();
            header.Add(opCodeText | 0x80);

            if (payload.Length <= 125)
            {
                header.Add((byte)payload.Length);
            }
            else if (payload.Length <= 0xFFFF)
            {
                byte[] length = BitConverter.GetBytes((ushort)payload.Length);

                Array.Reverse(length);

                header.Add(126);
                header.AddRange(length);
            }
            else if (payload.LongLength <= 0x7FFFFFFFFFFFFFFFL)
            {
                byte[] length = BitConverter.GetBytes((ulong)payload.LongLength);

                Array.Reverse(length);

                header.Add(127);
                header.AddRange(length);
            }
            else
            {
                throw new ArgumentOutOfRangeException("data length is too long");
            }

            session.Socket.Send(header.ToArray());

            int written = 0;
            while (written < payload.Length)
            {
                int bytes = session.Socket.Send(payload, written, payload.Length - written, SocketFlags.None);
                if (bytes > 0)
                {
                    written += bytes;
                }
                else
                {
                    session.Socket.Close();
                }
            }
        }

        private void NewAccept()
        {
            server.BeginAccept(OnAccept, null);
        }

        private void NewClient(Socket client)
        {
            ReceiveHandshake(client, new StringBuilder());
        }

        private void NewSession(Socket client)
        {
            WebSocketSession session;

            session = new WebSocketSession(client);
            sessions.Add(session);

            Connected?.Invoke(session);

            ReceiveWebSocket(client, session);
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket socket = server.EndAccept(ar);

                NewAccept();
                NewClient(socket);
            }
            catch
            {
                terminate = true;
            }

            if (terminate)
            {
                if (server != null)
                {
                    server.Dispose();
                    server = null;
                }
            }
        }

        private void OnReceiveHandshake(IAsyncResult ar)
        {
            var args = ar.AsyncState as object[];
            var client = args[0] as Socket;
            var buffer = args[1] as byte[];
            var content = args[2] as StringBuilder;

            try
            {
                int bytes = client.EndReceive(ar);
                if (bytes > 0)
                {
                    content.Append(Encoding.ASCII.GetString(buffer, 0, bytes));

                    string header = content.ToString();

                    if (header.Length > 4 && header.EndsWith("\r\n\r\n"))
                    {
                        ProcessHandshake(client, header);
                    }
                    else
                    {
                        ReceiveHandshake(client, content);
                    }
                }
                else
                {
                    client.Dispose();
                }
            }
            catch
            {
                client.Dispose();
            }
        }

        private void OnReceiveWebSocket(IAsyncResult ar)
        {
            var args = ar.AsyncState as object[];
            var client = args[0] as Socket;
            var buffer = args[1] as byte[];
            var session = args[2] as WebSocketSession;

            try
            {
                int bytes = client.EndReceive(ar);
                if (bytes > 0)
                {
                    ReceiveWebSocket(client, session);

                    Debug.WriteLine("Receive from WebSocket: " + Convert.ToBase64String(buffer, 0, bytes));
                }
                else
                {
                    client.Dispose();

                    sessions.Remove(session);
                    Closed?.Invoke(session);
                }
            }
            catch
            {
                client.Dispose();

                sessions.Remove(session);
            }
        }

        private void ReceiveHandshake(Socket client, StringBuilder content)
        {
            byte[] buffer = new byte[512];

            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnReceiveHandshake, new object[] { client, buffer, content });
        }

        private void ReceiveWebSocket(Socket client, WebSocketSession session)
        {
            byte[] buffer = new byte[512];

            client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnReceiveWebSocket, new object[] { client, buffer, session });
        }

        private void ProcessHandshake(Socket client, string header)
        {
            string key = null;
            string[] lines = header.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.StartsWith("Sec-WebSocket-Key"))
                {
                    Match match = Regex.Match(line, "Sec-WebSocket-Key: (.*)");
                    if (match.Success)
                    {
                        key = match.Groups[1].Value.Trim();

                        break;
                    }
                }
            }

            if (key != null)
            {
                StringBuilder response;

                string magicString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                string acceptValue = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(key + magicString)));

                response = new StringBuilder();
                response.Append("HTTP/1.1 101 Switching Protocols\r\n");
                response.Append("Connection: Upgrade\r\n");
                response.Append("Upgrade: websocket\r\n");
                response.Append("Sec-WebSocket-Accept: " + acceptValue + "\r\n\r\n");

                Debug.WriteLine(header);

                client.Send(Encoding.ASCII.GetBytes(response.ToString()));

                NewSession(client);
            }
        }
    }
}
