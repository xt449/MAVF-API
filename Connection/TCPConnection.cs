using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Sockets;

namespace MILAV.API.Connection
{
    [JsonObject("tcp")]
    public class TCPConnection : IPConnection
    {
        protected readonly TcpClient client;

        public TCPConnection(string ip, int port) : base(ip, port)
        {
            client = new TcpClient();
        }

        public override bool Connect()
        {
            client.Connect(ip, port);
            return client.Connected;
        }

        public override void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }

        public override byte[] ReadBytes(int maxLength)
        {
            byte[] buffer = new byte[maxLength];
            client.GetStream().Read(buffer, 0, maxLength);
            return buffer.Take(maxLength).ToArray();
        }

        public override void WriteBytes(byte[] buffer, int offset, int length)
        {
            client.GetStream().Write(buffer, offset, length);
        }
    }
}
