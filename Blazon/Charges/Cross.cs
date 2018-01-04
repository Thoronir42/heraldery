using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public class Cross : Charge
    {
        public CrossType CrossType { get; set; }

        public Cross() : base(ChargeType.Cross)
        {   
        }

        public override bool Equals(object obj)
        {
            var cross = obj as Cross;
            return cross != null &&
                   base.Equals(obj) &&
                   CrossType == cross.CrossType;
        }

        public override int GetHashCode()
        {
            var hashCode = -2086658045;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + CrossType.GetHashCode();
            return hashCode;
        }
    }

    public enum CrossType
    {
        GreekCross,
        CrossMoline,
        CrossPatonce,
        CrossFlory,
        CrossPommee,
        CrossCrosslet,
        CrossPotent,
        Saltire,
        CrossVoided,
        CrossFourchee,
        CrossPattee,
        MalteseCross,
        CrossBottony,
    }
}
