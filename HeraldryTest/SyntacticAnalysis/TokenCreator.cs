using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
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
    /// <summary>
    /// Syntactic sugar for token instantiating
    /// </summary>
    public class TokenCreator
    {
        private int n = 0;

        public Token Tincture(TinctureType type, string text)
        {
            return Tincture(new Tincture(type, text));
        }

        public Token Tincture(Tincture tincture)
        {
            return new Token(++n, new TinctureDefinition(tincture));
        }

        public Token TinctureFur(string pattern, string primaryColor, string secondaryColor)
        {
            return Tincture(new FurTincture(pattern, new Tincture(TinctureType.Html, primaryColor), new Tincture(TinctureType.Html, secondaryColor)));
        }

        public Token Number(NumberType type, int value)
        {
            return new Token(++n, new NumberDefinition(value, type));
        }

        public Token Number(int value)
        {
            return this.Number(NumberType.Cardinal, value);
        }

        public Token FieldDivision(FieldDivisionType type)
        {
            return new Token(++n, new FieldDivisionDefinition(type));
        }

        public Token FieldVariation(FieldVariationType type)
        {
            return new Token(++n, new FieldVariationDefinition(type));
        }

        public Token Ordinary(Ordinary type, OrdinarySize size)
        {
            return new Token(++n, new OrdinaryDefinition(type, size));
        }


        public Token Keyword(KeyWord keyword)
        {
            return new Token(++n, new KeyWordDefinition(keyword));
        }

        public Token Separator(Separator separator)
        {
            return new Token(++n, new SeparatorDefinition(separator));
        }

    }
}
