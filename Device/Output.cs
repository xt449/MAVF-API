using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Output : IIdentifiable
    {
        public string Id { get; init; }

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly IOType type;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int port;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string group;

        public Output(string id, IOType type, int port, string group)
        {
            this.Id = id;
            this.type = type;
            this.port = port;
            this.group = group;
        }
    }
}
