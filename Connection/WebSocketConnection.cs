using System.Net.WebSockets;
using System.Text;

namespace MILAV.API.Connection
{
    internal class WebSocketConnection : IPConnection
    {
        private readonly ClientWebSocket websocket;

        public WebSocketConnection(string ip, int port) : base(ip, port)
        {
            websocket = new ClientWebSocket();
        }

        public override bool Connect()
        {
            using var source = new CancellationTokenSource(5_000);
            websocket.ConnectAsync(new Uri($"ws://{ip}:{port}"), source.Token).Wait();
            return !source.IsCancellationRequested;
        }

        public override void Disconnect()
        {
            websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None).Wait();
        }

        public override void Dispose()
        {
            websocket.Dispose();
        }

        public override byte[] ReadBytes(int maxLength = 4096)
        {
            var buffer = new ArraySegment<byte>(new byte[maxLength]);
            using var source = new CancellationTokenSource(5_000);
            var result = websocket.ReceiveAsync(buffer, source.Token).Result;
            if(result == null)
            {
                return new byte[0];
            }
            // This should never be null
            return buffer.Array;
        }

        public override void WriteBytes(byte[] buffer, int offset, int length)
        {
            using var source = new CancellationTokenSource(5_000);
            websocket.SendAsync(new ArraySegment<byte>(buffer, offset, length), WebSocketMessageType.Binary, true, source.Token).Wait();
        }

        public override void WriteASCII(string text)
        {
            using var source = new CancellationTokenSource(5_000);
            websocket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(text)), WebSocketMessageType.Text, true, source.Token).Wait();
        }
    }
}
