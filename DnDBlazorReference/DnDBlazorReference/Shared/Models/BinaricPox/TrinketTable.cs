using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class TrinketTable : IComparable<TrinketTable>
    {
        public string TrinketTableType { get; set; }
        public List<TrinketTableRow> Rows { get; set; }

        public TrinketTable()
        {
            int rowNumber = 1;
            Rows = new List<TrinketTableRow>()
            {
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
                new TrinketTableRow { D8Roll = rowNumber++ },
            };
        }

        public TrinketTable(string tableType) : this()
        {
            TrinketTableType = tableType;
        }

        public int CompareTo(TrinketTable other)
        {
            return TrinketTableType.CompareTo(other.TrinketTableType);
        }
    }
}
