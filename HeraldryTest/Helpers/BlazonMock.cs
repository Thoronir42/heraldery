using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
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
            return new DividedField()
            {
                Division = divisionType,
                Subfields = fields,
            };
        }

        public static ContentField SolidColorField(String color, TinctureType tinctureType)
        {
            return new ContentField()
            {
                Background = SolidFilling(color, tinctureType)
            };
        }


        public static Filling SolidFilling(String color, TinctureType tinctureType)
        {
            return SolidFilling(new Tincture { TinctureType = tinctureType, Value = color });
        }
        public static Filling SolidFilling(Tincture tincture)
        {
            return new Filling(tincture);
        }

    }
}
