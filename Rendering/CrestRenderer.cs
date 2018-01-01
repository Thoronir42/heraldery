using Heraldry.App;
using Heraldry.Blazon;
using Heraldry.Blazon.Structure;
using Heraldry.Rendering.Svg;
using Heraldry.Rendering.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Heraldry.Rendering
{
    public abstract class CrestRenderer : ParseStep<BlazonInstance, Boolean>
    {
        public Stream PrintStream { get; set; }

        public override bool Execute(BlazonInstance input)
        {
            return this.Render(input, PrintStream);
        }

        public abstract Boolean Render(BlazonInstance instance, Stream writer);
    }
}
