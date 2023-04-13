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

        public Dictionary<string, Input> Inputs { get; private set; }

        public Dictionary<string, Output> Outputs { get; private set; }

        public IPConnection Connection { get; private set; }

        public void Initialize(Input[] inputs, Output[] outputs)
        {
            // Only if Connection not yet initialized
            if (Connection == null)
            {
                this.Inputs = inputs.ToDictionary(i => i.id, i => i);
                this.Outputs = outputs.ToDictionary(o => o.id, o => o);

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

                    // Read and map inputs
                    Input[] inputArray;
                    var inputs = jObject["inputs"];
                    if(inputs == null)
                    {
                        inputArray = new Input[0];
                    }
                    else
                    {
                        inputArray = inputs.Select(json => new Input(value, (IOType) Enum.Parse(typeof(IOType), (string)json["type"], true), (string)json["group"], (string)json["id"], (int)json["input"])).ToArray();
                    }

                    // Read and map outputs
                    Output[] outputArray;
                    var outputs = jObject["outputs"];
                    if (outputs == null)
                    {
                        outputArray = new Output[0];
                    }
                    else
                    {
                        outputArray = outputs.Select(json => new Output(value, (IOType)Enum.Parse(typeof(IOType), (string)json["type"], true), (string)json["group"], (string)json["id"], (int)json["output"])).ToArray();
                    }

                    // Populate the default value with the values from the jObject
                    serializer.Populate(jObject.CreateReader(), value);

                    // Finish initialization of device
                    value.Initialize(inputArray, outputArray);

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
