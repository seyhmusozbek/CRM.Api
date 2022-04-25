using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Helpers.JsonConverters
{
    public class JsonTimestampConverter:IsoDateTimeConverter
    {
        
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer){
            try
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var jsonValue = reader.Value?.ToString();
                var unixValue = Convert.ToInt32(string.IsNullOrEmpty(jsonValue) ?"0":jsonValue);
                dateTime = dateTime.AddSeconds( unixValue ).ToLocalTime();
                return dateTime;
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            
        }
    }
}