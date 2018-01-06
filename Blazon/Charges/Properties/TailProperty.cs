using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    public class TailProperty : FeatureProperty
    {
        public TailStyle Style { get; }

        public TailProperty(TailStyle style, Filling filling = null) : base(ChargeFeature.Tail, filling)
        {
            Style = style;
        }

        public override bool Equals(object obj)
        {
            var property = obj as TailProperty;
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
        QueueForchee,
    }
}
