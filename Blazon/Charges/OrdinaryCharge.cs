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
/// 
/// TODO: Fix field not being considered in comparisons, plzz
/// </summary>
namespace Heraldry.Blazon.Charges
{
    public class OrdinaryCharge : InnerFieldCharge
    {
        /// <summary>
        /// Particular ordinary type.
        /// </summary>
        public Ordinary OrdinaryType { get; set; }

        /// <summary>
        /// Size of the ordinary.
        /// </summary>
        public OrdinarySize OrdinarySize { get; set; }

        public OrdinaryCharge() : base(ChargeType.Ordinary)
        {   
        }

        public override bool Equals(object obj)
        {
            var charge = obj as OrdinaryCharge;
            return charge != null &&
                   base.Equals(obj) &&
                   OrdinaryType == charge.OrdinaryType &&
                   OrdinarySize == charge.OrdinarySize &&
                   EqualityComparer<Filling>.Default.Equals(Filling, charge.Filling);
        }

        public override int GetHashCode()
        {
            var hashCode = -2029137507;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + OrdinaryType.GetHashCode();
            hashCode = hashCode * -1521134295 + OrdinarySize.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Filling>.Default.GetHashCode(Filling);
            return hashCode;
        }
    }
}
