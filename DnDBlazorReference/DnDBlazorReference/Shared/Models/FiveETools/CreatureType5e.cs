using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class CreatureType5eConverter : JsonConverter<CreatureType5e>
    {
        public override CreatureType5e Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            CreatureType5e cType = new CreatureType5e();
            cType.Tags = new List<string>();

            if (reader.TokenType == JsonTokenType.String)
            {
                cType.Type = reader.GetString();

                return cType;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Not start of object. " + reader.TokenType);
            }

            bool firstToken = true;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return cType;
                }

                if (firstToken && reader.TokenType == JsonTokenType.String)
                {
                    firstToken = false;
                    cType.Type = reader.GetString();
                    continue;
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
                    case "type":
                        cType.Type = reader.GetString();
                        break;
                    case "tags":
                        cType.Tags = JsonSerializer.Deserialize<List<string>>(ref reader);
                        break;
                    case "swarmSize":
                        cType.SwarmSize = reader.GetString();
                        break;
                    default:
                        throw new JsonException(propertyName);
                }
            }

            throw new JsonException("Ended?");
        }

        public override void Write(Utf8JsonWriter writer, CreatureType5e value, JsonSerializerOptions options)
        {
        }
    }

    [JsonConverter(typeof(CreatureType5eConverter))]
    public class CreatureType5e
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("swarmSize")]
        public string SwarmSize { get; set; }

        public override string ToString()
        {
            string output = Type;

            if (Tags.Count > 0)
            {
                output = $"{output} ({string.Join(", ", Tags)})";
            }

            if(!string.IsNullOrEmpty(SwarmSize))
            {
                output = $"{output} (swarm:{SwarmSize})";
            }

            return output;
        }
    }
}
