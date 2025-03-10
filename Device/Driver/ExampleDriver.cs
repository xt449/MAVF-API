using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	// Register IDriver implementing type to DriverRegistry
	[Driver("test")]
	public sealed class ExampleDriver : AbstractCommunicationDriver<ExampleDriver.DriverProperties>
	{
		// Map constructor for JsonSerializer
		[JsonConstructor]
		public ExampleDriver(DriverProperties properties) : base(properties)
		{
			Connection.Connect();

			Connection.WriteASCII(Properties.Example);
		}

		// Records

		public record DriverProperties : CommunicationDriverProperties
		{
			// Map property name for JsonSerializer
			[JsonPropertyName("example")]
			public required string Example { get; init; }
		}

		public record DriverProperties2
		{

		}
	}
}
