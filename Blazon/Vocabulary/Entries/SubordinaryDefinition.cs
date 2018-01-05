using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class SubordinaryDefinition : Definition<Subordinary>
    {
        public Subordinary Type { get; }

        public SubordinaryDefinition(Subordinary subordinary) : base(DefinitionType.Subordinary, subordinary)
        {
            this.Type = subordinary;
        }
    }
}
