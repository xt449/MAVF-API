using MILAV.API.Device;

namespace MILAV.API
{
    public interface IServerAPI
    {
        // Devices

        public IEnumerable<IDevice> GetDevices();

        // Device

        public IDevice? GetDeviceById(string deviceId);

        public IEnumerable<Input>? GetDeviceInputsById(string deviceId);

        public IEnumerable<Output>? GetDeviceOutputsById(string deviceId);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();

        // Routing

        public bool TryRoute(string deviceId, Input input, Output output);

        public Input? GetRoute(string deviceId, Output output);
    }
}
