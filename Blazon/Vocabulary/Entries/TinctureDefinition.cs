using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class TinctureDefinition : Definition
    {
        public TinctureType Type { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Tincture;
        }

        public override string ToString()
        {
            return String.Format("Tincture {0}:{1} [{2}]", this.Text, this.Type);
        }
    }
}
