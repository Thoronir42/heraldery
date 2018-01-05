using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Charges;

namespace Heraldry.Blazon.Vocabulary.Entries.ChargeProperties
{
    public class AttitudePropertyDefinition : ChargePropertyDefinition
    {
        public ChargeType[] ExclusiveTo { get; }

        public Attitude Attitude { get; }

        public AttitudePropertyDefinition(Attitude attitude, params ChargeType[] exclusiveTo) : base(PropertyType.Attitude)
        {
            Attitude = attitude;
            this.ExclusiveTo = exclusiveTo;
        }
    }
}
