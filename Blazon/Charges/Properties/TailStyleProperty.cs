using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    class TailStyleProperty : ChargeProperty
    {
        public TailStyle Style { get; }

        public TailStyleProperty(TailStyle style) : base(PropertyType.TailStyle)
        {
            Style = style;
        }

        public override bool Equals(object obj)
        {
            var property = obj as TailStyleProperty;
            return property != null &&
                   Style == property.Style;
        }

        public override int GetHashCode()
        {
            return -1755968282 + Style.GetHashCode();
        }
    }

    public enum TailStyle
    {
        Regular,
        Coward,
        Fourche,
        Saltire,
    }
}
