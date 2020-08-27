using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.FiveETools
{
    public class AdditionalSource5e
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }

        public override string ToString()
        {
            return Source;
        }
    }
}
