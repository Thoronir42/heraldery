using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    public class FillingPrinter : BasePrinter<Filling>
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

            if(item is SemeFilling)
            {
                SemeFilling semeFilling = item as SemeFilling;
                Print.Write(Define.FieldVariation(FieldVariationType.SemeOf));
                Print.Charge.P(semeFilling.Charge);
                return;
            }

            if(item is PatternFilling)
            {
                PatternFilling patternFilling = item as PatternFilling;

                Print.Write(Define.FieldVariation(patternFilling.Type));
                if(patternFilling.HasNumber)
                {
                    Print.Write(Define.Number(patternFilling.Number, NumberType.Cardinal));
                }

                Tincture(patternFilling.PrimaryTincture);
                Print.Write(KeyWord.And);
                Tincture(patternFilling.SecondaryTincture);

            }

        }

        public void Tincture(Tincture tincture)
        {
            if(tincture.TinctureType == TinctureType.Colour || tincture.TinctureType == TinctureType.Metal)
            {
                Print.Write(Define.Tincture(tincture));
            }
        }


    }
}
