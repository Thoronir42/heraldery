using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Charges.Properties;

namespace Heraldry.Blazon.Vocabulary.Entries.ChargeProperties
{
    public class AttitudeDirectionPropertyDefinition : ChargePropertyDefinition
    {
        public AttitudeDirection Direction { get; }

        public AttitudeDirectionPropertyDefinition(AttitudeDirection direction) : base(PropertyType.AttitudeDirection)
        {
            Direction = direction;
        }
    }
}
