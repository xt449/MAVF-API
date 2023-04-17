using Newtonsoft.Json;

namespace MILAV.API.Device.Routing
{
    public class InputOutputPort : IInputOutput
    {
        public string Id { get; init; }

        public IOType Type { get; init; }

        public string Group { get; init; }

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly int port;
    }
}
