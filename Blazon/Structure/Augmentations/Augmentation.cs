using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Augmentations
{
    public abstract class Augmentation
    {
        public Charge Charge { get; }

        public Augmentation(Charge charge)
        {
            Charge = charge;
        }
    }
}
