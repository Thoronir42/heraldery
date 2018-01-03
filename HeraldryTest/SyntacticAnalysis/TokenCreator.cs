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
        public Token Tincture(TinctureType type, string text)
        {
            return Tincture(new Tincture(type, text));
        }

        public Token Tincture(Tincture tincture)
        {
            return new Token
            {
                Definition = new TinctureDefinition(tincture)
            };
        }

        public Token TinctureFur(string pattern, string primaryColor, string secondaryColor)
        {
            return Tincture(new FurTincture(pattern, primaryColor, secondaryColor));
        }

        public Token Number(NumberType type, int value)
        {
            return new Token { Definition = new NumberDefinition(value, type) };
        }

        public Token Number(int value)
        {
            return this.Number(NumberType.Cardinal, value);
        }

        public Token FieldDivision(FieldDivisionType type)
        {
            return new Token { Definition = new FieldDivisionDefinition { Type = type } };
        }

        public Token FieldVariation(FieldVariationType type)
        {
            return new Token { Definition = new FieldVariationDefinition { VariationType = type } };
        }

        public Token Ordinary(Ordinary type, OrdinarySize size)
        {
            return new Token { Definition = new OrdinaryDefinition { Type = type, Size = size } };
        }


        public Token Keyword(KeyWord keyword)
        {
            return new Token { Definition = new KeyWordDefinition { KeyWord = KeyWord.And } };
        }

        public Token Separator(Separator separator)
        {
            return new Token { Definition = new SeparatorDefinition { Separator = separator } };
        }

    }
}
