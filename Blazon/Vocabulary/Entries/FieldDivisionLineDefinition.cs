﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class FieldDivisionLineDefinition : Definition
    {
        public FieldDivisionLine Line { get; }

        public FieldDivisionLineDefinition(FieldDivisionLine line)
        {
            this.Line = line;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.FieldDivisionLine;
        }

        public override object GetSubtype()
        {
            return Line;
        }
    }
}
