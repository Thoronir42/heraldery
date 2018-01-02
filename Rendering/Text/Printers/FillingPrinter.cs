using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    class FillingPrinter : BasePrinter<Filling>
    {
        public FillingPrinter(RootPrinter root) : base(root)
        {

        }

        public override void P(Filling item)
        {
            if(item is SolidFilling)
            {
                SolidFilling solidFilling = item as SolidFilling;
                Print.Write(Define.Tincture(solidFilling.Tincture));
                return;
            }

            if(item is FurFilling)
            {
                // todo: print furs
            }

            if(item is SemeFilling)
            {
                SemeFilling semeFilling = item as SemeFilling;
                Print.Write(Define.FieldVariation(FieldVariationType.SemeOf));
                Print.Charge.P(semeFilling.Charge);
                return;
            }

            if(item is PatternFilling)
            {
                // todo: print patterns
            }

        }


    }
}
