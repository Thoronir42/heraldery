using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Structure.Fillings
{

    // todo: replace FurFilling by FurTincture
    public class FurFilling : Filling
    {
        public static char PATTERN_SEPARATOR = ':',
            COLOR_SEPARATOR = ',';


        public String Pattern { get; }
        public Tincture PrimaryColor { get; set; }
        public Tincture SecondaryColor { get; set; }

        public FurFilling(String definition)
        {
            String[] parts = definition.Split(PATTERN_SEPARATOR);
            this.Pattern = parts[0];

            String[] colors = parts[1].Split(COLOR_SEPARATOR);
            PrimaryColor = new Tincture { TinctureType = Elements.TinctureType.Html, Value = colors[0] };
            SecondaryColor = new Tincture { TinctureType = Elements.TinctureType.Html, Value = colors[1] };
        }
    }
}
