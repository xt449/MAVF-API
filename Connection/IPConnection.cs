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
        public abstract byte[] ReadBytes(int maxLength);
        public abstract void WriteBytes(byte[] buffer, int offset, int length);

        public abstract bool Connect();
        public abstract void Disconnect();

        public abstract void Dispose();
    }
}
