using MILAV.API.Device.Routing;
using Newtonsoft.Json;

namespace MILAV.API
{
	/// <summary>
	/// Physical interface
	/// </summary>
	public class User : IIdentifiable
	{
		[JsonProperty("ip", Required = Required.Always)]
		public string Id { get; init; }

		[JsonProperty("modeGroups", Required = Required.Always)]
		public readonly Dictionary<string, string[]> modeGroups;

		/// <summary>
		/// Groups that can be controlled
		/// </summary>
		private string[] controlGroups = Array.Empty<string>();

		public void UpdateMode(string mode)
		{
			controlGroups = modeGroups[mode] ?? Array.Empty<string>();
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
