using Heraldry.Blazon;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering
{
    class CrestRenderer
    {
        private SvgLoader Loader { get; set; }

        public CrestRenderer() : this(new SvgLoader(400, 400)) { /* default constructor */ }

        public CrestRenderer(SvgLoader loader) {
            this.Loader = loader;
        }

        public void Render(BlazonInstance blazon) {
            // todo: return image
        }
    }
}
