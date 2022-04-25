using System;
using Bussiness.Models;
using Newtonsoft.Json;

namespace Core.Helpers.JsonConverters
{
    public class ZeroOneBooleanConverter:JsonConverter
    {
        public ZeroOneBooleanConverter()
        {
            
        }
        public ZeroOneBooleanConverter(BooleanJsonConvertOption option=BooleanJsonConvertOption.ZeroOne)
        {
            ConvertOption = option;
        }

        public BooleanJsonConvertOption ConvertOption { get; set; }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var writenStr = ConvertOption switch
            {
                BooleanJsonConvertOption.YesNo => ((bool)value) ? "yes" : "no",
                BooleanJsonConvertOption.ZeroOne => ((bool)value) ? "1" : "0",
                BooleanJsonConvertOption.TrueFalse => ((bool)value) ? "true" : "false",
                BooleanJsonConvertOption.YesNoFirstLetter => ((bool)value) ? "y" : "n",
                BooleanJsonConvertOption.TrueFalseFirstLetter => ((bool)value) ? "t" : "f",
                _ => ((bool)value) ? "true" : "false"
            };
            writer.WriteValue(writenStr);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.Value?.ToString()?.ToLower().Trim())
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
                case "false":
                case "no":
                case "n":
                case "0":
                    return false;
                default:
                    return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}