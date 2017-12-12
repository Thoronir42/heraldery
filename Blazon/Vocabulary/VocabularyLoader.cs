using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary
{
    public class VocabularyLoader
    {

        public static BlazonVocabulary LoadFromDirectory(string blazonDirectory)
        {

            return new BlazonVocabulary
            {
                Tinctures = LoadList(blazonDirectory + "tinctures.csv", "Tinctures", LoadTinctures),
                FieldDivisions = LoadList(blazonDirectory + "field_divisions.csv", "Field Divisions", LoadFieldDivisions),
                FieldDivisionLines = LoadList(blazonDirectory + "field_division_lines.csv", "Field division lines", LoadFieldDivisionLines),
                Positions = LoadList(blazonDirectory + "positions.csv", "Positions", LoadPositions),
                KeyWords = LoadList(blazonDirectory + "keywords.csv", "KeyWords", LoadKeyWords),
                Numbers = LoadList(blazonDirectory + "numbers.csv", "Numbers", LoadNumbers)
            };
        }



        private static List<T> LoadList<T>(string file, string itemsLabel, Func<string, List<T>> loadFunc)
        {
            Console.Write("Loading " + itemsLabel + "...");
            List<T> list = loadFunc(file);
            Console.WriteLine(" " + list.Count() + " loaded");

            return list;
        }

        private static List<TinctureDefinition> LoadTinctures(string filename)
        {
            Func<string[], TinctureDefinition> f = new Func<string[], TinctureDefinition>(parts =>
            {
                TinctureType type = (TinctureType)Enum.Parse(typeof(TinctureType), parts[1]);
                return new TinctureDefinition() { Text = parts[0], TinctureType = type, Value = parts[2] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionDefinition> LoadFieldDivisions(string filename)
        {
            Func<string[], FieldDivisionDefinition> f = new Func<string[], FieldDivisionDefinition>(parts =>
            {
                FieldDivisionType type = (FieldDivisionType)Enum.Parse(typeof(FieldDivisionType), parts[1]);
                return new FieldDivisionDefinition() { Text = parts[0], Type = type };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionLineDefinition> LoadFieldDivisionLines(string filename)
        {
            Func<string[], FieldDivisionLineDefinition> f = new Func<string[], FieldDivisionLineDefinition>(parts =>
            {
                FieldDivisionLine type = (FieldDivisionLine)Enum.Parse(typeof(FieldDivisionLine), parts[1]);
                return new FieldDivisionLineDefinition() { Text = parts[0], Line = type };
            });

            return ParseCsvFile(filename, f);

        }

        private static List<PositionDefinition> LoadPositions(string filename)
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

        private static List<KeyWordDefinition> LoadKeyWords(string filename)
        {
            Func<string[], KeyWordDefinition> f = new Func<string[], KeyWordDefinition>(parts =>
            {
                KeyWord word = ParseEnumValue<KeyWord>(parts[1]);
                return new KeyWordDefinition() { Text = parts[0], KeyWord = word };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<NumberDefinition> LoadNumbers(string filename)
        {
            Func<string[], NumberDefinition> f = new Func<string[], NumberDefinition>(parts =>
            {
                NumberType type = ParseEnumValue<NumberType>(parts[1]);
                int value = int.Parse(parts[2]);
                return new NumberDefinition() { Text = parts[0], Type = type, Value = value };
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
