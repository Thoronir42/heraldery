using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
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

        public override void P(Charge charge)
        {
            if(charge is ShapeCharge)
            {
                PrintShapeCharge(charge as ShapeCharge);
                return;
            }
            if(charge is OrdinaryCharge)
            {
                PrintOrdinaryCharge(charge as OrdinaryCharge);
                return;
            }
            if(charge is SubordinaryCharge)
            {
                PrintSubordinaryCharge(charge as SubordinaryCharge);
                return;
            }
            if(charge is GenericCharge)
            {
                Print.Write(String.Format("[unrecognized charge {0}]", (charge as GenericCharge).Value));
                return;
            }
            
            throw new NotImplementedException();
        }

        private void PrintShapeCharge(ShapeCharge charge)
        {
            Print.Write(Define.Shape(charge.Shape));

            if(charge.Hole.HasValue)
            {
                if(charge.Hole.Value == charge.Shape)
                {
                    Print.Write(Define.ShapeType(ShapeType.Voided));
                } else if (charge.Hole.Value == Shape.Circle)
                {
                    Print.Write(Define.ShapeType(ShapeType.Pierced));
                } else
                {
                    Print.Write("[hole of shape " + charge.Hole.Value.ToString() + "]");
                }
            }

            Print.Filling.P(charge.Filling);
        }

        private void PrintOrdinaryCharge(OrdinaryCharge charge)
        {

        }

        private void PrintSubordinaryCharge(SubordinaryCharge charge)
        {

        }
    }
}
