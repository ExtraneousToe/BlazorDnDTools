using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DnDBlazorReference.Shared.Models.FiveETools;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public static class CreatureTypeUtility
    {
        public static CreatureType ConvertToUsable(this CreatureType5e creatureType5e)
        {
            return new CreatureType()
            {
                Type = creatureType5e.Type,
                Tags = creatureType5e.Tags,
            };
        }

        public static bool Compare(CreatureType creatureType, CreatureType5e creatureType5e)
        {
            return creatureType.Type.Equals(creatureType5e.Type) &&
                creatureType.Tags.Equals(creatureType5e.Tags);
        }
    }

    public class CreatureType : IEquatable<CreatureType>, IComparable<CreatureType>
    {
        public string Type { get; set; }
        public List<string> Tags { get; set; }

        public override string ToString()
        {
            string output = Type;
            
            if (Tags.Count() > 0)
            {
                output = $"{output} ({string.Join(", ", Tags)})";
            }

            return output;
        }

        public bool Equals(CreatureType other)
        {
            return ToString().Equals(other.ToString());
        }

        public int CompareTo(CreatureType other)
        {
            return ToString().CompareTo(other.ToString());
        }
    }
}
