using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    class BlazonInstance
    {
        public CoatOfArms CoatOfArms { get; set; }
        public List<Charge> Supporters { get; set; }
    }
}
