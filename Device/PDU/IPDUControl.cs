using Newtonsoft.Json;

namespace MAVF.API.Device.PDU
{
	/// <summary>
	/// This interfaces funcationilty may be built into instances of IDevice later
	/// </summary>
	public interface IPDUControl
	{
		public void TurnPowerOn(int port);

		public void TurnPowerOff(int port);

		/// <summary>
		/// Key: Device#Id
		/// Value: Port
		/// </summary>
		[JsonProperty("ports", Required = Required.Always)]
		public Dictionary<string, int> Ports { get; init; }
	}
}
