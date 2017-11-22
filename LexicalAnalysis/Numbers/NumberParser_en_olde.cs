using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis.Numbers
{
    class NumberParser_en_olde : NumberParser
    {
        private String nThPattern = "(\\d+)(st|nd|rd|th)";

        public override Token FindNumber(string text)
        {
            Match match = Regex.Match(text, nThPattern);
            if(!match.Success)
            {
                return null;
            }

            var value = int.Parse(match.Groups[1].Value);
            var def = new NumberDefinition() { Text = match.Groups[0].Value, Type = Blazon.Vocabulary.Entries.NumberType.Ordinal, Value = value };

            return new Token() { Definition = def, Position = match.Index };
        }
    }
}
