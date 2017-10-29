using Heraldry.Blazon;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Svg;

namespace Heraldry.Rendering
{
    class CrestRenderer
    {
        private SvgLoader Loader { get; set; }

        public CrestRenderer() : this(new SvgLoader(100, 100)) { /* default constructor */ }

        public CrestRenderer(SvgLoader loader) {
            this.Loader = loader;
        }

        public Bitmap Render(BlazonInstance blazon) {
            SvgDocument tmp = Loader.GetSvgFromFile("iconmonstr-shield-1.svg");
            tmp.Children[0].Fill = new SvgColourServer(Color.Red);
            return GetBitmapFromSvg(tmp);
        }

        private Bitmap GetBitmapFromSvg(SvgDocument input) {
            Bitmap retval = input.Draw(100, 100);
            return retval;
        }
    }
}
