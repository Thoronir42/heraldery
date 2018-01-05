using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class ChargeDefinition<TCharge> : Definition<ChargeType> where TCharge : Charge
    {
        public TCharge Charge { get; }

        public ChargeDefinition(TCharge charge) : base(DefinitionType.Charge, charge.Type)
        {
            this.Charge = charge;
        }
    }
}
