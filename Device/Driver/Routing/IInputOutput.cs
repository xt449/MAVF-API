using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver.Routing
{
	public interface IInputOutput : IIdentifiable
	{
		[JsonPropertyName("type")]
		public IOType Type { get; init; }

		[JsonPropertyName("group")]
		public string Group { get; init; }
	}
}
