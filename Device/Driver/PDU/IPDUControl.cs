﻿namespace MAVF.API.Device.Driver.PDU
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
		public Dictionary<string, int> Ports { get; }
	}
}
