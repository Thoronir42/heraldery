using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Fillings
{
    public class SemeFilling : Filling
    {
        public Tincture Background { get; set; }
        
        public Charge Charge { get; set; }


        public SemeFilling(Tincture background, Charge charge)
        {
            this.Background = background;
            this.Charge = charge;
        }

        public override bool Equals(object obj)
        {
            var filling = obj as SemeFilling;
            return filling != null &&
                   EqualityComparer<Tincture>.Default.Equals(Background, filling.Background) &&
                   EqualityComparer<Charge>.Default.Equals(Charge, filling.Charge);
        }

        public override int GetHashCode()
        {
            var hashCode = -247438890;
            hashCode = hashCode * -1521134295 + EqualityComparer<Tincture>.Default.GetHashCode(Background);
            hashCode = hashCode * -1521134295 + EqualityComparer<Charge>.Default.GetHashCode(Charge);
            return hashCode;
        }
    }
}
