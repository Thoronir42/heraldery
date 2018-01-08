using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Entries.ChargeProperties;
using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary
{
    public class VocabularyDefiner
    {
        private readonly BlazonVocabulary vocabulary;

        internal VocabularyDefiner(BlazonVocabulary vocabulary)
        {
            this.vocabulary = vocabulary;
        }

        public string Tincture(Tincture tincture) => Tincture(tincture.Value, tincture.TinctureType);
        public string Tincture(string value, TinctureType type)
        {
            var definition = FindDefinition(vocabulary.Tinctures, (d) => d.Tincture.Value == value && d.Tincture.TinctureType == type);
            return FormatDefinition(definition);
        }

        public string FieldDivision(FieldDivisionType divisionType)
        {
            var definition = FindDefinition(vocabulary.FieldDivisions, (d) => d.Type == divisionType);
            return FormatDefinition(definition);
        }

        public string FieldDivisionLine(FieldDivisionLine type)
        {
            var definition = FindDefinition(vocabulary.FieldDivisionLines, (d) => d.Line == type);
            return FormatDefinition(definition);
        }

        public string FieldVariation(FieldVariationType type)
        {
            var definition = FindDefinition(vocabulary.FieldVariations, d => d.VariationType == type);
            return FormatDefinition(definition);
        }

        public string Position(Position position)
        {
            var definition = FindDefinition(vocabulary.Positions, (d) => position.Equals(d.Position));
            return FormatDefinition(definition, "[position {0}]", position.ToString());
        }

        public string Keyword(KeyWord keyword)
        {
            var def = FindDefinition(vocabulary.KeyWords, d => d.KeyWord == keyword);
            return FormatDefinition(def);
        }

        public string Separator(Separator separator)
        {
            return BlazonVocabulary.Separator(separator);
        }

        public string Ordinary(Ordinary ordinary, OrdinarySize size)
        {
            var def = FindDefinition(vocabulary.Ordinaries, d => d.Type == ordinary && d.Size == size);
            return FormatDefinition(def, "[ordinary {0} {1}]", ordinary.ToString(), size.ToString());
        }

        public string Shape(Shape shape)
        {
            var def = FindDefinition(vocabulary.Shapes, (d) =>
            {
                ShapeCharge defCharge = d.Charge as ShapeCharge;
                return defCharge != null && defCharge.Shape == shape && defCharge.ImplicitFilling == null;
            });

            return FormatDefinition(def, "[shape {0}]", shape.ToString());
        }

        public string ShapeType(ShapeType type)
        {
            var def = FindDefinition(vocabulary.ShapeTypes, d => d.ShapeType == type);
            return FormatDefinition(def, "[shape type " + type.ToString() + "]");
        }

        public string Number(int value, NumberType type)
        {
            return vocabulary.NumberVocabulary.FormatDigital(value, type);
        }

        public string ChargeTailStyle(TailStyle style, bool prefixTailWord = false)
        {
            String result = "";
            if (prefixTailWord || style == TailStyle.Regular)
            {
                var def = FindDefinition(vocabulary.ChargeProperties, d => d.TokenSubtype == PropertyType.Tail);
                result = FormatDefinition(def, "[tail]");

                if (style == TailStyle.Regular)
                {
                    return result;
                }
            }

            var styleDef = FindDefinition(vocabulary.ChargeProperties, d =>
            {
                return d.TokenSubtype == PropertyType.TailStyle && (d as TailStylePropertyDefinition).Style == style;
            });

            result += (prefixTailWord ? " " : "") + FormatDefinition(styleDef, "[tail style {0}]", style.ToString());
            return result;

        }

        public string ChargeAttitude(Attitude attitude)
        {
            var def = FindDefinition(vocabulary.ChargeProperties, d =>
            {
                return d.TokenSubtype == PropertyType.Attitude && (d as AttitudePropertyDefinition).Attitude == attitude;
            });

            return FormatDefinition(def, "[attitude {0}]", attitude.ToString());
        }

        public string ChargeAttitudeDirection(AttitudeDirection direction)
        {
            var def = FindDefinition(vocabulary.ChargeProperties, d =>
            {
                return d.TokenSubtype == PropertyType.AttitudeDirection && (d as AttitudeDirectionPropertyDefinition).Direction == direction;
            });

            return FormatDefinition(def, "[attitude {0}]", direction.ToString());
        }

        public string ChargeFeature(ChargeFeature feature)
        {
            var def = FindDefinition(vocabulary.ChargeProperties, d =>
            {
                return d.TokenSubtype == PropertyType.Feature && (d as FeaturePropertyDefinition).Feature == feature;
            });
            return FormatDefinition(def, "[feature {0}]", feature.ToString());
        }



        private TDefinition FindDefinition<TDefinition>(List<TDefinition> list, Func<TDefinition, bool> equalityCheck) where TDefinition : IDefinition
        {
            foreach (var def in list)
            {
                if (equalityCheck(def))
                {
                    return def;
                }
            }

            return default(TDefinition);
        }

        private string FormatDefinition(IDefinition definition, string fallback = null, params string[] formatArguments)
        {
            if (definition == null)
            {
                if (fallback != null)
                {
                    return String.Format(fallback, formatArguments);
                }

                // todo: maybe throw exception here, hmm?
                return "n/a";
            }

            return definition.Text;
        }
    }
}
