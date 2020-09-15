using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DnDBlazorReference.Shared.Models.FiveETools;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public static class MonsterUtility
    {
        public static Monster ConvertToUsable(this Monster5e monster5e)
        {
            return new Monster()
            {
                Name = monster5e.Name,
                ReferenceCardSize = ECardSize.None,
                PrimarySource = monster5e.Source,
                OtherSources = new List<string>(monster5e.OtherSources != null ? monster5e.OtherSources.Select(s => s.Source) : new string[] { }),
                CreatureTypeRef = monster5e.CreatureType.ToString(),
                HarvestingTable = new HarvestingTable
                {
                    Rows = new List<HarvestingTableRow>()
                },
                StatBlock = new StatBlock
                {
                    Strength = monster5e.Strength,
                    Dexterity = monster5e.Dexterity,
                    Constitution = monster5e.Constitution,
                    Intelligence = monster5e.Intelligence,
                    Wisdom = monster5e.Wisdom,
                    Charisma = monster5e.Charisma
                },
                ChallengeRating = monster5e.ChallengeRating?.ConvertToUsable()
            };
        }
    }

    public class Monster : IEquatable<Monster>, IComparable<Monster>
    {
        public string Name { get; set; }
        public ECardSize ReferenceCardSize { get; set; }

        public string PrimarySource { get; set; }
        public List<string> OtherSources { get; set; }

        public string CreatureTypeRef { get; set; }
        [JsonIgnore]
        public CreatureType CreatureType { get; set; }

        public string TrinketTableType { get; set; }
        [JsonIgnore]
        public TrinketTable TrinketTable { get; set; }

        public HarvestingTable HarvestingTable { get; set; }
        public StatBlock StatBlock { get; set; }
        public ChallengeRating ChallengeRating { get; set; }

        public bool Equals(Monster other)
        {
            return Name.Equals(other.Name) &&
                ReferenceCardSize.Equals(other.ReferenceCardSize) &&
                CreatureTypeRef.Equals(other.CreatureTypeRef) &&
                ChallengeRating.Equals(other.ChallengeRating) &&
                //HarvestingTable.Equals(other.HarvestingTable) &&
                StatBlock.Equals(other.StatBlock);
        }

        public void CopyInMissing(Monster other)
        {
            Name ??= other.Name;
            CreatureTypeRef ??= other.CreatureTypeRef;
            PrimarySource ??= other.PrimarySource;
            OtherSources ??= other.OtherSources;
            HarvestingTable ??= other.HarvestingTable;
            StatBlock ??= other.StatBlock;
            ChallengeRating ??= other.ChallengeRating;
        }

        public int CompareTo(Monster other)
        {
            return Name.CompareTo(other.Name);
        }
    }

    public class StatBlock : IEquatable<StatBlock>
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public bool Equals(StatBlock other)
        {
            return Strength.Equals(other.Strength) &&
                  Dexterity.Equals(other.Dexterity) &&
                  Constitution.Equals(other.Constitution) &&
                  Intelligence.Equals(other.Intelligence) &&
                  Wisdom.Equals(other.Wisdom) &&
                  Charisma.Equals(other.Charisma);
        }
    }
}
