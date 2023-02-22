namespace MILAV.API.Device
{
    public interface IChannelControl
    {
        string GetChannel();

        void SetChannel(string channel);
    }
}
