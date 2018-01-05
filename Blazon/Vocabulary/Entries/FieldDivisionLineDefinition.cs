using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class FieldDivisionLineDefinition : Definition<FieldDivisionLine>
    {
        public FieldDivisionLine Line { get; }

        public FieldDivisionLineDefinition(FieldDivisionLine line) : base(DefinitionType.FieldDivision, line)
        {
            this.Line = line;
        }
    }
}
