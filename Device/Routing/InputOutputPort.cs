﻿using Newtonsoft.Json;

namespace MAVF.API.Device.Routing
{
	public class InputOutputPort : IInputOutput
	{
		public string Id { get; init; }

		public IOType Type { get; init; }

		public string Group { get; init; }

		[JsonProperty(Required = Required.Always)]
		public readonly int port;
	}
}
