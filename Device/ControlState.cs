using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlState : IIdentifiable
    {
        public string Id { get; init; }

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string[] groups;
    }
}
