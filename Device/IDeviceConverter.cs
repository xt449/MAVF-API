using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MILAV.API.Device
{
    public class IDeviceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IDevice).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(DeviceAttribute)) != null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DeviceRegistry.Initialize();

            var token = JToken.ReadFrom(reader);
            if (token is JObject jObject)
            {
                if (DeviceRegistry.TryGet((DeviceType)Enum.Parse(typeof(DeviceType), (string)jObject["type"], true), (string)jObject["id"], out Type type))
                {
                    return JsonConvert.DeserializeObject(token.ToString(Formatting.None), type);
                }
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JObject.FromObject(value);
            var attribute = (DeviceAttribute)Attribute.GetCustomAttribute(value.GetType(), typeof(DeviceAttribute));
            token["type"] = attribute.type.ToString();
            token["id"] = attribute.id;
            token.WriteTo(writer);
        }
    }
}
