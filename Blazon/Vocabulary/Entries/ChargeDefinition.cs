using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class ChargeDefinition<TCharge> : Definition where TCharge : Charge
    {
        public TCharge Charge { get; }

        public ChargeDefinition(TCharge charge)
        {
            this.Charge = charge;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Charge;
        }

        public override object GetSubtype()
        {
            if (Charge == null)
            {
                return ChargeType.Generic;
            }

            return Charge.Type;
        }
    }
}
