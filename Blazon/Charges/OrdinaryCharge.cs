using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class represents ordinary charge - cross (not to be mistaken with cross as object), chief, pale
/// bend, ...
/// </summary>
namespace Heraldry.Blazon.Charges
{
    public class OrdinaryCharge : Charge
    {
        /// <summary>
        /// Particular ordinary type.
        /// </summary>
        public Ordinary OrdinaryType { get; set; }

        /// <summary>
        /// Size of the ordinary.
        /// </summary>
        public OrdinarySize OrdinarySize { get; set; }

        /// <summary>
        /// Filling (color) of the ordinary. Field should be used instead of Filling but lets keep
        /// this for the sake of simplicity.
        /// </summary>
        public Filling Filling { get; set; }
    }
}
