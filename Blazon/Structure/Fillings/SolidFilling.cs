using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Fillings
{
    public class SolidFilling : Filling
    {
        public Tincture Tincture { get; set; }

        public SolidFilling(Tincture tincture)
        {
            this.Tincture = tincture;
        }

        public override bool Equals(object obj)
        {
            var filling = obj as SolidFilling;
            return filling != null &&
                   EqualityComparer<Tincture>.Default.Equals(Tincture, filling.Tincture);
        }

        public override int GetHashCode()
        {
            return -144272061 + EqualityComparer<Tincture>.Default.GetHashCode(Tincture);
        }
    }
}
