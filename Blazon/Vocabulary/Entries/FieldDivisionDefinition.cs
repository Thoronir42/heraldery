﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class FieldDivisionDefinition : Definition
    {
        public FieldDivisionType Type { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.FieldDivision;
        }
    }
}
