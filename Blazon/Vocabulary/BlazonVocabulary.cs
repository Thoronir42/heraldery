using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.Blazon.Charges;

namespace Heraldry.Blazon.Vocabulary
{
    public class BlazonVocabulary
    {
        internal List<TinctureDefinition> Tinctures { get; set; }
        internal List<FieldDivisionDefinition> FieldDivisions { get; set; }
        internal List<FieldDivisionLineDefinition> FieldDivisionLines { get; set; }
        internal List<FieldVariationDefinition> FieldVariations { get; set; }
        internal List<PositionDefinition> Positions { get; set; }
        internal List<KeyWordDefinition> KeyWords { get; set; }
        internal List<NumberDefinition> Numbers { get; set; }
        internal List<OrdinaryDefinition> Ordinaries { get; set; }
        internal List<SubordinaryDefinition> Subordinaries { get; set; }
        internal List<ShapeTypeDefinition> ShapeTypes { get; set; }

        internal List<ChargeDefinition<ShapeCharge>> ShapeCharges { get; set; }

        internal NumberVocabulary NumberVocabulary { get; set; }

        internal BlazonVocabulary()
        {

        }

        public List<IDefinition> GetAllDefinitions(Boolean sortByLength = false)
        {
            var list = new List<IDefinition>();

            list.AddRange(this.Tinctures);
            list.AddRange(this.FieldDivisions);
            list.AddRange(this.FieldDivisionLines);
            list.AddRange(this.FieldVariations);
            list.AddRange(this.Positions);
            list.AddRange(this.KeyWords);
            list.AddRange(this.Numbers);
            list.AddRange(this.Ordinaries);
            list.AddRange(this.Subordinaries);
            list.AddRange(this.ShapeCharges);
            list.AddRange(this.ShapeTypes);

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
