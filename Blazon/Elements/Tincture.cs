using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Elements
{
    /// <summary>
    /// Tincture is a color or a pattern
    /// </summary>
    public class Tincture
    {
        public string Name { get; set; }
        public TinctureType Type { get; set; }
        public string Parameters { get; set; } // todo: possibly use better structure for parameters

        public override string ToString()
        {
            return String.Format("Tincture {0}:{1} [{2}]", this.Name, this.Type, this.Parameters);
        }
    }

    public enum TinctureType
    {
        Colour, Metal, Fur
    }
}
