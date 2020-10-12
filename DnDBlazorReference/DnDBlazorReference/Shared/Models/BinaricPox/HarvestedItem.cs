using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class HarvestedItem : Item
    {
        public string Quantity { get; set; }
        public Description UseText { get; set; }
        public List<string> RequiredToolNames { get; set; }
        public string CraftingUsage { get; set; }

        [JsonIgnore]
        public string NameWithQuantity
        {
            get 
            {
                string name = Name;

                if (!string.IsNullOrEmpty(Quantity)) 
                {
                    name = $"{name} ({Quantity})";
                }

                return name;
            }
        }
    }
}
