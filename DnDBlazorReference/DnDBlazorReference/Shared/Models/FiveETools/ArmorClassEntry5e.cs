using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class ArmorClassEntry5eJsonConverter : JsonConverter<ArmorClassEntry5e>
    {
        public override ArmorClassEntry5e Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ArmorClassEntry5e entry = new ArmorClassEntry5e();
            entry.From = new List<string>();

            if (reader.TokenType == JsonTokenType.Number)
            {
                entry.ArmorClass = reader.GetInt32();
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
                    case "ac":
                        entry.ArmorClass = reader.GetInt32();
                        break;
                    case "from":
                        entry.From = JsonSerializer.Deserialize<List<string>>(ref reader);
                        break;
                    case "condition":
                        entry.Condition = reader.GetString();
                        break;
                    case "braces":
                        entry.Braces = reader.GetBoolean();
                        break;
                    default:
                        throw new JsonException(propertyName);
                }
            }

            return entry;
        }

        public override void Write(Utf8JsonWriter writer, ArmorClassEntry5e value, JsonSerializerOptions options)
        {
            //throw new NotImplementedException();
        }
    }

    [JsonConverter(typeof(ArmorClassEntry5eJsonConverter))]
    public class ArmorClassEntry5e
    {
        [JsonPropertyName("ac")]
        public int ArmorClass { get; set; }
        
        [JsonPropertyName("from")]
        public List<string> From { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; }

        [JsonPropertyName("braces")]
        public bool Braces { get; set; }

        public override string ToString()
        {
            string output = $"{ArmorClass}";

            if (From.Count > 0)
            {
                output = $"{output} ({string.Join(", ", From)})";
            }

            if (!string.IsNullOrEmpty(Condition))
            {
                string condition = Condition;
                if (Braces)
                {
                    condition = $"({condition})";
                }

                output = $"{output} {condition}";
            }

            return output;
        }
    }
}
