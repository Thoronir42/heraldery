using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class NumberDefinition : Definition
    {
        public NumberType Type { get; set; }
        public int Value { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Number;
        }
    }

    public enum NumberType
    {
        /// <summary> One, two, 3, 4, ... </summary>
        Cardinal,
        /// <summary>First, 2nd, 3rd, ...</summary>
        Ordinal,
    }
}
