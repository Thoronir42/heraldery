using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
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

        public String FieldDivision(FieldDivisionType divisionType)
        {
            return divisionType.ToString();
        }
    }
}
