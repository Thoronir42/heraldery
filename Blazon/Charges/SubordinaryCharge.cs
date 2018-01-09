using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class SubordinaryCharge : InnerFieldCharge
    {
        public Subordinary Subordinary { get; }

        public SubordinaryCharge(Subordinary subordinary, ContentField content) : base(ChargeType.Subordinary, content)
        {
            Subordinary = subordinary;
        }
    }
}
