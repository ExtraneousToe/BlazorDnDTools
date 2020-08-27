using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class SavesBlock5e
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
}
