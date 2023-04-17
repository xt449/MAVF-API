using MILAV.API.Device;
using MILAV.API.Device.Routing;
using MILAV.API.Layout;

namespace MILAV.API
{
    public interface IServerAPI
    {
        // Devices

        public IEnumerable<IDevice> GetDevices();

        // Device

        public IDevice? GetDeviceById(string deviceId);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();

        // Routing

        public IEnumerable<IInputOutput>? GetDeviceInputsById(string deviceId);

        public IEnumerable<IInputOutput>? GetDeviceOutputsById(string deviceId);

        public bool TryRoute(string deviceId, IInputOutput input, IInputOutput output);

        public IInputOutput? GetRoute(string deviceId, IInputOutput output);

        // Layouts

        public bool TryLayout(string deviceId, ILayout layout);

        public ILayout? GetLayout(string deviceId);
    }
}
