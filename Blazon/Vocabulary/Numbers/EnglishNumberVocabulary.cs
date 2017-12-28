using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Numbers
{
    public class EnglishNumberVocabulary : NumberVocabulary
    {
        private String nThPattern = "(\\d+)(st|nd|rd|th)?";

        public override string FormatDigital(int value, NumberType type)
        {
            if (type == NumberType.Cardinal)
            {
                return value.ToString();
            }

            return value + OrdinalSuffix(value);
        }

        protected override Number FindIntegers(string text, out int index, out int length)
        {
            Match match = Regex.Match(text, nThPattern);
            if (!match.Success)
            {
                index = -1; length = 0;
                return null;
            }

            index = match.Index;
            length = match.Groups[0].Length;

            bool isOrdinal = match.Groups[2].Length != 0;

            // todo: enhance ordinal suffix matching

            return new Number {
                Value = int.Parse(match.Groups[1].Value),
                Type = isOrdinal ? NumberType.Ordinal : NumberType.Cardinal,
            };
        }

        private string OrdinalSuffix(int n)
        {
            if (n >= 11 && n <= 20)
            {
                return "th";
            }

            int mod = n % 10;
            switch (mod)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
            }

            return "th";
        }
    }
}
