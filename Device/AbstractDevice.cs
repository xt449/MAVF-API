using MILAV.API.Connection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API.Device
{
    [JsonConverter(typeof(AbstractDeviceConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AbstractDevice
    {
        [JsonProperty("driver", Required = Required.Default)]
        private string Driver => ((DeviceAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(DeviceAttribute)))?.driver ?? "unknown";

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string id;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string ip;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int port;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly Protocol protocol;

        // Nullable and not required
        [JsonProperty(Required = Required.Default)]
        public readonly Input[]? inputs;

        // Nullable and not required
        [JsonProperty(Required = Required.Default)]
        public readonly Output[]? outputs;

        public IPConnection? Connection { get; private set; }

        public void Initialize()
        {
            // Only if Connection not yet initialized
            if (Connection == null)
            {
                switch (protocol)
                {
                    case Protocol.TCP:
                        Connection = new TCPConnection(ip, port);
                        break;
                    case Protocol.TELNET:
                        Connection = new TelnetConnection(ip, port);
                        break;
                    case Protocol.HTTP:
                        Connection = new HttpConnection(ip, port);
                        break;
                    case Protocol.WEBSOCKET:
                        Connection = new WebSocketConnection(ip, port);
                        break;
                    case Protocol.SSH:
                        Connection = new SSHConnection(ip, port);
                        break;
                    case Protocol.UDP:
                        Connection = new UDPConnection(ip, port);
                        break;
                }
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
                    var value = (AbstractDevice)serializer.ContractResolver.ResolveContract(type).DefaultCreator();

                    // Not thread safe :(
                    lock (Configuration.LOCK)
                    {
                        // Add InputConverter for this device instance
                        var inputConveter = new InputConverter(value);
                        serializer.Converters.Add(inputConveter);

                        // Populate the default value with the values from the jObject
                        serializer.Populate(jObject.CreateReader(), value);

                        // Remove InputConverter for this device instance
                        serializer.Converters.Remove(inputConveter);
                    }

                    // Finish initialization of device
                    value.Initialize();

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
