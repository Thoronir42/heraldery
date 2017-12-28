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

        public String Charge(string text)
        {
            // todo: lookup simple charges

            return String.Format("[charge {0}]", text);
        }

        public String Tincture(string value, TinctureType type)
        {
            var definition = FindDefinition(vocabulary.Tinctures, (d) => d.Value == value && d.TinctureType == type);
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
