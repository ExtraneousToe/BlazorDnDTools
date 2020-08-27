using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class SkillsBlock5e
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
}
