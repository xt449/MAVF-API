using System;

namespace MILAV.API.Connection
{
    public abstract class IPConnection : IConnection, IDisposable
    {
        public readonly string ip;
        public readonly int port;

        public IPConnection(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public abstract bool Connect();
        public abstract void Dispose();
        public abstract byte[] ReadBytes(int maxLength);
        public abstract void WriteBytes(byte[] buffer, int offset, int length);
    }
}
