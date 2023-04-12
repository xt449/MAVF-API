using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Input
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string id;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int input;

        public readonly AbstractDevice device;

        public Input(AbstractDevice device, string id, int input)
        {
            this.device = device;
            this.id = id;
            this.input = input;
        }
    }

    public class InputConverter : JsonConverter
    {
        private readonly AbstractDevice device;

        public InputConverter(AbstractDevice device)
        {
            this.device = device;
        }

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Input).IsAssignableFrom(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (JToken.ReadFrom(reader) is JObject jObject)
            {
                return new Input(
                    device, 
                    (string?)jObject["id"] ?? throw new JsonException("Device was deserialized with null 'id'"), 
                    (int?)jObject["input"] ?? throw new JsonException("Device was deserialized with null 'input'")
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
