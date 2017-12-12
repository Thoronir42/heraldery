using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis.Numbers
{
    public abstract class NumberParser
    {
        public abstract Token FindNumber(String text);
    }
}
