using MILAV.API.Device;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API
{
    [JsonConverter(typeof(ConfigurationConverter))]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Configuration
    {
        public readonly bool debug;

        public readonly string defaultState;

        public readonly Dictionary<string, AbstractDevice> devices;

        public readonly Dictionary<string, User> users;

        public Configuration(bool debug, string defaultState, AbstractDevice[] devices, User[] users)
        {
            this.debug = debug;
            this.defaultState = defaultState;
            this.devices = devices.ToDictionary(d => d.id, d => d);
            this.users = users.ToDictionary(u => u.ip, u => u);
        }
    }

    public class ConfigurationConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Configuration).IsAssignableFrom(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (JToken.ReadFrom(reader) is JObject jObject)
            {
                return new Configuration(
                    (bool?)jObject["debug"] ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'debug'"),
                    (string?)jObject["defaultState"] ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'defaultState'"),
                    jObject["devices"]?.ToObject<AbstractDevice[]>() ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'devices'"),
                    jObject["users"]?.ToObject<User[]>() ?? throw new JsonException("Unable to deserialize User. Missing or invalid property 'users'")
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
