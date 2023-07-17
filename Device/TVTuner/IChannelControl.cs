namespace MILAV.API.Device.TVTuner
{
	public interface IChannelControl
	{
		public string? GetChannel();

		public void SetChannel(string channel);
	}
}
