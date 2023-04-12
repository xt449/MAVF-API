using MILAV.API;
using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.Config
{
    // Using properties instead of fields in this JsonObject for initializing with default values
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [JsonArray(AllowNullItems = false)]
    public class Configuration
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly bool debug = false;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string defaultState = "";

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly AbstractDevice[] devices = new AbstractDevice[0];

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly User[] users = new User[0];

        // (De)Serialization lock
        public static readonly object LOCK = new object();
    }
}
