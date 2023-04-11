namespace MILAV.API.Device.TVTuner
{
    public interface IChannelControl
    {
        string GetChannel();

        void SetChannel(string channel);
    }
}
