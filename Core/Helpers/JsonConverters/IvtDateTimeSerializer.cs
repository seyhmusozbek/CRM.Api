using System;
using Newtonsoft.Json;

namespace Core.Helpers.JsonConverters
{
    public class IvtDateTimeSerializer:JsonConverter<DateTime>
    {
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString("yyyy-MM-dd"));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (hasExistingValue)
            {
                return existingValue;
            }

            var stringValue = reader.ReadAsString();
            return DateTime.Parse(stringValue);
        }
    }
}