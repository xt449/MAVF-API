using MAVF.API.Connection;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
    public record CommunicationDriverProperties
	{
		[JsonPropertyName("ip")]
		public required string Ip { get; init; }

		[JsonPropertyName("port")]
		public required int Port { get; init; }

		[JsonPropertyName("protocol")]
		public required Protocol Protocol { get; init; }

		[JsonPropertyName("username")]
		public string? Username { get; init; }

		[JsonPropertyName("password")]
		public string? Password { get; init; }

		[JsonPropertyName("httpRelativeAddress")]
		public string? HttpRelativeAddress { get; init; }
	}
}
