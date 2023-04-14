using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MILAV.API.Device.USB
{
    public class CustomUSBController : AbstractDevice, IUSBControl
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string requestSetRoute;

        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string responseSetRoute;

        public bool Route(Input input, Output output)
        {
            if (input.device != output.device)
            {
                return false;
            }

            if (Connection.Connect())
            {
                // RegEx formatting ($1)?
                // C# formatting ({0})?
                // or something else?
                Connection.WriteASCII(requestSetRoute.Replace("$1", input.input.ToString()).Replace("$2", output.output.ToString()));

                return Regex.Match(Connection.ReadASCII(), responseSetRoute).Success;
            }

            return false;
        }
    }
}
