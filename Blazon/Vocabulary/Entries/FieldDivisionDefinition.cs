using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class FieldDivisionDefinition : Definition<FieldDivisionType>
    {
        public FieldDivisionType Type { get; }

        public FieldDivisionDefinition(FieldDivisionType type)
            : base(DefinitionType.FieldDivision, type)
        {
            this.Type = type;
        }
    }
}
