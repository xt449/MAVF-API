using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.API
{
    public class User : IIdentifiable
    {
        [JsonProperty("ip", Required = Required.Always)]
        public string Id { get; init; }

        /// <summary>
        /// Groups that can be controlled
        /// </summary>
        private string[] controlGroups = new string[0];

        public void SetControlGroups(string[] groups)
        {
            controlGroups = groups;
        }

        public bool CanRouteInput(IInputOutput input)
        {
            return controlGroups.Contains(input.Group);
        }

        public bool CanRouteOutput(IInputOutput output)
        {
            return controlGroups.Contains(output.Group);
        }
    }
}
