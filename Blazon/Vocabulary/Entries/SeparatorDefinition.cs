using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class SeparatorDefinition : Definition
    {
        public Separator Separator { get; }

        public SeparatorDefinition(Separator separator)
        {
            this.Separator = separator;
        }

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
