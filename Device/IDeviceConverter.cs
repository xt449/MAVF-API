using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MILAV.API.Device
{
    public class IDeviceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // True for types that are castable to IDevice and have the DeviceAttribute
            return typeof(IDevice).IsAssignableFrom(objectType) && Attribute.GetCustomAttribute(objectType, typeof(DeviceAttribute)) != null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (token is JObject jObject)
            {
                // Ensure that the DeviceRegistry has been initialized before accessing it
                DeviceRegistry.Initialize();

                // Get the type device type that matches the "type" and "id" properties of the JSON object
                if (DeviceRegistry.TryGet((DeviceType)Enum.Parse(typeof(DeviceType), (string)jObject["type"], true), (string)jObject["id"], out Type type))
                {
                    // Use the default deserializer for the specific type found
                    return JsonConvert.DeserializeObject(token.ToString(Formatting.None), type);
                }
            }

            // Fail when the token is not an object
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Get the basic JSON object from the value
            var token = JObject.FromObject(value);
            var attribute = (DeviceAttribute)Attribute.GetCustomAttribute(value.GetType(), typeof(DeviceAttribute));
            // Add the "type" property with the `type` from the DeviceAttribute which will be used for deserialization
            token["type"] = attribute.type.ToString();
            // Add the "id" property with the `id` from the DeviceAttribute which will be used for deserialization
            token["id"] = attribute.id;
            token.WriteTo(writer);
        }
    }
}
