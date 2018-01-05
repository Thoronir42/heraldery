using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Augmentations
{
    public class FieldAugmentation : Augmentation
    {
        public FieldAugType Type { get; }

        public FieldAugmentation(Charge charge, FieldAugType type = FieldAugType.Overall) : base(charge)
        {
            Type = type;
        }
    }

    public enum FieldAugType
    {
        /// <summary> Over the field </summary>
        Overall,
    }
}
