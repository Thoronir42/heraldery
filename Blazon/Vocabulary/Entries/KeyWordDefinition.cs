using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class KeyWordDefinition : Definition<KeyWord>
    {
        public KeyWord KeyWord { get; }

        public KeyWordDefinition(KeyWord keyword) : base(DefinitionType.KeyWord, keyword)
        {
            this.KeyWord = keyword;
        }
    }
}
