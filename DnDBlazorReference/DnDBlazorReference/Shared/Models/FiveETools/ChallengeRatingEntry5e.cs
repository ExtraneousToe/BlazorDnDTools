using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class ChallengeRatingEntry5eConverter : JsonConverter<ChallengeRatingEntry5e>
    {
        public override ChallengeRatingEntry5e Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ChallengeRatingEntry5e entry = new ChallengeRatingEntry5e();

            if (reader.TokenType == JsonTokenType.String)
            {
                entry.ChallengeRating = reader.GetString();
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
                    case "cr":
                        entry.ChallengeRating = reader.GetString();
                        break;
                    case "coven":
                        entry.Coven = reader.GetString();
                        break;
                    case "lair":
                        entry.Lair = reader.GetString();
                        break;
                    default:
                        throw new JsonException(propertyName);
                }
            }

            return entry;
        }

        public override void Write(Utf8JsonWriter writer, ChallengeRatingEntry5e value, JsonSerializerOptions options)
        {
            //throw new NotImplementedException();
        }
    }

    [JsonConverter(typeof(ChallengeRatingEntry5eConverter))]
    public class ChallengeRatingEntry5e
    {
        [JsonPropertyName("cr")]
        public string ChallengeRating { get; set; }
        [JsonPropertyName("coven")]
        public string Coven { get; set; }
        [JsonPropertyName("lair")]
        public string Lair { get; set; }

        public override string ToString()
        {
            string output = ChallengeRating;

            if (!string.IsNullOrEmpty(Coven))
            {
                output = $"{output}, {Coven} in a coven";
            }

            if (!string.IsNullOrEmpty(Lair))
            {
                output = $"{output}, {Lair} in the creature's lair";
            }

            return output;
        }
    }
}
