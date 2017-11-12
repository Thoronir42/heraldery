using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    class Token
    {
        public Definition Definition { get; set; }
        public DefinitionType Type
        {
            get { return Definition.GetTokenType(); }
        }

        public object Value { get; set; } // todo: generify Value type

        public int Position { get; set; }
    }
}
