using MAVF.API.Device.Driver;
using System.Text.Json.Serialization;

namespace MAVF.API.Device
{
	public sealed record Device : IIdentifiable
	{
		[JsonPropertyName("id")]
		public required string Id { get; init; }

		[JsonPropertyName("driver")]
		public required IDriver Driver { get; init; }

		[JsonPropertyName("classifications")]
		public string[] Classifications { get; init; } = [];

		[JsonPropertyName("powerControl")]
		public PowerControlProperties? PowerControl { get; init; } = null;

		public sealed record PowerControlProperties
		{
			[JsonPropertyName("pduDeviceId")]
			public required string PduDeviceId { get; init; }

			[JsonPropertyName("pduOutletNumber")]
			public required int PduOutletNumber { get; init; }
		}
	}
}
