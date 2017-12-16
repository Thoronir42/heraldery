﻿using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    /// <summary>
    /// Base class for divided fields.
    /// </summary>
    public abstract class DividedField : Field
    {
        // field is virtual so that setters can be overrided in child classes
        public virtual FieldDivisionType? Division { get; set; } = null;
        public FieldDivisionLine Line { get; set; } = FieldDivisionLine.Straight;

        public Field[] Subfields { get; set; } = null;
    }
}
