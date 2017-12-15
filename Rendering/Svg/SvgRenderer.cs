using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Svg;
using Heraldry.Blazon.Structure;

namespace Heraldry.Rendering.Svg
{
    class SvgRenderer : CrestRenderer
    {
        private SvgLoader Loader { get; set; }

        public SvgRenderer() : this(new SvgLoader(100, 100)) { /* default constructor */ }

        public SvgRenderer(SvgLoader loader)
        {
            this.Loader = loader;
        }

        public override Boolean Render(BlazonInstance blazon, String outputPath)
        {
            SvgDocument tmp = Loader.GetSvgFromFile("iconmonstr-shield-1.svg");
            tmp.Children[0].Fill = new SvgColourServer(Color.Red);

            var bmp = GetBitmapFromSvg(tmp);

            return false; // todo: dump this to file
        }

        private Bitmap GetBitmapFromSvg(SvgDocument input)
        {
            Bitmap retval = input.Draw(100, 100);
            return retval;
        }
    }
}
