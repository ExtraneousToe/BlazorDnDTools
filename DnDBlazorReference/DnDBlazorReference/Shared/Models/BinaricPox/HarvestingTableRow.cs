using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class HarvestingTableRow
    {
        public int DifficultyClass { get; set; }
        public string ItemNameRef { get; set; }

        [JsonIgnore]
        public HarvestedItem Item { get; set; }
    }
}
