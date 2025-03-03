using MAVF.API.Connection;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	public abstract class AbstractNetworkDriver : IDriver
	{
		[JsonInclude]
		public readonly string ip;

		[JsonInclude]
		public readonly int port;

		[JsonInclude]
		public readonly Protocol protocol;

		[JsonInclude]
		public readonly string? username;

		[JsonInclude]
		public readonly string? password;

		[JsonInclude]
		public readonly string? httpRelativeAddress;

		public NetworkConnection Connection { get; private set; }

		public object Properties { get; init; }

		public void Initialize()
		{
			// Only if Connection not yet initialized
			if (Connection == null)
			{
				switch (protocol)
				{
					case Protocol.TCP:
						Connection = new RawTCPConnection(ip, port);
						break;
					case Protocol.TELNET:
						Connection = new TelnetConnection(ip, port);
						break;
					case Protocol.HTTP:
						Connection = new HttpConnection(ip, port, false, httpRelativeAddress ?? throw new Exception("Missing httpRelativeAddress for HTTP connection"));
						break;
					case Protocol.HTTPS:
						Connection = new HttpConnection(ip, port, true, httpRelativeAddress ?? throw new Exception("Missing httpRelativeAddress for HTTPS connection"));
						break;
					case Protocol.WEBSOCKET:
						Connection = new WebSocketConnection(ip, port, false, httpRelativeAddress ?? "");
						break;
					case Protocol.SSH:
						Connection = new SSHConnection(ip, port, username ?? throw new Exception("Missing username for SSH connection"), password ?? throw new Exception("Missing password for SSH connection"));
						break;
					case Protocol.UDP:
						Connection = new RawUDPConnection(ip, port);
						break;
				}
			}
		}
	}
}
