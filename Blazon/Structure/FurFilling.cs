using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure
{
    public class FurFilling : Filling
    {
        public static char PATTERN_SEPARATOR = ':',
            COLOR_SEPARATOR = ',';


        public String Pattern { get; }
        public TinctureDefinition PrimaryColor { get; set; }
        public TinctureDefinition SecondaryColor { get; set; }

        public FurFilling(TinctureDefinition def)
        {
            String[] parts = def.Text.Split(PATTERN_SEPARATOR);
            this.Pattern = parts[0];

            String[] colors = parts[1].Split(COLOR_SEPARATOR);
            PrimaryColor = new TinctureDefinition { TinctureType = Elements.TinctureType.Html, Value = colors[0] };
            SecondaryColor = new TinctureDefinition { TinctureType = Elements.TinctureType.Html, Value = colors[1] };
        }
    }
}
