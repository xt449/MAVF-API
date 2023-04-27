using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.API
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Configuration
    {
        [JsonProperty(Required = Required.Always)]
        public readonly bool debug;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<IDevice>))]
        [JsonProperty(Required = Required.Always)]
        public readonly Dictionary<string, IDevice> devices;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<User>))]
        [JsonProperty(Required = Required.Always)]
        public readonly Dictionary<string, User> users;

        [JsonProperty(Required = Required.Always)]
        public readonly string masterUser;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<ControlState>))]
        [JsonProperty(Required = Required.Always)]
        public readonly Dictionary<string, ControlState> states;

        [JsonProperty(Required = Required.Always)]
        public readonly string defaultState;

        public Configuration(string defaultState)
        {
            this.debug = true;
            this.devices = new Dictionary<string, IDevice>();
            this.users = new Dictionary<string, User>();
            this.masterUser = string.Empty;
            this.states = new Dictionary<string, ControlState>();
            this.defaultState = defaultState;
        }
    }
}
