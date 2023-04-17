using Newtonsoft.Json;

namespace MILAV.API.Device.Routing
{
    public interface IInputOutput : IIdentifiable
    {
        [JsonProperty("type", Required = Required.DisallowNull)]
        public IOType Type { get; init; }

        [JsonProperty("group", Required = Required.DisallowNull)]
        public string Group { get; init; }
    }
}
