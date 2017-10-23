using Heraldry.LexicalAnalysis.Tokens;
using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon
{
    class BlazonDefinition
    {
        List<TinctureToken> Tinctures { get; set; } // todo: Todd, hide the setter, god dammit
        List<FieldDivisionToken> FieldDivisions { get; set; }

        public BlazonDefinition(string blazonDirectory)
        {
            Console.Write("Loading Tinctures...");
            this.Tinctures = this.LoadTinctures(blazonDirectory + "tinctures.csv");
            Console.WriteLine(" " + this.Tinctures.Count() + " loaded");

            Console.Write("Loading Field Divisions...");
            this.FieldDivisions = this.LoadFieldDIvisions(blazonDirectory + "field_divisions.csv");
            Console.WriteLine(" " + this.FieldDivisions.Count() + " loaded");
        }

        private List<TinctureToken> LoadTinctures(string filename)
        {
            int lineNumber = 0; // todo: Todd, wtf this is not how you count lines. Probably
            return File.ReadLines(filename)
                .Select(line => { return line.Split(';'); })
                .Select(parts =>
                {
                    try
                    {
                        lineNumber++;
                        TinctureType type = (TinctureType)Enum.Parse(typeof(TinctureType), parts[1]);
                        return new TinctureToken() { Name = parts[0], Type = type };
                    }
                    catch
                    {
                        throw new ArgumentException("TinctureType on line " + lineNumber + " (" + parts[1] + ") is not recognized");
                    }

                })
                .ToList();
        }

        private List<FieldDivisionToken> LoadFieldDIvisions(string filename)
        {
            return File.ReadLines(filename)
                .Select(line => { return line.Split(';'); })
                .Select(parts =>
                {
                    FieldDivisionType type = (FieldDivisionType)Enum.Parse(typeof(FieldDivisionType), parts[1]);
                    return new FieldDivisionToken() { Name = parts[0], Type = type };
                })
                .ToList();

        }
    }
}
