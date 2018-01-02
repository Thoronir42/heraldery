using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Heraldry.Blazon.Vocabulary
{
    public class VocabularyLoader
    {
        public static BlazonVocabulary LoadFromDirectory(string blazonDirectory, string numbers = null, bool verbose = true)
        {
            var loader = new VocabularyLoader(blazonDirectory)
            {
                Verbose = verbose
            };

            if (numbers != null)
            {
                loader.Numbers = numbers;
            }

            return loader.Load();
        }

        public string BlazonDirectory { get; set; }
        public string Numbers { get; set; } = "english";
        public bool Verbose { get; set; } = true;

        private VocabularyLoader(string blazonDirectory)
        {
            this.BlazonDirectory = blazonDirectory;
        }

        public BlazonVocabulary Load()
        {
            return new BlazonVocabulary
            {
                Tinctures = LoadList(BlazonDirectory + "tinctures.csv", "Tinctures", LoadTinctures),
                FieldDivisions = LoadList(BlazonDirectory + "field_divisions.csv", "Field Divisions", LoadFieldDivisions),
                FieldDivisionLines = LoadList(BlazonDirectory + "field_division_lines.csv", "Field division lines", LoadFieldDivisionLines),
                FieldVariations = LoadList(BlazonDirectory + "field_variations.csv", "Field variations", LoadFieldVariations),
                Positions = LoadList(BlazonDirectory + "positions.csv", "Positions", LoadPositions),
                KeyWords = LoadList(BlazonDirectory + "keywords.csv", "KeyWords", LoadKeyWords),
                Numbers = LoadList(BlazonDirectory + "numbers.csv", "Numbers", LoadNumbers),
                Ordinaries = LoadList(BlazonDirectory + "ordinaries.csv", "Ordinaries", LoadOrdinaries),
                Subordinaries = LoadList(BlazonDirectory + "subordinaries.csv", "Subordinaries", LoadSubordinaries),
                ShapeCharges = LoadList(BlazonDirectory + "shapes.csv", "Shape charges", LoadShapeCharges),
                ShapeTypes = LoadList(BlazonDirectory + "shape_types.csv", "Shape types", LoadShapeTypes),

                NumberVocabulary = CreateNumberVocabulary(Numbers),
            };
        }

        private static NumberVocabulary CreateNumberVocabulary(string id)
        {
            switch (id.ToLower())
            {
                case "english":
                    return new EnglishNumberVocabulary();
                case "czech":
                    return new CzechNumberVocabulary();
            }

            throw new NotSupportedException("Number vocabulary " + id + " is not supported");
        }

        private List<T> LoadList<T>(string file, string itemsLabel, Func<string, List<T>> loadFunc)
        {
            if (this.Verbose)
            {
                Console.Write("Loading " + itemsLabel + "...");
            }
            List<T> list = loadFunc(file);
            if(this.Verbose)
            {
                Console.WriteLine(" " + list.Count() + " loaded");
            }

            return list;
        }

        private static List<TinctureDefinition> LoadTinctures(string filename)
        {
            Func<string[], TinctureDefinition> f = new Func<string[], TinctureDefinition>(parts =>
            {
                TinctureType type = ParseEnumValue<TinctureType>(parts[1]);

                return new TinctureDefinition()
                {
                    Text = parts[0],
                    Tincture = new Tincture { TinctureType = type, Value = parts[2] },
                };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionDefinition> LoadFieldDivisions(string filename)
        {
            Func<string[], FieldDivisionDefinition> f = new Func<string[], FieldDivisionDefinition>(parts =>
            {
                return new FieldDivisionDefinition()
                {
                    Text = parts[0],
                    Type = ParseEnumValue<FieldDivisionType>(parts[1])
                };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionLineDefinition> LoadFieldDivisionLines(string filename)
        {
            Func<string[], FieldDivisionLineDefinition> f = new Func<string[], FieldDivisionLineDefinition>(parts =>
            {
                return new FieldDivisionLineDefinition()
                {
                    Text = parts[0],
                    Line = ParseEnumValue<FieldDivisionLine>(parts[1])
                };
            });

            return ParseCsvFile(filename, f);

        }

        private static List<FieldVariationDefinition> LoadFieldVariations(string filename)
        {
            Func<string[], FieldVariationDefinition> f = new Func<string[], FieldVariationDefinition>(parts =>
            {
                return new FieldVariationDefinition()
                {
                    Text = parts[0],
                    VariationType = ParseEnumValue<FieldVariationType>(parts[1]),
                };
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

                return new NumberDefinition
                {
                    Text = parts[0],
                    Number = new Number { Type = type, Value = value }
                };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<OrdinaryDefinition> LoadOrdinaries(string filename)
        {
            Func<string[], OrdinaryDefinition> f = new Func<string[], OrdinaryDefinition>(parts =>
            {
                Ordinary type = ParseEnumValue<Ordinary>(parts[1]);
                OrdinarySize size = ParseEnumValue<OrdinarySize>(parts[2]);

                return new OrdinaryDefinition() { Text = parts[0], Type = type, Size = size };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<SubordinaryDefinition> LoadSubordinaries(string filename)
        {
            Func<string[], SubordinaryDefinition> f = new Func<string[], SubordinaryDefinition>(parts =>
            {
                Subordinary type = ParseEnumValue<Subordinary>(parts[1]);

                return new SubordinaryDefinition() { Text = parts[0], Type = type };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<ChargeDefinition> LoadShapeCharges(string filename)
        {
            Func<string[], ChargeDefinition> f = new Func<string[], ChargeDefinition>(parts =>
            {
                ShapeCharge charge = new ShapeCharge();

                // Shape-Hole [shape minus hole]
                string[] shape = parts[1].Split('-');
                charge.Shape = ParseEnumValue<Shape>(shape[0]);

                if (shape.Length > 1)
                {
                    charge.Hole = ParseEnumValue<Shape>(shape[1]);
                }

                if (parts.Length > 2)
                {
                    charge.ImplicitFilling = parts[2];
                }

                return new ChargeDefinition() { Text = parts[0], Charge = charge };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<ShapeTypeDefinition> LoadShapeTypes(string filename)
        {
            Func<string[], ShapeTypeDefinition> f = new Func<string[], ShapeTypeDefinition>(parts =>
            {
                ShapeType type = ParseEnumValue<ShapeType>(parts[1]);

                return new ShapeTypeDefinition() { Text = parts[0], ShapeType = type };
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
