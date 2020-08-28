using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class SpeedBlock5e
    {
        [JsonPropertyName("walk")]
        public SpeedSubBlock5e Walk { get; set; }
        [JsonPropertyName("fly")]
        public SpeedSubBlock5e Fly { get; set; }
        [JsonPropertyName("swim")]
        public SpeedSubBlock5e Swim { get; set; }
        [JsonPropertyName("climb")]
        public SpeedSubBlock5e Climb { get; set; }
        [JsonPropertyName("burrow")]
        public SpeedSubBlock5e Burrow { get; set; }

        [JsonPropertyName("canHover")]
        public bool CanHover { get; set; }
    }

    public class SpeedSubBlock5eConverter : JsonConverter<SpeedSubBlock5e>
    {
        public override SpeedSubBlock5e Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            SpeedSubBlock5e entry = new SpeedSubBlock5e();

            if (reader.TokenType == JsonTokenType.Number)
            {
                entry.Number = reader.GetInt32();
                return entry;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Not start of object. " + reader.TokenType);
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Not a property name. " + reader.TokenType);
                }

                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "number":
                        entry.Number = reader.GetInt32();
                        break;
                    case "condition":
                        entry.Condition = reader.GetString();
                        break;
                    default:
                        throw new JsonException(propertyName);
                }
            }

            return entry;
        }

        public override void Write(Utf8JsonWriter writer, SpeedSubBlock5e value, JsonSerializerOptions options)
        {
            //throw new NotImplementedException();
        }
    }

    [JsonConverter(typeof(SpeedSubBlock5eConverter))]
    public class SpeedSubBlock5e
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }
    }
}
