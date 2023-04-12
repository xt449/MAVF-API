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
        [JsonProperty("states", Required = Required.DisallowNull)]
        public ControlState[] States { get; private set; }

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        public ControlState? State { get; private set; }


        public void Validate()
        {
            if (ip == null) throw new JsonException("Device was deserialized with null 'ip'");
            if (States == null) throw new JsonException("Device was deserialized with null 'states'");
        }

        public void SetControlState(string nextState)
        {
            State = States.FirstOrDefault(cs => cs.id == nextState);
        }

        public bool CanControlOutput(Output output)
        {
            return State?.groups.Contains(output.group) ?? false;
        }
    }
}
