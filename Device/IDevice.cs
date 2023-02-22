using MILAV.API.Connection;
using Newtonsoft.Json;

namespace MILAV.API.Device
{
    [JsonConverter(typeof(IDeviceConverter))]
    public interface IDevice
    {
        [JsonProperty("room")]
        string Room { get; }

        [JsonProperty("id")]
        string Id { get; }

        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("communication")]
        IConnection Communication { get; }
    }
}
