using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MILAV.API.Connection
{
    internal class IConnectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // True for types that are castable to IConnection and have the JsonObjectAttribute
            return typeof(IConnection).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(JsonObjectAttribute)) != null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (token is JObject jObject)
            {
                // Get the first type that has the JsonObjectAttribute with the `Id` property equal to the same value as the "type" property od the JSON object 
                var ttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                    .First(type => ((JsonObjectAttribute)Attribute.GetCustomAttribute(type, typeof(JsonObjectAttribute)))?.Id == (string)jObject["type"]);

                // Use the default deserializer for the specific type found
                return JsonConvert.DeserializeObject(token.ToString(Formatting.None), ttype);
            }

            // Fail when the token is not an object
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Get the basic JSON object from the value
            var token = JObject.FromObject(value);
            var attribute = (JsonObjectAttribute)Attribute.GetCustomAttribute(value.GetType(), typeof(JsonObjectAttribute));
            // Add the "type" property with the `Id` from the JsonObjectAttribute which will be used for deserialization
            token["type"] = attribute.Id;
            token.WriteTo(writer);
        }
    }
}
