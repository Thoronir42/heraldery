using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Numbers
{
    class CzechNumberVocabulary : NumberVocabulary
    {
        private String nThPattern = "(\\d+)(.?)";

        public override string FormatDigital(int value, NumberType type)
        {
            if(type == NumberType.Cardinal)
            {
                return value.ToString();
            }

            return value + ".";
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
            bool isOrdinal = match.Groups[1].Length != 0;

            int value = int.Parse(match.Groups[1].Value);
            return new Number(value, isOrdinal ? NumberType.Ordinal : NumberType.Cardinal);
        }
    }
}
