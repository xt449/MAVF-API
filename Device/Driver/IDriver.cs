using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	[JsonConverter(typeof(DriverConverter))]
	public interface IDriver
	{
		sealed string Type => ((DriverAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(DriverAttribute), false))?.driver ?? "unknown";

		object? Properties { get; }
	}

	public interface IDriver<DriverProperties> : IDriver where DriverProperties : notnull
	{
		object? IDriver.Properties => Properties;

		new DriverProperties Properties { get; }
	}

	public class DriverConverter : JsonConverter<IDriver>
	{
		public override IDriver Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var rootElement = JsonElement.ParseValue(ref reader);

			if (rootElement.ValueKind != JsonValueKind.Object)
			{
				// Return null when the element is not an object
				throw new JsonException("Unable to deserialize driver. Invalid value type.");
			}

			if (!rootElement.TryGetProperty("type", out var driverTypeElement))
			{
				// Throw exception when the type property is not present
				throw new JsonException("Unable to deserialize driver. Missing type property.");
			}

			var driverTypeString = driverTypeElement.GetString();

			if (driverTypeString == null || !DriverRegistry.TryGet(driverTypeString, out var driverType))
			{
				// Throw exception when the type property is not valid
				throw new JsonException($"Unable to deserialize driver. Invalid type '{driverTypeString}'.");
			}

			// Use default deserializer
			var value = (IDriver?)rootElement.Deserialize(driverType, options);

			if (value == null)
			{
				throw new JsonException($"Unable to deserialize driver. Deserialization returned null for type '{driverTypeString}'.");
			}

			return value;
		}

		public override void Write(Utf8JsonWriter writer, IDriver value, JsonSerializerOptions options)
		{
			// Start
			writer.WriteStartObject();

			writer.WriteString("type", value.Type);

			writer.WritePropertyName("properties");
			// Use default serializer for properties
			JsonSerializer.Serialize(writer, value.Properties, options);

			// Finish
			writer.WriteEndObject();
		}
	}
}
