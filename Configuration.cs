using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.API
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Configuration
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly bool debug;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string defaultState;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<IDevice>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly Dictionary<string, IDevice> devices;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<User>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly Dictionary<string, User> users;

        public Configuration(bool debug, string defaultState)
        {
            this.debug = debug;
            this.defaultState = defaultState;
            this.devices = new Dictionary<string, IDevice>();
            this.users = new Dictionary<string, User>();
        }
    }
}
