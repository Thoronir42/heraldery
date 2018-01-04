using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Charges
{
    public abstract class Charge
    {
        public ChargeType Type { get; }
        public virtual Filling Filling { get; set; }

        protected Charge(ChargeType type)
        {
            this.Type = type;
        }
    }

    public enum ChargeType
    {
        Generic,
        SimpleShape,
        Ordinary,
        Subordinary,
        Cross,
    }
}
