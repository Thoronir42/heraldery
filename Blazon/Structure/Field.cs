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
        // todo: move these to ChargeField
        public Filling Background { get; set; }
        public Charge Charge { get; set; }

        // todo: move these to FieldDivision class
        public FieldDivisionType? Division { get; set; } = null;
        public FieldDivisionLine Line { get; set; } = FieldDivisionLine.Straight;

        public Field[] Subfields { get; set; } = null;
    }
}
