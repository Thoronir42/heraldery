using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Vocabulary.Entries;

namespace Heraldry.Blazon.Vocabulary
{
    public class BlazonVocabulary
    {
        internal List<TinctureDefinition> Tinctures { get; set; } // todo: Todd, hide the setter, god dammit
        internal List<FieldDivisionDefinition> FieldDivisions { get; set; }
        internal List<FieldDivisionLineDefinition> FieldDivisionLines { get; set; }
        internal List<PositionDefinition> Positions { get; set; }
        internal List<KeyWordDefinition> KeyWords { get; set; }
        internal List<NumberDefinition> Numbers { get; set; }
        internal List<OrdinaryDefinition> Ordinaries { get; set; }
        internal List<SubordinaryDefinition> Subordinaries { get; set; }

        internal BlazonVocabulary()
        {
            
        }

        public List<Definition> GetAllDefinitions(Boolean sortByLength = false)
        {
            var list = new List<Definition>();

            list.AddRange(this.Tinctures);
            list.AddRange(this.FieldDivisions);
            list.AddRange(this.FieldDivisionLines);
            list.AddRange(this.Positions);
            list.AddRange(this.KeyWords);
            list.AddRange(this.Numbers);
            list.AddRange(this.Ordinaries);
            list.AddRange(this.Subordinaries);

            if (sortByLength)
            {
                return list.OrderByDescending(o => o.Text.Length).ToList();
            }

            return list;
        }

        public VocabularyDefiner GetDefiner()
        {
            return new VocabularyDefiner(this);
        }
    }
}
