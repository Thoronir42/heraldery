using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class Filling
    {
        public TinctureLayout Layout { get; set; }

        public TinctureDefinition[] Tinctures { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Filling() { }

        /// <summary>
        /// Construct filling with solid tincture.
        /// 
        /// </summary>
        /// <param name="tinctureDefinition">Definition of the tincture.</param>
        public Filling(TinctureDefinition tinctureDefinition)
        {
            Layout = TinctureLayout.Solid;
            Tinctures = new TinctureDefinition[] { tinctureDefinition };
        }
    }
}
