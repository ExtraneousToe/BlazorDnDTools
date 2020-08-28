using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public class TrinketTable
    {
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
    }
}
