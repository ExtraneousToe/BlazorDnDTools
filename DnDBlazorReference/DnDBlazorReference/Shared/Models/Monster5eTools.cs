using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models
{
    public class CreatureTypeConverter : JsonConverter<CreatureType>
    {
        public override CreatureType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            CreatureType cType = new CreatureType();
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
                    throw new JsonException("Not a property name. "+ reader.TokenType);
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

        public override void Write(Utf8JsonWriter writer, CreatureType value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class CreatureType
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("swarmSize")]
        public string SwarmSize { get; set; }

        public override string ToString()
        {
            if (Tags.Count == 0)
            {
                return $"{Type}";
            }
            else
            {
                return $"{Type} ({string.Join(", ", Tags)})";
            }
        }
    }

    public class AdditionalSource
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }

        public override string ToString()
        {
            return Source;
        }
    }

    public class HealthBlock
    {
        [JsonPropertyName("average")]
        public int Average { get; set; }

        [JsonPropertyName("formula")]
        public string Formula { get; set; }

        public override string ToString()
        {
            return $"{Average} ({Formula})";
        }
    }

    public class SpeedBlock
    {
        [JsonPropertyName("walk")]
        public object Walk { get; set; }
        [JsonPropertyName("fly")]
        public object Fly { get; set; }
        [JsonPropertyName("swim")]
        public object Swim { get; set; }
        [JsonPropertyName("climb")]
        public object Climb { get; set; }
        [JsonPropertyName("burrow")]
        public object Burrow { get; set; }
    }

    public class NotedResist
    {
        [JsonPropertyName("resist")]
        public List<string> Resistances { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
    }

    public class NotedImmune
    {
        [JsonPropertyName("immune")]
        public List<string> Immunities { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
    }

    public class ArmorClassEntry
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
            return $"{ArmorClass} ({string.Join(", ", From)})";
        }
    }

    public class ChallengeRatingEntry
    {
        [JsonPropertyName("cr")]
        public string ChallengeRating { get; set; }
        [JsonPropertyName("coven")]
        public string Coven { get; set; }
        [JsonPropertyName("lair")]
        public string Lair { get; set; }
    }

    public class SavesBlock
    {
        [JsonPropertyName("str")]
        public string Strength { get; set; }
        [JsonPropertyName("dex")]
        public string Dexterity { get; set; }
        [JsonPropertyName("con")]
        public string Constituition { get; set; }
        [JsonPropertyName("int")]
        public string Intelligence { get; set; }
        [JsonPropertyName("wis")]
        public string Wisdom { get; set; }
        [JsonPropertyName("cha")]
        public string Charisma { get; set; }
    }

    public class SkillsBlock
    {
        // Strength
        [JsonPropertyName("athletics")]
        public string Athletics { get; set; }

        // Dexterity
        [JsonPropertyName("acrobatics")]
        public string Acrobatics { get; set; }

        [JsonPropertyName("sleight of hand")]
        public string SleightOfHand { get; set; }

        [JsonPropertyName("stealth")]
        public string Stealth { get; set; }

        // Con

        // Intelligence
        [JsonPropertyName("arcana")]
        public string Arcana { get; set; }
        [JsonPropertyName("history")]
        public string History { get; set; }
        [JsonPropertyName("investigation")]
        public string Investigation { get; set; }
        [JsonPropertyName("nature")]
        public string Nature { get; set; }
        [JsonPropertyName("religion")]
        public string Religion { get; set; }

        // Wisdom
        [JsonPropertyName("animal handling")]
        public string AnimalHandling { get; set; }
        [JsonPropertyName("insight")]
        public string Insight { get; set; }
        [JsonPropertyName("medicine")]
        public string Medicine { get; set; }
        [JsonPropertyName("perception")]
        public string Perception { get; set; }
        [JsonPropertyName("survival")]
        public string Survival { get; set; }

        // Charisma
        [JsonPropertyName("deception")]
        public string Deception { get; set; }
        [JsonPropertyName("intimidation")]
        public string Intimidation { get; set; }
        [JsonPropertyName("performance")]
        public string Performance { get; set; }
        [JsonPropertyName("persuasion")]
        public string Persuasion { get; set; }
    }

    public class Monster5eTools
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("otherSources")]
        public List<AdditionalSource> OtherSources { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("ac")]
        public List<object> ArmorClass { get; set; }

        [JsonPropertyName("hp")]
        public HealthBlock Health { get; set; }

        [JsonPropertyName("speed")]
        public SpeedBlock Speed { get; set; }

        [JsonPropertyName("save")]
        public SavesBlock Saves { get; set; }

        [JsonPropertyName("skill")]
        public SkillsBlock Skills { get; set; }

        // is either a string or a CreatureType?
        [JsonPropertyName("type")]
        [JsonConverter(typeof(CreatureTypeConverter))]
        public CreatureType CreatureType { get; set; }

        [JsonPropertyName("cr")]
        public object ChallengeRating { get; set; }

        [JsonPropertyName("passive")]
        public int PassivePerception { get; set; }

        [JsonPropertyName("senses")]
        public List<string> Senses { get; set; }

        [JsonPropertyName("resist")]
        public List<object> DamageResistances { get; set; }
        
        [JsonPropertyName("immune")]
        public List<object> DamageImmunities { get; set; }

        [JsonPropertyName("conditionImmune")]
        public List<string> ConditionImmunities { get; set; }

        [JsonPropertyName("languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("str")]
        public int Strength { get; set; }
        [JsonPropertyName("dex")]
        public int Dexterity { get; set; }
        [JsonPropertyName("con")]
        public int Constitution { get; set; }
        [JsonPropertyName("int")]
        public int Intelligence { get; set; }
        [JsonPropertyName("wis")]
        public int Wisdom { get; set; }
        [JsonPropertyName("cha")]
        public int Charisma { get; set; }
    }
}
