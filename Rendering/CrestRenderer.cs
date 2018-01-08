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
    public abstract class CrestRenderer : ParseProcess.Step<BlazonInstance, ParseProcess.Result>
    {
        protected Stream PrintStream { get; }
        public bool CloseWhenDone { get; set; } = false;

        public CrestRenderer(Stream printStream)
        {
            this.PrintStream = printStream;
        }

        public override ParseProcess.Result Execute(BlazonInstance input)
        {
            var result = this.Render(input, PrintStream);

            if(CloseWhenDone)
            {
                PrintStream.Close();
            }
            return result;
        }

        public abstract ParseProcess.Result Render(BlazonInstance instance, Stream stream);
    }
}
