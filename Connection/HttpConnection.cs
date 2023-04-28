namespace MILAV.API.Connection
{
    internal class HttpConnection : NetworkConnection
    {
        private readonly HttpClient client;

        public HttpConnection(string ip, int port, string relativeAddress) : base(ip, port)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"http://{ip}:{port}/{relativeAddress}");
        }

        public override bool Connect()
        {
            // Unused for HTTP
            return true;
        }

        public override void Disconnect()
        {
            // Unused for HTTP
        }

        public override void Dispose()
        {
            client.Dispose();
        }

        public override byte[] ReadBytes(int maxLength = 4096)
        {
            throw new NotImplementedException();
        }

        public override void WriteBytes(byte[] buffer, int offset, int length)
        {
            throw new NotImplementedException();
        }
    }
}
