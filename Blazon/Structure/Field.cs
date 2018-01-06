using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Structure.Augmentations;

namespace Heraldry.Blazon.Structure
{
    public abstract class Field
    {
        // add more possibilities to augment field with
        public List<Augmentation> Augmentations { get; } = new List<Augmentation>();

        public string Comment { get; set; }
    }
}
