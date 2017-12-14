using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class CoatOfArms
    {
        /// <summary>
        /// Type of the shield.
        /// </summary>
        public EscutcheonShape Escutcheon { get; set; } = EscutcheonShape.ModernFrench;

        /// <summary>
        /// Contet of the shield (this is why we are doing this).
        /// </summary>
        public Field Content { get; set; }


        public Charge Crest { get; set; }
        
    }
}
