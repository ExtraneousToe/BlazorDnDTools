using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class HarvestingTable : IEquatable<HarvestingTable>
    {
        public List<HarvestingTableRow> Rows { get; set; }

        public bool Equals(HarvestingTable other)
        {
            return Rows.Equals(other.Rows);
        }
    }
}
