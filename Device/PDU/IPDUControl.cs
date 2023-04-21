namespace MILAV.API.Device.PDU
{
    /// <summary>
    /// This interfaces funcationilty may be built into instances of IDevice later
    /// </summary>
    public interface IPDUControl
    {
        public void TurnPowerOn(int port);

        public void TurnPowerOff(int port);
    }
}
