using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAVF.API.Connection
{
	[JsonConverter(typeof(ProtocolConverter))]
	public enum Protocol
	{
		TCP, TELNET, HTTP, HTTPS, WEBSOCKET, SSH, UDP
	}

	internal class ProtocolConverter : JsonConverter<Protocol>
	{
		public override Protocol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var rootElement = JsonElement.ParseValue(ref reader);

			var stringValue = rootElement.GetString();

			if (!Enum.TryParse<Protocol>(stringValue, out var value))
			{
				throw new Exception("Unable to deserialize protocol. Invalid element value.");
			}

			return value;
		}

		public override void Write(Utf8JsonWriter writer, Protocol value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
