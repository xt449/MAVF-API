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

        [JsonProperty]
        public readonly string id;

        [JsonProperty]
        public readonly string ip;

        [JsonProperty]
        public readonly int port;

        [JsonProperty]
        public readonly Protocol protocol;

        [JsonProperty]
        public readonly string room;

        /// <summary>
        /// Used to determine which rooms this device can send control actions to
        /// </summary>
        [JsonProperty("states")]
        public ControlState[] States { get; private set; }

        /// <summary>
        /// Used to determine which rooms this device can send control actions to
        /// </summary>
        public ControlState? State { get; private set; }

        public IPConnection? Connection { get; private set; }

        public void InnerValidate()
        {
            if (id == null) throw new JsonException("Device was deserialized with null 'id'");
            if (ip == null) throw new JsonException("Device was deserialized with null 'ip'");
            if (port == null) throw new JsonException("Device was deserialized with null 'port'");
            if (protocol == null) throw new JsonException("Device was deserialized with null 'protocol'");
            if (room == null) throw new JsonException("Device was deserialized with null 'room'");
            if (States == null) throw new JsonException("Device was deserialized with null 'states'");

            Validate();

            InitializeConnection();
        }

        public virtual void Validate() { }

        /// <summary>
        /// Called after immdiately Validate to ensure the Connection property is never null
        /// </summary>
        private void InitializeConnection()
        {
            if (Connection != null)
            {
                // Return if already initialized
                return;
            }

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

        public void SetControlState(string nextState)
        {
            State = States.FirstOrDefault(cs => cs.id == nextState);
        }

        public bool CanControlDevice(AbstractDevice abstractDevice)
        {
            return State?.rooms.Contains(abstractDevice.room) ?? false;
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

                    // Assert
                    ((AbstractDevice)value).InnerValidate();

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
