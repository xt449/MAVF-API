namespace MAVF.API.Device.Driver.TVTuner
{
	public interface IChannelControl
	{
		public string? GetChannel();

		public void SetChannel(string channel);
	}
}
