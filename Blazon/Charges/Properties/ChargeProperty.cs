using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    public class ChargeProperty
    {
        public PropertyType Type { get; }

        public ChargeProperty(PropertyType type)
        {
            Type = type;
        }
    }

    public enum PropertyType
    {
        Tail,
        Crown,
    }
}
