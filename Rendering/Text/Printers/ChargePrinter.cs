using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    class ChargePrinter : BasePrinter<Charge>
    {
        public ChargePrinter(RootPrinter printer) : base(printer)
        {
        }

        public override void P(Charge item)
        {
            // todo: print charge
            throw new NotImplementedException();
        }
    }
}
