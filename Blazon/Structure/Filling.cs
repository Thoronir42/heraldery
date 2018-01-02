using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class Filling
    {
        public FillingLayout Layout { get; set; }

        public Tincture[] Tinctures { get; set; } = new Tincture[0];

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Filling() {  }

        /// <summary>
        /// Construct filling with solid tincture.
        /// 
        /// </summary>
        /// <param name="tincture">Definition of the tincture.</param>
        public Filling(Tincture tincture)
        {
            Layout = FillingLayout.Solid();
            Tinctures = new Tincture[] { tincture };
        }

        /// <summary>
        /// Fills tincture definition array from the list of fillings. Only fillings with solid tinctures are accepted.
        /// </summary>
        /// <param name="fillings">List of fillings.</param>
        public void AddTinctureDefinitions(List<Filling> fillings)
        {
            List<Tincture> tmpDefs = new List<Tincture>();
            foreach(Filling f in fillings)
            {
                if(f.Layout.FillingLayoutType == FillingLayoutType.Solid)
                {
                    tmpDefs.Add(f.Tinctures[0]);
                }
            }

            Tinctures = tmpDefs.ToArray();
        }
    }
}
