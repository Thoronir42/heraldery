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
        public ContentField Field { get; }

        public SubordinaryCharge(Subordinary subordinary, ContentField field) : base(ChargeType.Subordinary)
        {
            Subordinary = subordinary;
            Field = field;
        }
    }
}
