using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV.API
{
    public interface ServerAPI
    {
        // Devices

        public AbstractDevice[] GetDevices();

        // Device

        public AbstractDevice? GetDeviceById(string id);

        public string? GetDeviceDriverById(string id);

        // Should this be hidden?
        public string? GetDeviceIpById(string id);

        public int? GetDevicePortById(string id);

        public Protocol? GetDeviceProtocolById(string id);

        public string? GetDeviceRoomById(string id);

        public ControlState? GetDeviceStateById(string id);

        public ControlState[]? GetDeviceStatesById(string id);

        public string[]? GetDeviceStateRoomsById(string id, string state);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();
    }
}
