using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDBlazorReference.Client.Data
{
    /// <summary>
    /// Should handle any roll where nDm is given.
    /// 
    /// Standard rolls are likely to include:
    /// - D4, D6, D8, D10, D12, D20
    /// 
    /// Input will exist as the formula directly, or preceeded by '/r'
    /// </summary>
    public class DiceRoller
    {
        public struct RollFormula
        {
            public string Formula { get; set; }
        }

        public Regex Regex { get; private set; }
        public Regex AltRegex { get; private set; }

        public Regex EquationRegex { get; private set; }
 
        public Random Random { get; set; }

        public DiceRoller()
        {
            Random = new Random((int)DateTime.UtcNow.Ticks);

            Regex = new Regex(@"(?<num_dice>\d+)[dD](?<size_dice>\d+)");
            AltRegex = new Regex(@"[dD](?<size_dice>\d+)");

            // non functional
            EquationRegex = new Regex(@"\((?'sub_equation'\S+?[^()]?\S+?)\)");
        }

        // ((x)+(y)) + (z) + 0

        public KeyValuePair<float, string> ProcessInputString(string aInstruction)
        {
            // remove all whitespace
            aInstruction = aInstruction.Replace(" ", "");

            MatchCollection matches = EquationRegex.Matches(aInstruction);
            string outputString = string.Empty;

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                foreach (Group group in groups)
                {
                    if (outputString.Length > 0)
                    {
                        outputString += " : ";
                    }
                    outputString += $"[{group.Name} {group.Value}]";
                }
                outputString += " , ";
            }

            // perform bodmas

            return new KeyValuePair<float, string>(0, outputString);
            //return ProcessDice(aInstruction);
        }

        public KeyValuePair<float, string> ProcessDice(string aDiceInstruction)
        {
            MatchCollection matches = null;

            matches = Regex.Matches(aDiceInstruction);

            if (matches.Count == 0)
            {
                matches = AltRegex.Matches(aDiceInstruction);

                if (matches.Count == 0)
                {
                    throw new Exception($"Bad instruction: {aDiceInstruction}");
                }
            }

            Match match = matches[0];
            GroupCollection groups = match.Groups;

            int numDice = 1;
            if (groups["num_dice"].Success)
            {
                numDice = int.Parse(groups["num_dice"].Value);
            }
            int sizeDice = int.Parse(groups["size_dice"].Value);

            if (sizeDice < 1)
            {
                throw new Exception($"Bad instruction: {aDiceInstruction}, dice size must be positive.");
            }

            float outValue = 0f;
            string outputString = string.Empty;
            for (int i = 0; i < numDice; ++i)
            {
                float val = Random.Next(1, sizeDice);

                if (outputString.Length > 0)
                {
                    outputString += "+";
                }
                outputString += $"[{val}]";

                outValue += val;
            }

            outputString += $" = {outValue}";

            return new KeyValuePair<float, string>(outValue, outputString);
        }
    }
}
