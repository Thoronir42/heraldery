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
        List<FieldDivisionVariantDefinition> FieldDivisionVariants { get; set; }
        List<PositionDefinition> Positions { get; set; }

        public BlazonVocabulary(string blazonDirectory)
        {
            this.Tinctures = LoadList(blazonDirectory + "tinctures.csv", "Tinctures", LoadTinctures);
            this.FieldDivisions = LoadList(blazonDirectory + "field_divisions.csv", "Field Divisions", LoadFieldDivisions);
            this.FieldDivisionVariants = LoadList(blazonDirectory + "field_division_variants.csv", "Field division", LoadFieldDivisionVariants);
            this.Tinctures = LoadList(blazonDirectory + "tinctures.csv", "Tinctures", LoadTinctures);
            this.Positions = LoadList(blazonDirectory + "positions.csv", "Positions", LoadPositions);

            foreach (var def in Positions)
            {
                Console.WriteLine(def.Text + " - " + def.Type.ToString() + "/" + def.Position.ToString());
            }
        }

        public List<Definition> GetAllDefinitions(Boolean sortByLength = false)
        {
            var list = new List<Definition>();
            list.AddRange(this.Tinctures);
            list.AddRange(this.FieldDivisions);
            list.AddRange(this.FieldDivisionVariants);
            list.AddRange(this.Positions);


            if (sortByLength)
            {
                return list.OrderByDescending(o => o.Text.Length).ToList();
            }

            return list;
        }

        private List<T> LoadList<T>(string file, string itemsLabel, Func<string, List<T>> loadFunc)
        {
            Console.Write("Loading " + itemsLabel + "...");
            List<T> list = loadFunc(file);
            Console.WriteLine(" " + list.Count() + " loaded");

            return list;
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

        private List<FieldDivisionDefinition> LoadFieldDivisions(string filename)
        {
            Func<string[], FieldDivisionDefinition> f = new Func<string[], FieldDivisionDefinition>(parts =>
            {
                FieldDivisionType type = (FieldDivisionType)Enum.Parse(typeof(FieldDivisionType), parts[1]);
                return new FieldDivisionDefinition() { Text = parts[0], Type = type };
            });

            return ParseCsvFile(filename, f);

        }

        private List<FieldDivisionVariantDefinition> LoadFieldDivisionVariants(string filename)
        {
            Func<string[], FieldDivisionVariantDefinition> f = new Func<string[], FieldDivisionVariantDefinition>(parts =>
            {
                FieldDivisionVariant type = (FieldDivisionVariant)Enum.Parse(typeof(FieldDivisionVariant), parts[1]);
                return new FieldDivisionVariantDefinition() { Text = parts[0], Variant = type };
            });

            return ParseCsvFile(filename, f);

        }

        private List<PositionDefinition> LoadPositions(string filename)
        {
            Func<string[], PositionDefinition> f = (parts =>
            {
                PositionType type = ParseEnumValue<PositionType>(parts[1]);
                Position position = new Position();
                switch (type)
                {
                    case PositionType.Horizontal:
                        position.Horizontal = ParseEnumValue<HorizontalPosition>(parts[2]);
                        break;
                    case PositionType.Point:
                    case PositionType.Vertical:
                        position.Vertical = ParseEnumValue<VerticalPosition>(parts[2]);
                        break;
                }

                return new PositionDefinition() { Text = parts[0], Type = type, Position = position };
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

        private static T ParseEnumValue<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

    }
}
