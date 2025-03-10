using MAVF.API.Device.Driver.Routing;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace MAVF.API
{
	/// <summary>
	/// Physical interface
	/// </summary>
	public class UserInterface : IIdentifiable
	{
		[JsonPropertyName("ip")]
		public required string Id { get; init; }

		[JsonPropertyName("modeGroups")]
		public required Dictionary<string, string[]> ModeGroups { get; init; }

		/// <summary>
		/// Groups that can be controlled
		/// </summary>
		private string[] controlGroups = Array.Empty<string>();

		public void UpdateMode(string mode)
		{
			controlGroups = ModeGroups.GetValueOrDefault(mode, []);
		}

		public bool CanRouteInput(IInputOutput input)
		{
			return controlGroups.Contains(input.Group);
		}

		public bool CanRouteOutput(IInputOutput output)
		{
			return controlGroups.Contains(output.Group);
		}
	}
}
