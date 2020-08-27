using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class SpeedBlock5e
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
}
