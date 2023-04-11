using MILAV.API.Connection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API.Device
{
    [JsonConverter(typeof(AbstractDeviceConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AbstractDevice
    {
        [JsonProperty("driver")]
        public string Driver => ((DeviceAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(DeviceAttribute)))?.driver ?? "unknown";

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("ip")]
        public string? Ip { get; }

        [JsonProperty("port")]
        public int? Port { get; }

        [JsonProperty("protocol")]
        public Protocol? Protocol { get; }

        [JsonProperty("room")]
        public string Room { get; }

        [JsonProperty("states")]
        public ControlState[] States { get; }

        [JsonIgnore]
        public ControlState? State { get; set; }

        [JsonIgnore]
        public IPConnection? Connection { get; private set; }

        public void InitializeConnection()
        {
            if (Ip == null || Port == null || Protocol == null)
            {
                Connection = null;
                return;
            }

            switch (Protocol)
            {
                case API.Connection.Protocol.TCP:
                    Connection = new TCPConnection(Ip, (int)Port);
                    break;
                case API.Connection.Protocol.TELNET:
                    Connection = new TelnetConnection(Ip, (int)Port);
                    break;
                case API.Connection.Protocol.HTTP:
                    Connection = new HttpConnection(Ip, (int)Port);
                    break;
                case API.Connection.Protocol.WEBSOCKET:
                    Connection = new WebSocketConnection(Ip, (int)Port);
                    break;
                case API.Connection.Protocol.SSH:
                    Connection = new SSHConnection(Ip, (int)Port);
                    break;
                case API.Connection.Protocol.UDP:
                    Connection = new UDPConnection(Ip, (int)Port);
                    break;
            }
        }
    }

    public class AbstractDeviceConverter : JsonConverter
    {
        public override bool CanWrite => false;

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
            // Use default serialization
        }
    }
}
