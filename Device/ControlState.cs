using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlState
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string id;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string[] groups;
    }
}
