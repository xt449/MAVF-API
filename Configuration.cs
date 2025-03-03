using MAVF.API.Device.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MAVF.API
{
	public class Configuration
	{
		[JsonPropertyName("debug")]
		public required bool Debug { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<Device.Device>))]
		[JsonPropertyName("devices")]
		public required Dictionary<string, Device.Device> Devices { get; init; }

		[JsonPropertyName("modes")]
		public required List<string> Modes { get; init; }

		[JsonPropertyName("defaultModeId")]
		public required string DefaultModeId { get; init; }

		[JsonConverter(typeof(JArrayDictionaryConverter<UserInterface>))]
		[JsonPropertyName("users")]
		public required Dictionary<string, UserInterface> Users { get; init; }

		[JsonPropertyName("masterUserId")]
		public required string MasterUserId { get; init; }

		[SetsRequiredMembers]
		public Configuration()
		{
			Debug = true;

			Devices = new()
			{
				["example"] = new Device.Device()
				{
					Id = "example",
					Driver = new TestDriver(new TestDriver.DriverProperties()
					{
						Example = 12
					})
				}
			};

			Modes =
			[
				"mode0"
			];
			DefaultModeId = "mode0";

			Users = new Dictionary<string, UserInterface>()
			{
				["172.16.0.11"] = new UserInterface()
				{
					Id = "172.16.0.11",
					ModeGroups = []
				}
			};
			MasterUserId = "172.16.0.11";
		}
	}
}
