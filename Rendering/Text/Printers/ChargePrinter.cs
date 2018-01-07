using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    public class ChargePrinter : BasePrinter<Charge>
    {
        public ChargePrinter(RootPrinter printer) : base(printer)
        {
        }

        public override void P(Charge charge)
        {
            var printed = false;
            if (charge is ShapeCharge)
            {
                PrintShapeCharge(charge as ShapeCharge);
                printed = true;
            }
            if (charge is OrdinaryCharge)
            {
                PrintOrdinaryCharge(charge as OrdinaryCharge);
                printed = true;
            }
            if (charge is SubordinaryCharge)
            {
                PrintSubordinaryCharge(charge as SubordinaryCharge);
                printed = true;
            }
            if (charge is GenericCharge)
            {
                Print.Write(String.Format("[{0}]", (charge as GenericCharge).Value));
                printed = true;
            }

            PrintImmediateChargeProperties(charge);

            Print.Filling.P(charge.Filling);

            PrintTincturedFeatures(charge.Features);

            if (!printed)
            {
                throw new NotImplementedException();
            }
        }

        private void PrintShapeCharge(ShapeCharge charge)
        {
            Print.Write(Define.Shape(charge.Shape));

            if (charge.Hole.HasValue)
            {
                if (charge.Hole.Value == charge.Shape)
                {
                    Print.Write(Define.ShapeType(ShapeType.Voided));
                }
                else if (charge.Hole.Value == Shape.Circle)
                {
                    Print.Write(Define.ShapeType(ShapeType.Pierced));
                }
                else
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
            throw new NotImplementedException();
        }


        public void PrintImmediateChargeProperties(Charge charge)
        {
            if (charge.Attitude != null)
            {
                Print.Write(Define.ChargeAttitude(charge.Attitude.Attitude));
                if (charge.Attitude.Direction != AttitudeDirection.Forward)
                {
                    Print.Write(Define.ChargeAttitudeDirection(charge.Attitude.Direction));
                }
            }

            if (charge.Tail != null)
            {
                Print.Write(Define.ChargeTailStyle(charge.Tail.Style, charge.Tail.Style != TailStyle.QueueForchee));
            }
        }

        public void PrintTincturedFeatures(List<FeatureProperty> features) {
            Filling filling = null;
            List<string> featureList = new List<string>();
            foreach (var property in features)
            {
                if (filling == null)
                {
                    filling = property.Filling;
                }

                if (!property.Filling.Equals(filling))
                {
                    PrintFeatureList(featureList, filling);
                    featureList.Clear();
                    filling = property.Filling;
                }

                featureList.Add(Define.ChargeFeature(property.Feature));
            }

            if (featureList.Count > 0)
            {
                PrintFeatureList(featureList, filling);
            }
        }

        private void PrintFeatureList(List<string> list, Filling filling)
        {

            Print.WriteList(list);
            Print.Filling.P(filling);
        }
    }
}
