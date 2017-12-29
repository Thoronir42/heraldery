﻿using Heraldry.Blazon.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class ChargeDefinition : Definition
    {
        public Charge Charge { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Charge;
        }

        public override object GetSubtype()
        {
            if(Charge == null)
            {
                return ChargeType.Generic;
            }

            return Charge.Type;
        }
    }
}
