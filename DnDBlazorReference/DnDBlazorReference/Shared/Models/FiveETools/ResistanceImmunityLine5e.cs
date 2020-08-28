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
            ResistanceImmunityLine5e resistanceImmunityLine = new ResistanceImmunityLine5e
            {
                DamageTypes = new List<string>(),
                Note = string.Empty
            };

            if (reader.TokenType == JsonTokenType.String)
            {
                resistanceImmunityLine.DamageTypes.Add(reader.GetString());

                return resistanceImmunityLine;
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

                string propName = reader.GetString();
                reader.Read();

                switch (propName)
                {
                    case "resist":
                    case "immune":
                        resistanceImmunityLine.DamageTypes = JsonSerializer.Deserialize<List<string>>(ref reader);
                        break;
                    case "preNote":
                        resistanceImmunityLine.PreNote = reader.GetString();
                        break;
                    case "note":
                        resistanceImmunityLine.Note = reader.GetString();
                        break;
                    case "special":
                        resistanceImmunityLine.Special = reader.GetString();
                        break;
                    default:
                        throw new JsonException("PropertyName: " + propName);
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

        [JsonPropertyName("preNote")]
        public string PreNote { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("special")]
        public string Special { get; set; }
    }
}
