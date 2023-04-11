using MILAV.API.Connection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API.Device
{
    [JsonConverter(typeof(AbstractDeviceConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AbstractDevice
    {
        [JsonProperty("id")]
        string Id { get; }

        [JsonProperty("driver")]
        string Driver { get; }

        [JsonProperty("ip")]
        string Ip { get; }

        [JsonProperty("port")]
        int Port { get; }

        [JsonProperty("protocol")]
        Protocol Protocol { get; }

        [JsonProperty("room")]
        string Room { get; }

        [JsonProperty("states")]
        ControlState[] States { get; }

        [JsonIgnore]
        ControlState? State { get; set; }
    }

    public class AbstractDeviceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // True for types that are castable to IDevice and have the DeviceAttribute
            return typeof(AbstractDevice).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(DeviceAttribute)) != null;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (token is JObject jObject)
            {
                // Ensure that the DeviceRegistry has been initialized before accessing it
                DeviceRegistry.Initialize();

                // Get the type device type that matches the "type" and "driver" properties of the JSON object
                if (DeviceRegistry.TryGet((string?)jObject["driver"], out Type? type))
                {
                    // Use the default deserializer for the specific type found
                    return type == null ? null : JsonConvert.DeserializeObject(token.ToString(Formatting.None), type);
                }
            }

            // Fail when the token is not a JObject
            return null;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // Get the basic JSON object from the value
            var token = JObject.FromObject(value);
            // Add the "driver" property from the `driver` field in the DeviceAttribute which will be used for deserialization
            token["driver"] = ((DeviceAttribute?)Attribute.GetCustomAttribute(value.GetType(), typeof(DeviceAttribute)))?.driver;
            token.WriteTo(writer);
        }
    }
}
