using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    public abstract class ParseStep<InType, OutType>
    {
        public abstract OutType Execute(InType input);
    }
}
