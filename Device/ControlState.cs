using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlState : IIdentifiable
    {
        public string Id { get; init; }

        /// <summary>
        /// Key: User#Id
        /// Value: Groups that can be controlled by User
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly Dictionary<string, string[]> controlling;
    }
}
