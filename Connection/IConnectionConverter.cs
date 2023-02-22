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
            return typeof(IConnection).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(JsonObjectAttribute)) != null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (token is JObject jObject)
            {
                var ttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                    .First(type => typeof(IConnection).IsAssignableFrom(type) && Attribute.GetCustomAttribute(type, typeof(JsonObjectAttribute)) != null && ((JsonObjectAttribute)Attribute.GetCustomAttribute(type, typeof(JsonObjectAttribute))).Id == (string)jObject["type"]);
                return JsonConvert.DeserializeObject(token.ToString(Formatting.None), ttype);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JObject.FromObject(value);
            var attribute = (JsonObjectAttribute)Attribute.GetCustomAttribute(value.GetType(), typeof(JsonObjectAttribute));
            token["type"] = attribute.Id;
            token.WriteTo(writer);
        }
    }
}
