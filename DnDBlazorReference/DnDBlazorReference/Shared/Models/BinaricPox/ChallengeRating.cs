using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using DnDBlazorReference.Shared.Models.FiveETools;

namespace DnDBlazorReference.Shared.Models.BinaricPox
{
    public static class ChallengeRatingUtility
    {
        public static ChallengeRating ConvertToUsable(this ChallengeRatingEntry5e cr5e)
        {
            return new ChallengeRating()
            {
                CR = cr5e.ChallengeRating,
                InCoven = cr5e.Coven,
                InLair = cr5e.Lair,
            };
        }
    }
    
    public class ChallengeRating
    {
        public string CR { get; set; }
        public string InCoven { get; set; }
        public string InLair { get; set; }

        public override string ToString()
        {
            string output = CR;

            if (!string.IsNullOrEmpty(InCoven))
            {
                output = $"{output}, {InCoven} in a coven";
            }

            if (!string.IsNullOrEmpty(InLair))
            {
                output = $"{output}, {InLair} in the creature's lair";
            }

            return output;
        }
    }
}
