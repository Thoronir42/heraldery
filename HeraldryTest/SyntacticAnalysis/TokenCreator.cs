using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Entries.ChargeProperties;
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

        public Token FromDefinition(IDefinition definition)
        {
            return new Token(++n, definition);
        }

        public Token Tincture(Tincture tincture) => FromDefinition(new TinctureDefinition(tincture));

        public Token Tincture(TinctureType type, string text) => Tincture(new Tincture(type, text));

        public Token TinctureFur(string pattern, string primaryColor, string secondaryColor)
        {
            return Tincture(new FurTincture(pattern, new Tincture(TinctureType.Html, primaryColor), new Tincture(TinctureType.Html, secondaryColor)));
        }

        public Token Number(NumberType type, int value) => FromDefinition(new NumberDefinition(value, type));

        public Token Number(int value) => this.Number(NumberType.Cardinal, value);

        public Token FieldDivision(FieldDivisionType type) => FromDefinition(new FieldDivisionDefinition(type));

        public Token FieldVariation(FieldVariationType type) => FromDefinition(new FieldVariationDefinition(type));

        public Token Ordinary(Ordinary type, OrdinarySize size) => FromDefinition(new OrdinaryDefinition(type, size));


        public Token Keyword(KeyWord keyword) => FromDefinition(new KeyWordDefinition(keyword));

        public Token Separator(Separator separator) => FromDefinition(new SeparatorDefinition(separator));

        public Token GenericCharge(String text) => FromDefinition(new ChargeDefinition(new GenericCharge(text)));


        public Token ChargeProperty(PropertyType type) => FromDefinition(new ChargePropertyDefinition(type));

        public Token FeatureProperty(ChargeFeature feature) => FromDefinition(new FeaturePropertyDefinition(feature));

        public Token TailStyleProperty(TailStyle style) => FromDefinition(new TailStylePropertyDefinition(style));

        public Token AttitudeProperty(Attitude attitude) => FromDefinition(new AttitudePropertyDefinition(attitude));

        public Token AttitudeProperty(AttitudeDirection direction) => FromDefinition(new AttitudeDirectionPropertyDefinition(direction));
    }
}
