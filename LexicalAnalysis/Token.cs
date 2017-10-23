using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    class Token
    {
        public TokenType Type { get; set; }
        public Object Value { get; set; }

        public int Position { get; set; }
    }
}
