using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Numbers
{
    public abstract class NumberVocabulary
    {

        public string FormatDigital(Number number)
        {
            return FormatDigital(number.Value, number.Type);
        }

        public abstract string FormatDigital(int value, NumberType type);

        public Number FindInText(string text, out int index, out int length)
        {
            return FindIntegers(text, out index, out length);
        }

        abstract protected Number FindIntegers(string text, out int index, out int length);

        
    }
}
