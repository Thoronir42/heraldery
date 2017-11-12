using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    abstract class Definition
    {
        public string Text { get; set; }

        abstract public DefinitionType GetTokenType();
        
    }
}
