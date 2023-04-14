using MILAV.API.Device;
using MILAV.API.Device.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API
{
    [JsonConverter(typeof(UserConverter))]
    public class User : IIdentifiable
    {
        private readonly string ip;

        public string Id { get => ip; init { } }

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        private readonly Dictionary<string, ControlState> states;

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        public ControlState? State { get; private set; }

        public User(string ip, ControlState[] states)
        {
            this.ip = ip;
            this.states = states.ToDictionary(s => s.id, s => s);
        }

        public void SetControlState(string nextState)
        {
            State = states[nextState];
        }

        public bool CanRouteInput(IInputOutput input)
        {
            return State?.groups.Contains(input.Group) ?? false;
        }

        public bool CanRouteOutput(IInputOutput output)
        {
            return State?.groups.Contains(output.Group) ?? false;
        }
    }

    public class UserConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(User).IsAssignableFrom(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (JToken.ReadFrom(reader) is JObject jObject)
            {
                return new User(
                    (string?)jObject["ip"] ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'ip'"),
                    jObject["states"]?.ToObject<ControlState[]>() ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'states'")
                );
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
