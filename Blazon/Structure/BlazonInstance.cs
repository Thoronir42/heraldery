using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    /// <summary>
    /// Class which describes whole shield of arms with supporters, crest, motto, ...
    /// </summary>
    public class BlazonInstance
    {
        /// <summary>
        /// Main part of amrs.
        /// </summary>
        public CoatOfArms CoatOfArms { get; set; }
        public List<Charge> Supporters { get; set; }
    }
}
