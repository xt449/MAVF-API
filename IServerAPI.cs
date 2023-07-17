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

        // Mode

        public string GetMode();

        public void SetMode(string mode);

        public void ResetMode();

        // Routing

        public IEnumerable<IInputOutput>? GetDeviceInputsById(string deviceId);

        public IEnumerable<IInputOutput>? GetDeviceOutputsById(string deviceId);

        public bool TrySetRoute(string deviceId, IInputOutput input, IInputOutput output);

        public IInputOutput? GetRoute(string deviceId, IInputOutput output);

        // Layout

        public bool TrySetLayout(string deviceId, ILayout layout);

        public ILayout? GetLayout(string deviceId);

        // TVTuner

        public bool TrySetChannel(string deviceId, string channel);

        public string? GetChannel(string deviceId);

        // PDU

        public bool TryTurnPowerOn(string deviceId, int port);

        public bool TryTurnPowerOff(string deviceId, int port);
    }
}
