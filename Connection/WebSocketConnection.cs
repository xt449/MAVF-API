using System.Net.WebSockets;
using System.Text;

namespace MAVF.API.Connection
{
	internal class WebSocketConnection : NetworkConnection
	{
		private readonly ClientWebSocket client;
		private readonly Uri uri;

		public WebSocketConnection(string ip, int port, bool secure, string relativeAddress) : base(ip, port)
		{
			client = new ClientWebSocket();
			uri = new Uri($"{(secure ? "wss" : "ws")}://{ip}:{port}/{relativeAddress}");
		}

		public override bool Connect()
		{
			using var source = new CancellationTokenSource(5_000);
			client.ConnectAsync(uri, source.Token).Wait();
			return !source.IsCancellationRequested;
		}

		public override void Disconnect()
		{
			client.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None).Wait();
		}

		public override void Dispose()
		{
			client.Dispose();
		}

		public override byte[] ReadBytes(int maxLength = 4096)
		{
			var buffer = new ArraySegment<byte>(new byte[maxLength]);
			using var source = new CancellationTokenSource(5_000);
			var result = client.ReceiveAsync(buffer, source.Token).Result;

			return result == null ? Array.Empty<byte>() : buffer.Array!;
			// buffer.Array is never null
		}

		public override void WriteBytes(byte[] buffer, int offset, int length)
		{
			using var source = new CancellationTokenSource(5_000);
			client.SendAsync(new ArraySegment<byte>(buffer, offset, length), WebSocketMessageType.Binary, true, source.Token).Wait();
		}

		public override void WriteASCII(string text)
		{
			using var source = new CancellationTokenSource(5_000);
			client.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(text)), WebSocketMessageType.Text, true, source.Token).Wait();
		}
	}
}
