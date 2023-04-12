using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    [JsonArray(AllowNullItems = false)]
    public class Output
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string id;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int output;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string group;

        public readonly AbstractDevice device;

        public Output(AbstractDevice device, string id, int output, string group)
        {
            this.device = device;
            this.id = id;
            this.output = output;
            this.group = group;
        }
    }

    public class OutputConverter : JsonConverter
    {
        private readonly AbstractDevice device;

        public OutputConverter(AbstractDevice device)
        {
            this.device = device;
        }

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Output).IsAssignableFrom(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (JToken.ReadFrom(reader) is JObject jObject)
            {
                return new Output(
                    device,
                    (string?)jObject["id"] ?? throw new JsonException("Device was deserialized with null 'id'"),
                    (int?)jObject["output"] ?? throw new JsonException("Device was deserialized with null 'output'"),
                    (string?)jObject["group"] ?? throw new JsonException("Device was deserialized with null 'group'")
                );
            }

            // Return null when the token is not a JObject
            return null;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            // Use default serialization
        }
    }
}
