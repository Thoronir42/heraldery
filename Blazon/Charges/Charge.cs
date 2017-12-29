using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class Charge
    {
        public String Value { get; set; }

        public Filling Filling { get; set; }
        public ChargeType Type { get; protected set; } = ChargeType.Generic;

        public override bool Equals(object obj)
        {
            var charge = obj as Charge;
            return charge != null &&
                   Value == charge.Value &&
                   Type == charge.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1574892647;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }

    public enum ChargeType
    {
        Generic,
        SimpleShape,
        Ordinary,
        Cross,
    }
}
