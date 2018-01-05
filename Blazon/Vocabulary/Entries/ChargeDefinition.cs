using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class ChargeDefinition : Definition<ChargeType>
    {
        public Charge Charge { get; }

        public ChargeDefinition(Charge charge) : base(DefinitionType.Charge, charge.Type)
        {
            this.Charge = charge;
        }
    }
}
