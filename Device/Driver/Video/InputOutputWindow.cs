using MAVF.API.Device.Driver.Routing;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver.Video
{
	public record InputOutputWindow : IInputOutput
	{
		[JsonPropertyName("id")]
		public required string Id { get; init; }

		[JsonPropertyName("type")]
		public required IOType Type { get; init; }

		[JsonPropertyName("group")]
		public required string Group { get; init; }

		[JsonPropertyName("window")]
		public required int Window { get; init; }
	}
}
