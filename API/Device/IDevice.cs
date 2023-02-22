﻿using MILAV.API.Connection;
using MILAV.JSON;
using Newtonsoft.Json;

namespace MILAV
{
    public interface IDevice
    {
        [JsonProperty("room")]
        string Room { get; }

        [JsonProperty("id")]
        string Id { get; }

        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("communication")]
        [JsonConverter(typeof(IConnectionConverter))]
        IConnection Communication { get; }
    }
}
