using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver.Routing
{
	public class InputOutputPort : IInputOutput
	{
		[JsonPropertyName("id")]
		public required string Id { get; init; }

		[JsonPropertyName("type")]
		public required IOType Type { get; init; }

		[JsonPropertyName("group")]
		public required string Group { get; init; }

		[JsonPropertyName("port")]
		public required int Port { get; init; }
	}
}
