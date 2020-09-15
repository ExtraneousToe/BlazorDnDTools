using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class Item : IComparable<Item>
    {
        public string Name { get; set; }
        public Description Description { get; set; }
        public bool IsConsumable { get; set; }
        public string ValueGP { get; set; }
        public string WeightLB { get; set; }

        public int CompareTo(Item other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
