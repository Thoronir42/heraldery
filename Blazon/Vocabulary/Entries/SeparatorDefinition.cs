using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class SeparatorDefinition : Definition
    {
        public Separator Separator { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Separator;
        }

        public override object GetSubtype()
        {
            return Separator;
        }
    }
}
