using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class Item
    {
        public string Name { get; set; }
        public Description Description { get; set; }
        public bool IsConsumable { get; set; }
        public float ValueGP { get; set; }
        public float WeightLB { get; set; }
    }
}
