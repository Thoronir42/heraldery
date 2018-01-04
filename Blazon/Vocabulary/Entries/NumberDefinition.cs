using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class NumberDefinition : Definition
    {
        public Number Number { get; }

        public NumberDefinition(Number number)
        {
            this.Number = number;
        }

        public NumberDefinition(int value, NumberType type) : this(new Number(value, type))
        {
        }

        public override bool Equals(object obj)
        {
            var definition = obj as NumberDefinition;
            return definition != null &&
                Number.Equals(definition.Number);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Number;
        }

        public override object GetSubtype()
        {
            return Number.Type;
        }
    }
}
