using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class ChallengeRatingEntry5e
    {
        [JsonPropertyName("cr")]
        public string ChallengeRating { get; set; }
        [JsonPropertyName("coven")]
        public string Coven { get; set; }
        [JsonPropertyName("lair")]
        public string Lair { get; set; }
    }
}
