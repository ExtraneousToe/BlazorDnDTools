using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class ResistanceImmunityLine5eConverter : JsonConverter<ResistanceImmunityLine5e>
    {
        public override ResistanceImmunityLine5e Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ResistanceImmunityLine5e resistanceImmunityLine = new ResistanceImmunityLine5e();
            resistanceImmunityLine.DamageTypes = new List<string>();
            resistanceImmunityLine.Note = string.Empty;

            string outputBuilder = string.Empty;
            do
            {
                outputBuilder += reader.TokenType + ", ";

                switch (reader.TokenType)
                {
                    case JsonTokenType.None:
                        break;
                    case JsonTokenType.StartObject:
                        break;
                    case JsonTokenType.EndObject:
                        break;
                    case JsonTokenType.StartArray:
                        break;
                    case JsonTokenType.EndArray:
                        break;
                    case JsonTokenType.Comment:
                        break;
                    case JsonTokenType.PropertyName:
                    case JsonTokenType.String:
                        outputBuilder += "{" +reader.GetString() + "}";
                        break;
                    case JsonTokenType.Number:
                        break;
                    case JsonTokenType.True:
                        break;
                    case JsonTokenType.False:
                        break;
                    case JsonTokenType.Null:
                        break;
                }
            } while (reader.Read());

            throw new Exception(outputBuilder);

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
                    case "note":
                        resistanceImmunityLine.Note = reader.GetString();
                        break;
                    default:
                        throw new JsonException("PropertyName: " + propertyName);
                }
            }

            return resistanceImmunityLine;
        }

        public override void Write(Utf8JsonWriter writer, ResistanceImmunityLine5e value, JsonSerializerOptions options)
        {
            //throw new NotImplementedException();
        }
    }

    [JsonConverter(typeof(ResistanceImmunityLine5eConverter))]
    public class ResistanceImmunityLine5e
    {
        public List<string> DamageTypes { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }
    }
}
