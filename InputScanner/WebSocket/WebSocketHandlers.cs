namespace InputScanner.WebSocket
{
    public delegate void WebSocketConnected(WebSocketSession session);
    public delegate void WebSocketReceivedString(WebSocketSession session, string data);
    public delegate void WebSocketReceivedBytes(WebSocketSession session, byte[] data);
    public delegate void WebSocketClosed(WebSocketSession session);
}
