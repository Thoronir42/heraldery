using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Charges.Properties;

namespace Heraldry.Blazon.Vocabulary.Entries.ChargeProperties
{
    public class TailStylePropertyDefinition : ChargePropertyDefinition
    {
        public TailStyle Style { get; }

        public TailStylePropertyDefinition(TailStyle style) : base(PropertyType.TailStyle)
        {
            Style = style;
        }
    }
}
