using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.SyntacticAnalysis
{
    public class TokenCreator
    {
        public Token Tincture(TinctureType type, string text)
        {
            return new Token { Definition = new TinctureDefinition { TinctureType = type, Text = text } };
        }

        public Token TinctureFur(string pattern, string primaryColor, string secondaryColor)
        {
            string text = pattern + FurFilling.PATTERN_SEPARATOR + primaryColor + FurFilling.COLOR_SEPARATOR + secondaryColor;
            return new Token { Definition = new TinctureDefinition { TinctureType = TinctureType.Fur, Text = text } };
        }

        public Token Number(NumberType type, int value)
        {
            return new Token { Definition = new NumberDefinition(value, type) };
        }

    }
}
