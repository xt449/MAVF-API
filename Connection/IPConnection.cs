using System.Text;

namespace MILAV.API.Connection
{
    public abstract class IPConnection : IDisposable
    {
        public readonly string ip;
        public readonly int port;

        public IPConnection(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public abstract byte[] ReadBytes(int maxLength = 4096);

        public string ReadASCII(int maxLength = 4096)
        {
            var data = ReadBytes(maxLength);
            return Encoding.ASCII.GetString(data, 0, data.Length);
        }

        public abstract void WriteBytes(byte[] buffer, int offset, int length);
        public void WriteASCII(string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
            WriteBytes(data, 0, data.Length);
        }

        public abstract bool Connect();
        public abstract void Disconnect();

        public abstract void Dispose();
    }
}
