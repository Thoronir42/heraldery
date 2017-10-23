using Heraldery.Blazon.IO;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldery.Blazon
{
    class BlazonDefinition
    {
        List<Tincture> Tinctures { get; set; } // todo: Todd, hide the setter, god dammit
        List<FieldDivision> FieldDivisions { get; set; }

        public BlazonDefinition(string blazonDirectory)
        {
            Console.Write("Loading Tinctures...");
            this.Tinctures = (new TinctureProvider(blazonDirectory + "tinctures.csv")).LoadItems();
            Console.WriteLine(" " + this.Tinctures.Count() + " loaded");

            Console.Write("Loading Field Divisions...");
            this.FieldDivisions = (new FieldDivisionTypeProvider(blazonDirectory + "field_divisions.csv")).LoadItems();
            Console.WriteLine(" " + this.FieldDivisions.Count() + " loaded");
            
        }
    }
}
