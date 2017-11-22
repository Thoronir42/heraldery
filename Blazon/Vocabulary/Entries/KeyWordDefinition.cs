﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class KeyWordDefinition : Definition
    {
        public KeyWord KeyWord { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.KeyWord;
        }

        public override object GetSubtype()
        {
            return this.KeyWord;
        }
    }
}
