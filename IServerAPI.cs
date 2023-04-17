using MILAV.API.Device;
using MILAV.API.Device.Routing;

namespace MILAV.API
{
    public interface IServerAPI
    {
        // Devices

        public IEnumerable<IDevice> GetDevices();

        // Device

        public IDevice? GetDeviceById(string deviceId);

        public IEnumerable<IInputOutput>? GetDeviceInputsById(string deviceId);

        public IEnumerable<IInputOutput>? GetDeviceOutputsById(string deviceId);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();

        // Routing

        public bool TryRoute(string deviceId, IInputOutput input, IInputOutput output);

        public IInputOutput? GetRoute(string deviceId, IInputOutput output);
    }
}
