using MILAV.API.Device;
using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.API
{
    public class User : IIdentifiable
    {
        public string Id { get; init; }

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        [JsonConverter(typeof(IdentifiableCollectionToDictionaryConverter<ControlState>))]
        private readonly Dictionary<string, ControlState> states;

        /// <summary>
        /// Used to determine which groups this device can send control actions to
        /// </summary>
        public ControlState? State { get; private set; }

        public void SetControlState(string nextState)
        {
            State = states[nextState];
        }

        public bool CanRouteInput(IInputOutput input)
        {
            return State?.groups.Contains(input.Group) ?? false;
        }

        public bool CanRouteOutput(IInputOutput output)
        {
            return State?.groups.Contains(output.Group) ?? false;
        }
    }
}
