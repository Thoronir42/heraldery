using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class SeparatorDefinition : Definition<Separator>
    {
        public Separator Separator { get; }

        public SeparatorDefinition(Separator separator) : base(DefinitionType.Separator, separator)
        {
            this.Separator = separator;
        }
    }
}
