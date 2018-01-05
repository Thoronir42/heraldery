using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Vocabulary.Entries.ChargeProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class ChargePropertyDefinition : Definition<PropertyType>
    {
        public ChargePropertyDefinition(PropertyType property) : base(DefinitionType.ChargeProperty, property)
        {
        }
    }
}
