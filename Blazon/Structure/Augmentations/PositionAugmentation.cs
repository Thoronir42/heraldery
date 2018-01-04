using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Augmentations
{
    public class PositionAugmentation : Augmentation
    {
        public Position Position { get; }

        public PositionAugmentation(Charge charge, Position position) : base(charge)
        {
            Position = position;
        }
        
    }
}
