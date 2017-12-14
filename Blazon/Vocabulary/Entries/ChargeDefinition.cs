using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class ChargeDefinition : Definition
    {
        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Charge;
        }
    }
}
