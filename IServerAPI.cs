using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV.API
{
    public interface IServerAPI
    {
        // Devices

        public IEnumerable<AbstractDevice> GetDevices();

        // Device

        public AbstractDevice? GetDeviceById(string id);

        // Should this be hidden?
        public string? GetDeviceIpById(string id);

        // Should this be hidden?
        public int? GetDevicePortById(string id);

        // Should this be hidden?
        public Protocol? GetDeviceProtocolById(string id);

        public IEnumerable<Input>? GetDeviceInputsById(string id);

        public IEnumerable<Output>? GetDeviceOutputsById(string id);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();
    }
}
