using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class ContentField : Field
    {
        public Filling Background { get; set; }
        public Charge Charge { get; set; }

        public ContentField(Filling background)
        {
            Background = background;
        }
    }
}
