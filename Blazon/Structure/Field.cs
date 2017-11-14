using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class Field
    {
        public Filling Background { get; set; }
        public Charge Charge { get; set; }

        public FieldDivisionType? Division { get; set; } = null;
        public FieldDivisionLine? Line { get; set; } = null;

        public Field[] Subfields { get; set; } = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Field(){}

        /// <summary>
        /// Constructs field with solid color as a background.
        /// </summary>
        /// <param name="tincture"></param>
        public Field(Filling tincture) 
        {
            Background = tincture;
        }

        /// <summary>
        /// Construct field from quaterly definition.
        /// Line type is defined as straight.
        /// </summary>
        /// <param name="divDefinition">Quaterly definition from which the field will be constructed.</param>
        public Field(QuaterlyDivisionDefinition divDefinition) {
            Division = divDefinition.Type;
            Line = FieldDivisionLine.Straight;
            Subfields = divDefinition.Fields.ToArray();
        }
    }
}
