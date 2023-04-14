using Newtonsoft.Json;

namespace MILAV.API.Device
{
    public interface IRouteControl
    {
        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<Input>))]
        [JsonProperty("inputs", Required = Required.DisallowNull)]
        public Dictionary<string, Input> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<Output>))]
        [JsonProperty("outputs", Required = Required.DisallowNull)]
        public Dictionary<string, Output> Outputs { get; init; }

        bool Route(Input input, Output output);
    }
}
