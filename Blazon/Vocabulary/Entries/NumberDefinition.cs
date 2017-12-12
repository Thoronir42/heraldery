using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class NumberDefinition : Definition
    {
        public NumberType Type { get; set; }
        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            var definition = obj as NumberDefinition;
            return definition != null &&
                   Type == definition.Type &&
                   Value == definition.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1265339359;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

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
