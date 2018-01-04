using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class GenericCharge : Charge
    {
        public String Value { get; set; }

        public GenericCharge(string value) : base(ChargeType.Generic)
        {
            this.Value = value;
        }
    }
}
