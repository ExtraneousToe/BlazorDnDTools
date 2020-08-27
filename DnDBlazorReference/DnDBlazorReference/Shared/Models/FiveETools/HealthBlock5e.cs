using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class HealthBlock5e
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

}
