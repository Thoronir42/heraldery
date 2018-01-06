using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges.Properties
{
    public class FeatureProperty : ChargeProperty
    {
        public ChargeFeature Feature { get; }
        public Filling Filling { get; set; }

        public FeatureProperty(ChargeFeature feature, Filling filling = null) : base(PropertyType.Feature)
        {
            Feature = feature;
            Filling = filling;
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
        Tail,
    }
}
