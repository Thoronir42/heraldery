using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    class CrownProperty : ChargeProperty
    {
        public Filling Tincture { get; }

        public CrownProperty(Filling tincture) : base(PropertyType.Crown)
        {
            Tincture = tincture;
        }
    }
}
