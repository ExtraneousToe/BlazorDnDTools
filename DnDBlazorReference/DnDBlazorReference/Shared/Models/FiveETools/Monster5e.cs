using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class Monster5e
    {
        [JsonPropertyName("isNpc")]
        public bool IsNPC { get; set; }
        [JsonPropertyName("_copy")]
        public object CopyDirective { get; set; }
        [JsonIgnore]
        public bool HasCopyDirective => CopyDirective != null;

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("otherSources")]
        public List<AdditionalSource5e> OtherSources { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("ac")]
        public List<ArmorClassEntry5e> ArmorClass { get; set; }

        [JsonPropertyName("hp")]
        public HealthBlock5e Health { get; set; }

        [JsonPropertyName("speed")]
        public SpeedBlock5e Speed { get; set; }

        [JsonPropertyName("save")]
        public SavesBlock5e Saves { get; set; }

        [JsonPropertyName("skill")]
        public SkillsBlock5e Skills { get; set; }

        // is either a string or a CreatureType?
        [JsonPropertyName("type")]
        public CreatureType5e CreatureType { get; set; }

        [JsonPropertyName("cr")]
        public ChallengeRatingEntry5e ChallengeRating { get; set; }

        [JsonPropertyName("passive")]
        public int PassivePerception { get; set; }

        [JsonPropertyName("senses")]
        public List<string> Senses { get; set; }

        [JsonPropertyName("resist")]
        public List<ResistanceImmunityLine5e> DamageResistances { get; set; }
        
        [JsonPropertyName("immune")]
        public List<ResistanceImmunityLine5e> DamageImmunities { get; set; }

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
