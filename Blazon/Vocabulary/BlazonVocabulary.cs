using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Vocabulary.Entries;

namespace Heraldry.Blazon.Vocabulary
{
    class BlazonVocabulary
    {
        List<TinctureDefinition> Tinctures { get; set; } // todo: Todd, hide the setter, god dammit
        List<FieldDivisionDefinition> FieldDivisions { get; set; }

        public BlazonVocabulary(string blazonDirectory)
        {
            Console.Write("Loading Tinctures...");
            this.Tinctures = this.LoadTinctures(blazonDirectory + "tinctures.csv");
            Console.WriteLine(" " + this.Tinctures.Count() + " loaded");

            Console.Write("Loading Field Divisions...");
            this.FieldDivisions = this.LoadFieldDIvisions(blazonDirectory + "field_divisions.csv");
            Console.WriteLine(" " + this.FieldDivisions.Count() + " loaded");
        }

        private List<TinctureDefinition> LoadTinctures(string filename)
        {
            Func<string[], TinctureDefinition> f = new Func<string[], TinctureDefinition>(parts =>
            {
                TinctureType type = (TinctureType)Enum.Parse(typeof(TinctureType), parts[1]);
                return new TinctureDefinition() { Text = parts[0], Type = type };
            });

            return ParseCsvFile(filename, f);
        }

        private List<FieldDivisionDefinition> LoadFieldDIvisions(string filename)
        {
            Func<string[], FieldDivisionDefinition> f = new Func<string[], FieldDivisionDefinition>(parts =>
            {
                FieldDivisionType type = (FieldDivisionType)Enum.Parse(typeof(FieldDivisionType), parts[1]);
                return new FieldDivisionDefinition() { Text = parts[0], Type = type };
            });

            return ParseCsvFile(filename, f);

        }

        private static List<T> ParseCsvFile<T>(string filename, Func<string[], T> parseLineFunction)
        {
            int lineNumber = 0;

            return File.ReadLines(filename)
                .Select(line => { return line.Split(';'); })
                .Select(parts =>
                {
                    lineNumber++;
                    try
                    {
                        return parseLineFunction(parts);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(String.Format("Could not parse line {0} of file {1}: {2}", lineNumber, filename, e.Message), e);
                    }

                })
                .ToList();
        }
    }
}
