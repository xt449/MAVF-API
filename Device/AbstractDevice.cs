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
        public string Driver { get; } //=> ((DeviceAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(DeviceAttribute)))?.driver ?? "unknown";

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("ip")]
        public string Ip { get; private set; }

        [JsonProperty("port")]
        public int Port { get; private set; }

        [JsonProperty("protocol")]
        public Protocol Protocol { get; private set; }

        [JsonProperty("room")]
        public string Room { get; private set; }

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
            if (Id == null) throw new JsonException("Device was deserialized with null 'id'");
            if (Ip == null) throw new JsonException("Device was deserialized with null 'ip'");
            if (Port == null) throw new JsonException("Device was deserialized with null 'port'");
            if (Protocol == null) throw new JsonException("Device was deserialized with null 'protocol'");
            if (Room == null) throw new JsonException("Device was deserialized with null 'room'");
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

            switch (Protocol)
            {
                case Protocol.TCP:
                    Connection = new TCPConnection(Ip, Port);
                    break;
                case Protocol.TELNET:
                    Connection = new TelnetConnection(Ip, Port);
                    break;
                case Protocol.HTTP:
                    Connection = new HttpConnection(Ip, Port);
                    break;
                case Protocol.WEBSOCKET:
                    Connection = new WebSocketConnection(Ip, Port);
                    break;
                case Protocol.SSH:
                    Connection = new SSHConnection(Ip, Port);
                    break;
                case Protocol.UDP:
                    Connection = new UDPConnection(Ip, Port);
                    break;
            }
        }

        public void SetControlState(string nextState)
        {
            State = States.FirstOrDefault(cs => cs.Id == nextState);
        }

        public bool CanControlDevice(AbstractDevice abstractDevice)
        {
            return State?.Rooms.Contains(abstractDevice.Room) ?? false;
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
