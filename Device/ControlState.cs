using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlState
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("rooms")]
        public string[] Rooms { get; }
    }
}
