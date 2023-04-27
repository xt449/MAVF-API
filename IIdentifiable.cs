using Newtonsoft.Json;

namespace MILAV.API
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IIdentifiable
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; init; }
    }
}
