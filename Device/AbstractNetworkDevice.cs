using MILAV.API.Connection;
using Newtonsoft.Json;

namespace MILAV.API.Device
{
    public abstract class AbstractNetworkDevice : IDevice
    {
        public string Id { get; init; }

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string ip;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int port;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly Protocol protocol;

        public IPConnection Connection { get; private set; }

        public void Initialize()
        {
            // Only if Connection not yet initialized
            if (Connection == null)
            {
                switch (protocol)
                {
                    case Protocol.TCP:
                        Connection = new TCPConnection(ip, port);
                        break;
                    case Protocol.TELNET:
                        Connection = new TelnetConnection(ip, port);
                        break;
                    case Protocol.HTTP:
                        Connection = new HttpConnection(ip, port);
                        break;
                    case Protocol.WEBSOCKET:
                        Connection = new WebSocketConnection(ip, port);
                        break;
                    case Protocol.SSH:
                        Connection = new SSHConnection(ip, port);
                        break;
                    case Protocol.UDP:
                        Connection = new UDPConnection(ip, port);
                        break;
                }
            }
        }
    }
}
