﻿using Newtonsoft.Json;

namespace MILAV.API.Connection
{
    [JsonConverter(typeof(ProtocolConverter))]
    public enum Protocol
    {
        TCP, TELNET, HTTP, WEBSOCKET, SSH, UDP
    }

    internal class ProtocolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Protocol) == objectType;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var value = reader.ReadAsString();
            return value == null ? null : Enum.Parse(typeof(Protocol), value, true);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(Enum.GetName(typeof(Protocol), value));
        }
    }
}