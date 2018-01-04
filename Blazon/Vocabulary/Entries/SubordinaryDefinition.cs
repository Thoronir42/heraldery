﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class SubordinaryDefinition : Definition
    {
        public Subordinary Type { get; }

        public SubordinaryDefinition(Subordinary subordinary)
        {
            this.Type = subordinary;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Subordinary;
        }

        public override object GetSubtype()
        {
            return Type;
        }
    }
}
