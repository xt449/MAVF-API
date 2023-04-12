using MILAV.API.Connection;
using MILAV.API.Device;
using Newtonsoft.Json;

namespace MILAV.API
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly string ip;

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        [JsonProperty(Required = Required.DisallowNull)]
        public readonly ControlState[] states;

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        public ControlState? State { get; private set; }

        public void Validate()
        {
            if (ip == null) throw new JsonException("Device was deserialized with null 'ip'");
            if (states == null) throw new JsonException("Device was deserialized with null 'states'");
        }

        public void SetControlState(string nextState)
        {
            State = states.FirstOrDefault(cs => cs.id == nextState);
        }

        public bool CanControlOutput(Output output)
        {
            return State?.groups.Contains(output.group) ?? false;
        }
    }
}
