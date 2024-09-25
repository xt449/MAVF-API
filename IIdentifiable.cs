using Newtonsoft.Json;

namespace MAVF.API
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIdentifiable
	{
		[JsonProperty("id", Required = Required.Always)]
		public string Id { get; init; }
	}
}
