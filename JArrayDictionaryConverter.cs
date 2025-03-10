using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAVF.API
{
	public class JArrayDictionaryConverter<T> : JsonConverter<Dictionary<string, T>> where T : IIdentifiable
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(Dictionary<string, T>).IsAssignableFrom(typeToConvert);
		}

		public override Dictionary<string, T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var rootElement = JsonElement.ParseValue(ref reader);

			if (rootElement.ValueKind != JsonValueKind.Array)
			{
				throw new JsonException($"Unable to deserialize {typeof(T)}. Invalid value type.");
			}

			return rootElement.EnumerateArray()
				// Deserialize each element of array
				.Select(element => JsonSerializer.Deserialize<T>(element) ?? throw new JsonException($"Unable to deserialize {typeof(T)}. Array element is null."))
				// Convert to dictionary
				.ToDictionary(idObject => idObject.Id);
		}

		public override void Write(Utf8JsonWriter writer, Dictionary<string, T> value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}

			JsonSerializer.Serialize(writer, value.Values.OrderBy(tObject => tObject.Id), options);
		}
	}
}
