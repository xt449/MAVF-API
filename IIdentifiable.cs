using Newtonsoft.Json;

namespace MILAV.API
{
    public interface IIdentifiable
    {
        [JsonProperty("id", Required = Required.DisallowNull)]
        public string Id { get; }
    }
}
