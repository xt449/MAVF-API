﻿using MILAV.API.Device.Routing;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.API.Device.USB
{
    public class CustomUSBController : AbstractDevice, IUSBControl<InputOutputPort, InputOutputPort>
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetRoute;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseSetRoute;

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, InputOutputPort> Inputs { get; init; }

        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<InputOutputPort>))]
        [JsonProperty(Required = Required.DisallowNull)]
        public Dictionary<string, InputOutputPort> Outputs { get; init; }

        bool IRouteControl<InputOutputPort, InputOutputPort>.ExecuteRoute(InputOutputPort input, InputOutputPort output)
        {
            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestSetRoute.Replace("$1", input.port.ToString()).Replace("$2", output.port.ToString()));

                return Regex.Match(Connection.ReadASCII(), responseSetRoute).Success;
            }

            return false;
        }
    }
}
