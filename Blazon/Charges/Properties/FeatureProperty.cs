using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    class FeatureProperty : ChargeProperty
    {
        public ChargeFeature Feature { get; }
        public Filling Tincture { get; }

        public FeatureProperty(ChargeFeature feature, Filling tincture) : base(PropertyType.Feature)
        {
            Feature = feature;
            Tincture = tincture;
        }
    }

    public enum ChargeFeature
    {
        ClawsHornsTusks,
        Tongue,
        Penis,
        AntlersHorns,
        Hooves,
        ManeHair,
        Crown,

    }
}
