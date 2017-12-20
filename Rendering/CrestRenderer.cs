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
    abstract class CrestRenderer : ParseStep<BlazonInstance, Boolean>
    {
        public Stream PrintStream { get; set; }

        public override bool Execute(BlazonInstance input)
        {
            return this.Render(input, PrintStream);
        }

        public abstract Boolean Render(BlazonInstance instance, Stream writer);

        public static CrestRenderer GetByType(RenderType type)
        {
            switch(type)
            {
                case RenderType.Svg: return new SvgRenderer();
                case RenderType.Text: return new TextRenderer();
            }

            throw new ArgumentException("Render type " + type.ToString() + " is not supported yet");
        }
    }
}
