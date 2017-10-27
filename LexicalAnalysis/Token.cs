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
        public object Value { get; set; } // todo: generify Value type

        public int Position { get; set; }
        public int OriginalText { get; set; }
    }
}
