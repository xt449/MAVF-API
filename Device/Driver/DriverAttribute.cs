namespace MAVF.API.Device.Driver
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DriverAttribute : Attribute
	{
		public readonly string driver;

		public DriverAttribute(string driver)
		{
			this.driver = driver;
		}
	}
}
