using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MILAV.API
{
	public class IdentifiableCollectionToDictionaryConverter<T> : JsonConverter where T : IIdentifiable
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			return typeof(Dictionary<string, T>).IsAssignableFrom(objectType);
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			if (JToken.ReadFrom(reader) is JArray jArray)
			{
				return jArray.Select(
					o => o.ToObject<T>() ?? throw new JsonException($"Unable to deserialize {typeof(T)} (object is null)")
				).ToDictionary(o => o.Id, o => o);
			}

			throw new JsonException($"Unable to deserialize to Dictionary<string, {typeof(T)}> (JToken is not JArray)");
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			// default
		}
	}
}
