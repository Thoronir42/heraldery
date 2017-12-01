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
        public FillingLayout Layout { get; set; }

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
            Layout = FillingLayout.Solid();
            Tinctures = new TinctureDefinition[] { tinctureDefinition };
        }

        /// <summary>
        /// Fills tincture definition array from the list of fillings. Only fillings with solid tinctures are accepted.
        /// </summary>
        /// <param name="fillings">List of fillings.</param>
        public void AddTinctureDefinitions(List<Filling> fillings)
        {
            List<TinctureDefinition> tmpDefs = new List<TinctureDefinition>();
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
