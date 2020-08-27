using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
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
            return $"{ArmorClass} ({string.Join(", ", From)})";
        }
    }
}
