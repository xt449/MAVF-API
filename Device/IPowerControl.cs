namespace MAVF.API.Device
{
	public interface IPowerControl
	{
		public bool GetPower();

		// TODO - Is this ever needed, or should a local variable track the last power state
		public void SetPower(bool state);
	}
}
