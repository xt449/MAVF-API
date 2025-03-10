using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	// Register IDriver implementing type to DriverRegistry
	[Driver("none")]
	public sealed class NoneDriver : IDriver
	{
		public object? Properties => null;

		// Map constructor for JsonSerializer
		[JsonConstructor]
		public NoneDriver()
		{
		}
	}
}
