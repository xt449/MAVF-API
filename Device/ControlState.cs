using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlState
    {
        [JsonProperty]
        public readonly string id;

        [JsonProperty]
        public readonly string[] rooms;
    }
}
