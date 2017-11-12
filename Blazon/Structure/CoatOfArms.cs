﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    class CoatOfArms
    {
        public EscutcheonShape Escutcheon { get; set; } = EscutcheonShape.ModernFrench;

        public Field Content { get; set; }

        public Charge Crest { get; set; }
        
    }
}