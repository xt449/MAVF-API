namespace MILAV.API.Device.Implementations
{
    [Device("customchannelcontroller")]
    public class CustomChannelController : AbstractDevice, IPowerControl
    {
        public bool GetPower()
        {
            return false;
        }

        public void SetPower(bool state)
        {
            // nothing
        }
    }
}
