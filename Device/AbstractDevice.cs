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
        public string Ip { get; }

        [JsonProperty("port")]
        public int Port { get; }

        [JsonProperty("protocol")]
        public Protocol Protocol { get; }

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
            switch (Protocol)
            {
                case Protocol.TCP:
                    Connection = new TCPConnection(Ip, (int)Port);
                    break;
                case Protocol.TELNET:
                    Connection = new TelnetConnection(Ip, (int)Port);
                    break;
                case Protocol.HTTP:
                    Connection = new HttpConnection(Ip, (int)Port);
                    break;
                case Protocol.WEBSOCKET:
                    Connection = new WebSocketConnection(Ip, (int)Port);
                    break;
                case Protocol.SSH:
                    Connection = new SSHConnection(Ip, (int)Port);
                    break;
                case Protocol.UDP:
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
            if (JToken.ReadFrom(reader) is JObject jObject)
            {
                // Ensure that the DeviceRegistry has been initialized before accessing it
                DeviceRegistry.Initialize();

                // Get the type device type that matches the "type" and "driver" properties of the JSON object
                if (DeviceRegistry.TryGet((string?)jObject["driver"], out Type? type))
                {
                    // Call the default "creator" used by Newtonsoft when deserializing
                    var value = serializer.ContractResolver.ResolveContract(type).DefaultCreator();
                    // Populate the default value with the values from the jObject
                    serializer.Populate(jObject.CreateReader(), value);

                    return value;
                }
            }

            // Return null when the token is not a JObject or when driver id is not valid
            return null;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            // Use default serialization
        }
    }
}
