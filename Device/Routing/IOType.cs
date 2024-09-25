namespace MAVF.API.Device.Routing
{
	[Flags]
	public enum IOType
	{
		None = 0,
		Audio = 1,
		Video = 2,
		USB = 4,
	}
}
