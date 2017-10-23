using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldery.Blazon.IO
{
    class TinctureProvider : CsvLoader<Tincture>
    {
        public TinctureProvider(string fileName) : base(fileName)
        {
        }

        protected override Tincture ParseLine(string[] parts)
        {
            TinctureType type = (TinctureType)Enum.Parse(typeof(TinctureType), parts[1]);
            return new Tincture() { Name = parts[0], Type = type, Parameters = parts[2] };
        }
    }
}
