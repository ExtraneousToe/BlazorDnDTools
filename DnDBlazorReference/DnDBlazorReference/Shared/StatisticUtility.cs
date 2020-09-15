using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBlazorReference.Shared
{
    public static class StatisticUtility
    {
        public static int GetModifier(this int stat)
        {
            return (int) Math.Floor((stat - 10) / 2f);
        }

        public static string AsModifierString(this int stat)
        {
            int mod = stat.GetModifier();
            string sign = string.Empty;
            if (Math.Sign(mod) > 0)
            {
                sign = "+";
            }
            else if (Math.Sign(mod) < 0)
            { 
                sign = "-";
            }

            return $"{stat} ({sign}{Math.Abs(mod)})";
        }
    }
}
