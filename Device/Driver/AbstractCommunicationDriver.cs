using MAVF.API.Connection;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
    public abstract class AbstractCommunicationDriver<DriverProperties> : IDriver<DriverProperties> where DriverProperties : CommunicationDriverProperties
	{
		[JsonPropertyName("properties")]
		public DriverProperties Properties { get; private init; }

		[JsonIgnore]
		public NetworkConnection Connection { get; private init; }

		public AbstractCommunicationDriver(DriverProperties properties)
		{
			// Ensure that value from JsonSerializer is not null
			Properties = properties ?? throw new ArgumentNullException(nameof(properties));

			Connection = Properties.Protocol switch
			{
				Protocol.TCP => new RawTCPConnection(Properties.Ip, Properties.Port),
				Protocol.TELNET => new TelnetConnection(Properties.Ip, Properties.Port),
				Protocol.HTTP => new HttpConnection(Properties.Ip, Properties.Port, false, Properties.HttpRelativeAddress ?? throw new Exception("Missing httpRelativeAddress for HTTP connection")),
				Protocol.HTTPS => new HttpConnection(Properties.Ip, Properties.Port, true, Properties.HttpRelativeAddress ?? throw new Exception("Missing httpRelativeAddress for HTTPS connection")),
				Protocol.WEBSOCKET => new WebSocketConnection(Properties.Ip, Properties.Port, false, Properties.HttpRelativeAddress ?? ""),
				Protocol.SSH => new SSHConnection(Properties.Ip, Properties.Port, Properties.Username ?? throw new Exception("Missing username for SSH connection"), Properties.Password ?? throw new Exception("Missing password for SSH connection")),
				Protocol.UDP => new RawUDPConnection(Properties.Ip, Properties.Port),
				_ => null!,// TODO
			};
		}
	}
}
