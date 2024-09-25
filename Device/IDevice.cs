using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MAVF.API.Device
{
	[JsonConverter(typeof(DeviceConverter))]
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDevice : IIdentifiable
	{
		[JsonProperty("driver", Required = Required.Default)]
		public string Driver => ((DeviceAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(DeviceAttribute)))?.driver ?? "unknown";

		public void Initialize();
	}

	public class DeviceConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			// True for types that are castable to IDevice and have the DeviceAttribute
			return typeof(IDevice).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(DeviceAttribute)) != null;
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			if (JToken.ReadFrom(reader) is JObject jObject)
			{
				// Get the type device type that matches the "driver" properties of the JSON object
				if (DeviceRegistry.TryGet((string?)jObject["driver"] ?? throw new JsonException($"Unable to deserialize device. Missing driver property"), out Type? type))
				{
					// Call the default "creator" used by Newtonsoft when deserializing
					var value = (IDevice?)serializer.ContractResolver.ResolveContract(type).DefaultCreator?.Invoke() ?? throw new JsonException($"Unable to deserialize device. DefaultCreator is null for driver '{(string?)jObject["driver"]}'");

					// Populate the default value with the values from the jObject
					serializer.Populate(jObject.CreateReader(), value);

					// Finish initialization of device
					value.Initialize();

					return value;
				}

				throw new JsonException($"Unable to deserialize device. Invalid driver '{(string?)jObject["driver"]}'");
			}

			// Return null when the token is not a JObject
			return null;
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			// default
		}
	}
}
