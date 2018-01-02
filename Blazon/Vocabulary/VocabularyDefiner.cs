using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
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

        public String Tincture(Tincture tincture)
        {
            return Tincture(tincture.Value, tincture.TinctureType);
        }

        public String Tincture(string value, TinctureType type)
        {
            var definition = FindDefinition(vocabulary.Tinctures, (d) => d.Tincture.Value == value && d.Tincture.TinctureType == type);
            return FormatDefinition(definition);
        }

        public String FieldDivision(FieldDivisionType divisionType)
        {
            var definition = FindDefinition(vocabulary.FieldDivisions, (d) => d.Type == divisionType);
            return FormatDefinition(definition);
        }

        public String FieldDivisionLine(FieldDivisionLine type)
        {
            var definition = FindDefinition(vocabulary.FieldDivisionLines, (d) => d.Line == type);
            return FormatDefinition(definition);
        }

        public String FieldVariation(FieldVariationType type)
        {
            var definition = FindDefinition(vocabulary.FieldVariations, d => d.VariationType == type);
            return FormatDefinition(definition);
        }

        public String Position(Position position)
        {
            var definition = FindDefinition(vocabulary.Positions, (d) => position.Equals(d.Position));
            return FormatDefinition(definition);
        }

        public String Keyword(KeyWord keyword)
        {
            var def = FindDefinition(vocabulary.KeyWords, d => d.KeyWord == keyword);
            return FormatDefinition(def);
        }

        public String Charge(Charge charge)
        {
            

            return String.Format("[charge {0}]", charge.Value);
        }

        public String Number(int value, NumberType type)
        {
            return vocabulary.NumberVocabulary.FormatDigital(value, type);
        }




        private DefinitionT FindDefinition<DefinitionT>(List<DefinitionT> list, Func<DefinitionT, bool> equalityCheck) where DefinitionT : Definition
        {
            foreach (var def in list)
            {
                if (equalityCheck(def))
                {
                    return def;
                }
            }

            return null;
        }

        private string FormatDefinition (Definition definition, string fallback = null)
        {
            if(definition == null)
            {
                if(fallback != null)
                {
                    return fallback;
                }

                // todo: maybe throw exception here, hmm?
                return "n/a";
            }

            return definition.Text;
        }
    }
}
