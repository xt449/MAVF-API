using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	// Register IDriver implementing type to DriverRegistry
	[Driver("test")]
	public sealed class TestDriver : IDriver<TestDriver.DriverProperties>
	{
		// Map property name for JsonSerializer
		[JsonPropertyName("properties")]
		public required DriverProperties Properties { get; init; }

		// Map constructor for JsonSerializer
		[JsonConstructor]
		// Allow direct instantiation of type (debug only)
		[SetsRequiredMembers]
		public TestDriver(DriverProperties properties)
		{
			// Ensure that value from JsonSerializer is not null
			Properties = properties ?? throw new ArgumentNullException(nameof(properties));
		}

		// Records

		public record DriverProperties
		{
			// Map property name for JsonSerializer
			[JsonPropertyName("example")]
			public required float Example { get; init; }
		}
	}
}
