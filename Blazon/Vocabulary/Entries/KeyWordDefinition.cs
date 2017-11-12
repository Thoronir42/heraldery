using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class KeyWordDefinition : Definition
    {
        public KeyWord KeyWord { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.KeyWord;
        }
    }
}
