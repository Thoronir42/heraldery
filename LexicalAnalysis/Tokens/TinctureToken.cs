using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis.Tokens
{
    /// <summary>
    /// Tincture is a color or a pattern
    /// </summary>
    class TinctureToken : TokenSpec
    {   
        public TinctureType Type { get; set; }
        

        public override string ToString()
        {
            return String.Format("Tincture {0}:{1} [{2}]", this.Name, this.Type);
        }
    }
}
