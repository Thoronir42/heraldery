using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    class FillingPrinter : BasePrinter<Filling>
    {
        public FillingPrinter(RootPrinter root) : base(root)
        {

        }

        public override void P(Filling item)
        {
            if(item.Layout.FillingLayoutType == Blazon.Elements.FillingLayoutType.Solid)
            {
                Print.Writer.Write(item.Tinctures[0].Text);
            }
        }
    }
}
