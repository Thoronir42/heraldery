using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Numbers
{
    public class Number
    {
        public static explicit operator Number(int n)
        {
            return new Number(n);
        }
        public static explicit operator int(Number n)
        {
            return n.Value;
        }

        public NumberType Type { get; }
        public int Value { get; }

        public Number(int value, NumberType type = NumberType.Cardinal)
        {
            this.Value = value;
            this.Type = type;
        }

        public override bool Equals(object obj)
        {
            var number = obj as Number;
            return number != null &&
                   Type == number.Type &&
                   Value == number.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1265339359;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
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
