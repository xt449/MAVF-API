﻿using MAVF.API.Device;
using Newtonsoft.Json;

namespace MAVF.API
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class Configuration
	{
		[JsonProperty(Required = Required.Always)]
		public readonly bool debug;

		[JsonConverter(typeof(JArrayDictionaryConverter<IDevice>))]
		[JsonProperty(Required = Required.Always)]
		public readonly Dictionary<string, IDevice> devices;

		[JsonProperty(Required = Required.Always)]
		public readonly List<string> modes;

		[JsonProperty(Required = Required.Always)]
		public readonly string defaultModeId;

		[JsonConverter(typeof(JArrayDictionaryConverter<UserInterface>))]
		[JsonProperty(Required = Required.Always)]
		public readonly Dictionary<string, UserInterface> users;

		[JsonProperty(Required = Required.Always)]
		public readonly string masterUserId;

		public Configuration()
		{
			this.debug = true;

			this.devices = new Dictionary<string, IDevice>();

			this.modes = new List<string>();
			this.defaultModeId = string.Empty;

			this.users = new Dictionary<string, UserInterface>();
			this.masterUserId = string.Empty;
		}
	}
}
