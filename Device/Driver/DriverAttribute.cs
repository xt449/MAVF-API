namespace MAVF.API.Device.Driver
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DriverAttribute(string driver) : Attribute
	{
		public readonly string driver = driver;
	}
}
