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
        protected Stream PrintStream { get; }

        public CrestRenderer(Stream printStream)
        {
            this.PrintStream = printStream;
        }

        public override bool Execute(BlazonInstance input)
        {
            return this.Render(input, PrintStream);
        }

        public abstract Boolean Render(BlazonInstance instance, Stream writer);
    }
}
