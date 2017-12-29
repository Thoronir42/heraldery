using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class ShapeCharge : Charge
    {
        public Shape Shape { get; set; }
        public Shape? Hole { get; set; }

        public string ImplicitFilling { get; set; }

        public ShapeCharge()
        {
            this.Type = ChargeType.SimpleShape;
        }

        public override bool Equals(object obj)
        {
            var charge = obj as ShapeCharge;
            return charge != null &&
                   base.Equals(obj) &&
                   EqualityComparer<Filling>.Default.Equals(Filling, charge.Filling) &&
                   Shape == charge.Shape &&
                   EqualityComparer<Shape?>.Default.Equals(Hole, charge.Hole);
        }

        public override int GetHashCode()
        {
            var hashCode = 1024802945;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Filling>.Default.GetHashCode(Filling);
            hashCode = hashCode * -1521134295 + Shape.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Shape?>.Default.GetHashCode(Hole);
            return hashCode;
        }
    }
}
