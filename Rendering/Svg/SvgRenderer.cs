using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Svg;
using Heraldry.Blazon.Structure;
using System.IO;
using System.Drawing.Imaging;
using Heraldry.App;

namespace Heraldry.Rendering.Svg
{
    class SvgRenderer : CrestRenderer
    {
        private SvgLoader Loader { get; }

        public SvgRenderer(Stream stream) : this(new SvgLoader(100, 100), stream) { /* default constructor */ }

        public SvgRenderer(SvgLoader loader, Stream stream) : base(stream)
        {
            this.Loader = loader;
        }

        public override ParseProcess.Result Render(BlazonInstance blazon, Stream stream)
        {
            SvgDocument tmp = Loader.GetSvgFromFile("iconmonstr-shield-1.svg");
            tmp.Children[0].Fill = new SvgColourServer(Color.Red);

            var bmp = GetBitmapFromSvg(tmp);

            bmp.Save(stream, ImageFormat.Png);

            return new ParseProcess.Result("To be implemented");
        }

        private Bitmap GetBitmapFromSvg(SvgDocument input)
        {
            Bitmap retval = input.Draw(100, 100);
            return retval;
        }
    }
}
