using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    class Field
    {
        Filling Background { get; set; }
        Charge Charge { get; set; }

        public FieldDivisionType? Division { get; set; } = null;
        public FieldDivisionVariant? Line { get; set; } = null;

        public Field[] Subfields { get; set; } = null;
    }
}
