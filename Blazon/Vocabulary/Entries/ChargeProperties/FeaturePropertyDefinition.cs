using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Charges.Properties;

namespace Heraldry.Blazon.Vocabulary.Entries.ChargeProperties
{
    public class FeaturePropertyDefinition : ChargePropertyDefinition
    {
        public ChargeFeature Feature { get; }

        public FeaturePropertyDefinition(ChargeFeature feature) : base(PropertyType.Feature)
        {
            Feature = feature;
        }
    }
}
