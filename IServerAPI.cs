﻿using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV.API
{
    public interface IServerAPI
    {
        // Devices

        public AbstractDevice[] GetDevices();

        // Device

        public AbstractDevice? GetDeviceById(string id);

        // Should this be hidden?
        public string? GetDeviceIpById(string id);

        public int? GetDevicePortById(string id);

        public Protocol? GetDeviceProtocolById(string id);

        public Input[]? GetDeviceInputsById(string id);

        public Output[]? GetDeviceOutputsById(string id);

        // ControlState

        public string GetControlState();

        public void SetControlState(string state);

        public void ResetControlState();
    }
}