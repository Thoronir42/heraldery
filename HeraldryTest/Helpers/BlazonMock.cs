using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.Helpers
{
    static class BlazonMock
    {
        public static DividedField DividedField(FieldDivisionType divisionType, params Field[] fields)
        {
            return new DividedField(divisionType)
            {
                Subfields = fields,
            };
        }

        public static ContentField SolidColorField(string color, TinctureType tinctureType = TinctureType.Colour)
        {
            return new ContentField(SolidFilling(color, tinctureType));
        }

        public static Filling SolidFilling(string color, TinctureType tinctureType = TinctureType.Colour)
        {
            return SolidFilling(new Tincture(tinctureType, color));
        }
        public static Filling SolidFilling(Tincture tincture)
        {
            return new SolidFilling(tincture);
        }

    }
}
