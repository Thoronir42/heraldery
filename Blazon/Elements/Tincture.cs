using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    [DebuggerDisplay("Tincture[{TinctureType}] = {Value}")]
    public class Tincture
    {
        public string Value { get; set; }
        public TinctureType TinctureType { get; set; }

        public Tincture(TinctureType type, string value)
        {
            this.TinctureType = type;
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            var tincture = obj as Tincture;
            return tincture != null &&
                   Value == tincture.Value &&
                   TinctureType == tincture.TinctureType;
        }

        public override int GetHashCode()
        {
            var hashCode = -666361299;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + TinctureType.GetHashCode();
            return hashCode;
        }
    }

    public enum TinctureType
    {
        /// <summary> Solid colour </summary>
        Colour,
        /// <summary> Metallic colour </summary>
        Metal,
        /// <summary> Fur or pattern </summary>
        Fur,
        /// <summary> String representation of HTML color </summary>
        Html,
    }
}
