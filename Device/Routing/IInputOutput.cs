using Newtonsoft.Json;

namespace MILAV.API.Device.Routing
{
    public interface IInputOutput : IIdentifiable
    {
        [JsonProperty("type", Required = Required.Always)]
        public IOType Type { get; init; }

        [JsonProperty("group", Required = Required.Always)]
        public string Group { get; init; }
    }
}
