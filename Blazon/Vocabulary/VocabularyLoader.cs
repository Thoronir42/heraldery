using Heraldry.App;
using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Entries.ChargeProperties;
using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Heraldry.Blazon.Vocabulary
{
    public class VocabularyLoader
    {
        private static readonly char VALUE_SEPARATOR = ';';

        public static readonly char FUR_PATTERN_SEPARATOR = ':',
            FUR_COLOR_SEPARATOR = ',';

        private int totalDefinitions = 0;
        private int definitionTypes = 0;

        public string BlazonDirectory { get; set; }
        public string Numbers { get; set; } = "english";

        public PrintSettings PrintSettings { get; set; } = new PrintSettings();

        public VocabularyLoader(string blazonDirectory, string numbers = "english")
        {
            this.BlazonDirectory = blazonDirectory;
            this.Numbers = numbers;
        }

        public BlazonVocabulary Load()
        {
            var vocabulary = new BlazonVocabulary
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
                Shapes = LoadList(BlazonDirectory + "shapes.csv", "Shapes", LoadShapes),
                ShapeTypes = LoadList(BlazonDirectory + "shape_types.csv", "Shape types", LoadShapeTypes),
                ChargeProperties = LoadChargeProperties(BlazonDirectory),

                NumberVocabulary = CreateNumberVocabulary(Numbers),
            };

            if(PrintSettings.PrintVocabularyLoadProgress)
            {
                Console.WriteLine();
                Console.WriteLine(String.Format("{0,-38}{1} in {2} types", 
                    "Total items loaded ...", totalDefinitions, definitionTypes));
            }

            return vocabulary;
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
            if (PrintSettings.PrintVocabularyLoadProgress)
            {
                Console.Write(String.Format("Loading {0,-30}", itemsLabel + " ..."));
            }

            List<T> list = loadFunc(file);
            totalDefinitions += list.Count();
            definitionTypes++;

            if (PrintSettings.PrintVocabularyLoadProgress)
            {
                Console.WriteLine(String.Format(" {0,2} loaded", list.Count()));
            }

            return list;
        }
        private List<T> LoadCsvList<T>(string file, string itemsLabel, Func<string[], T> parseFunction)
        {
            return LoadList(file, itemsLabel, filename => ParseCsvFile(filename, parseFunction));
        }

        private static List<TinctureDefinition> LoadTinctures(string filename)
        {
            Func<string[], TinctureDefinition> f = new Func<string[], TinctureDefinition>(parts =>
            {
                TinctureType type = ParseEnumValue<TinctureType>(parts[1]);
                Tincture tincture;
                if (type == TinctureType.Fur)
                {
                    String[] valueParts = parts[2].Split(FUR_PATTERN_SEPARATOR);

                    Tincture[] tinctures = valueParts[1].Split(FUR_COLOR_SEPARATOR)
                        .Select(s => new Tincture(TinctureType.Html, s)).ToArray();

                    tincture = new FurTincture(valueParts[0], tinctures)
                    {
                        Value = parts[2]
                    };
                }
                else
                {
                    tincture = new Tincture(type, parts[2]);
                }

                return new TinctureDefinition(tincture)
                {
                    Text = parts[0],
                };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionDefinition> LoadFieldDivisions(string filename)
        {
            Func<string[], FieldDivisionDefinition> f = new Func<string[], FieldDivisionDefinition>(parts =>
            {
                return new FieldDivisionDefinition(ParseEnumValue<FieldDivisionType>(parts[1]))
                {
                    Text = parts[0],
                };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<FieldDivisionLineDefinition> LoadFieldDivisionLines(string filename)
        {
            Func<string[], FieldDivisionLineDefinition> f = new Func<string[], FieldDivisionLineDefinition>(parts =>
            {
                return new FieldDivisionLineDefinition(ParseEnumValue<FieldDivisionLine>(parts[1]))
                {
                    Text = parts[0],
                };
            });

            return ParseCsvFile(filename, f);

        }

        private static List<FieldVariationDefinition> LoadFieldVariations(string filename)
        {
            Func<string[], FieldVariationDefinition> f = new Func<string[], FieldVariationDefinition>(parts =>
            {
                return new FieldVariationDefinition(ParseEnumValue<FieldVariationType>(parts[1]))
                {
                    Text = parts[0],
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

                return new PositionDefinition(position, type) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<KeyWordDefinition> LoadKeyWords(string filename)
        {
            Func<string[], KeyWordDefinition> f = new Func<string[], KeyWordDefinition>(parts =>
            {
                KeyWord word = ParseEnumValue<KeyWord>(parts[1]);
                return new KeyWordDefinition(word) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<NumberDefinition> LoadNumbers(string filename)
        {
            Func<string[], NumberDefinition> f = new Func<string[], NumberDefinition>(parts =>
            {
                NumberType type = ParseEnumValue<NumberType>(parts[1]);
                int value = int.Parse(parts[2]);

                return new NumberDefinition(new Number(value, type))
                {
                    Text = parts[0],
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

                return new OrdinaryDefinition(type, size) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<SubordinaryDefinition> LoadSubordinaries(string filename)
        {
            Func<string[], SubordinaryDefinition> f = new Func<string[], SubordinaryDefinition>(parts =>
            {
                Subordinary type = ParseEnumValue<Subordinary>(parts[1]);

                return new SubordinaryDefinition(type) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<ChargeDefinition> LoadShapes(string filename)
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

                return new ChargeDefinition(charge) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private static List<ShapeTypeDefinition> LoadShapeTypes(string filename)
        {
            Func<string[], ShapeTypeDefinition> f = new Func<string[], ShapeTypeDefinition>(parts =>
            {
                ShapeType type = ParseEnumValue<ShapeType>(parts[1]);

                return new ShapeTypeDefinition(type) { Text = parts[0] };
            });

            return ParseCsvFile(filename, f);
        }

        private List<ChargePropertyDefinition> LoadChargeProperties(string blazondirectory)
        {
            List<ChargePropertyDefinition> properties = new List<ChargePropertyDefinition>();

            properties.AddRange(LoadCsvList(BlazonDirectory + "charge_properties.csv", "Charge properties",
                parts =>
                {
                    ChargePropertyDefinition propDef;
                    PropertyType type = ParseEnumValue<PropertyType>(parts[1]);
                    switch (type)
                    {
                        case PropertyType.Tail:
                            propDef = new ChargePropertyDefinition(PropertyType.Tail);
                            break;
                        case PropertyType.TailStyle:
                            propDef = new TailStylePropertyDefinition(ParseEnumValue<TailStyle>(parts[2]));
                            break;
                        default:
                            return null;
                    }
                    propDef.Text = parts[0];
                    return propDef;
                }));

            properties.AddRange(LoadCsvList(BlazonDirectory + "charge_prop_attitude.csv", "Charge attitudes",
                parts =>
                {
                    ChargeType[] exclusiveTo = null;
                    if (parts.Length > 2)
                    {
                        exclusiveTo = parts[2].Split(',')
                        .Select(s => ParseEnumValue<ChargeType>(s))
                        .ToArray();
                    }
                    return new AttitudePropertyDefinition(ParseEnumValue<Attitude>(parts[1]), exclusiveTo) { Text = parts[0] };
                }));


            properties.AddRange(LoadCsvList(BlazonDirectory + "charge_prop_attitude_direction.csv", "Charge attitude directions",
                parts =>
            {
                return new AttitudeDirectionPropertyDefinition(ParseEnumValue<AttitudeDirection>(parts[1])){ Text = parts[0] };
            }));

            properties.AddRange(LoadCsvList(blazondirectory + "charge_prop_features.csv", "Charge features",
                parts =>
                {
                    ChargeFeature feature = ParseEnumValue<ChargeFeature>(parts[1]);
                    return new FeaturePropertyDefinition(feature) { Text = parts[0] };
                }));

            return properties;
        }


        private static List<T> ParseCsvFile<T>(string filename, Func<string[], T> parseLineFunction)
        {
            int lineNumber = 0;

            return File.ReadLines(filename)
                .Select(line => { return line.Split(VALUE_SEPARATOR); })
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
