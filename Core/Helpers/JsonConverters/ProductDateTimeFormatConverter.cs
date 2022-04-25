using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Helpers.JsonConverters
{
    public class ProductDateTimeFormatConverter:IsoDateTimeConverter
    {
        public ProductDateTimeFormatConverter()
        {
            DateTimeFormat="yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        }
        
        public ProductDateTimeFormatConverter(string datetimeFormat="yyyy'-'MM'-'dd' 'HH':'mm':'ss")
        {
            DateTimeFormat=datetimeFormat;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {

            try
            {
                if (DateTime.TryParseExact(reader.Value?.ToString(), DateTimeFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles, out var rtVal))
                {
                    return rtVal;
                }
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (Exception e)
            {
                return DateTime.MinValue;
            }
            
        }
    }
}