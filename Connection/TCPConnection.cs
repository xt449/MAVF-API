using System.Net.Sockets;

namespace MILAV.API.Connection
{
    //[JsonObject("tcp")]
    public class TCPConnection : NetworkConnection
    {
        protected readonly TcpClient client;

        public bool Connected => client.Connected;

        public TCPConnection(string ip, int port) : base(ip, port)
        {
            client = new TcpClient();
        }

        public override byte[] ReadBytes(int maxLength = 4096)
        {
            byte[] buffer = new byte[maxLength];
            client.GetStream().Read(buffer, 0, maxLength);
            return buffer.Take(maxLength).ToArray();
        }

        public override void WriteBytes(byte[] buffer, int offset, int length)
        {
            client.GetStream().Write(buffer, offset, length);
        }

        public override bool Connect()
        {
            if (client.Connected)
            {
                return true;
            }

            client.Connect(ip, port);
            return client.Connected;
        }

        public override void Disconnect()
        {
            client.Close();
        }

        public override void Dispose()
        {
            client.Dispose();
        }
    }
}
