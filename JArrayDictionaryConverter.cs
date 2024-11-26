using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MAVF.API
{
	public class JArrayDictionaryConverter<T> : JsonConverter where T : IIdentifiable
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Dictionary<string, T>).IsAssignableFrom(objectType);
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			// Read JSON Array
			return JArray.Load(reader)
				// Deserialize each element of array
				.Select(jToken => jToken.ToObject<T>() ?? throw new JsonSerializationException($"Unable to deserialize {typeof(T)} (object is null)"))
				// Convert to dictionary
				.ToDictionary(tObject => tObject.Id);
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			// Ensure value is a Disctionary of string to T
			if (value is not Dictionary<string, T> castValue)
			{
				throw new JsonSerializationException($"Unable to serialize {value.GetType()}");
			}

			// Get value set from dictionary and order by id
			// Serialize to writer
			serializer.Serialize(writer, castValue.Values.OrderBy(tObject => tObject.Id));
		}
	}
}
