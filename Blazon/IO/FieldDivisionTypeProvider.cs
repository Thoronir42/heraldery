using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldery.Blazon.IO
{
    class FieldDivisionTypeProvider : CsvLoader<FieldDivision>
    {
        public FieldDivisionTypeProvider(string fileName) : base(fileName)
        {
        }

        protected override FieldDivision ParseLine(string[] parts)
        {
            string name = parts[0];
            FieldDivisionType type = (FieldDivisionType)Enum.Parse(typeof(FieldDivisionType), parts[1]);

            return new FieldDivision() { Name = name, Type = type};
        }
    }
}
