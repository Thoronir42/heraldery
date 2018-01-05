using Heraldry.Blazon.Charges.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class PropertyDefinition : Definition<PropertyType>
    {
        public ChargeProperty Property { get; }

        public PropertyDefinition(ChargeProperty property) : base(DefinitionType.ChargeProperty, property.Type)
        {
            Property = property;
        }
    }
}
