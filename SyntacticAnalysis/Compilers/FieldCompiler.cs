using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Augmentations;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Formulas;
using Heraldry.SyntacticAnalysis.Formulas.FieldDivisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    class FieldCompiler : BaseCompiler
    {
        public FieldCompiler(RootCompiler root) : base(root)
        {

        }

        /// <summary>
        /// Division of the field.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Divided field.</returns>
        public DividedField Division()
        {
            Token currentToken = PopToken();

            // first of all, type of the division is expected - quaterly, per pale, per fess, ...
            if (currentToken.Type == DefinitionType.FieldDivision)
            {
                FieldDivisionType divisionType = ((FieldDivisionDefinition)currentToken.Definition).Type;
                if (divisionType == FieldDivisionType.Quarterly)
                {
                    return QDivision();
                }
                else if (divisionType.IsPartyPerDivision())
                {
                    DividedField f = PpDivision();
                    f.Division = divisionType;
                    return f;
                }
                else
                {
                    // todo: throw exception when undefined division type is found
                    return null;
                }
            }
            else
            {
                return null;
                // todo: throw exception or something
            }
        }

        /// <summary>
        /// Party per * division rule.
        /// 
        /// Party per * division is defined by two fields definitions.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Field defined by party per * division.</returns>
        protected DividedField PpDivision()
        {
            Field field1 = Field();

            PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

            Field field2 = Field();

            DividedField ppDividedField = new PartyPerDividedField(field1, field2);
            return ppDividedField;
        }

        /// <summary>
        /// Field rule. Field definition consists of backgound definition and optional principal charge.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed using recursive descent.</param>
        /// <returns>One field.</returns>
        public Field Field()
        {
            Token currentToken = PeekToken();
            Field field;
            // todo: support for variation
            switch (currentToken.Type)
            {
                case DefinitionType.FieldDivision:
                    field = Division();
                    break;

                case DefinitionType.Tincture:
                    var tincture = Compilers.Tincture.Tincture();

                    if (TokenIs(PeekToken(1), DefinitionType.Variation, FieldVariationType.SemeOf))
                    {
                        throw new NotImplementedException("Seme variation not implemented");
                    }
                    else
                    {
                        Filling fil = new SolidFilling(tincture);
                        field = new ContentField { Background = fil };
                    }

                    break;
                case DefinitionType.Variation:
                    field = Variation();
                    break;
                default:
                    return null;
            }

            Token nextToken = PeekToken();

            if(field is ContentField && IsTokenCharge(nextToken))
            {
                (field as ContentField).Charge = Compilers.Charge.PrincipalCharge();
            }

            if (TokenIs(nextToken, DefinitionType.KeyWord, KeyWord.Overall)) {
                PopToken();
                var aug = new FieldAugmentation(Compilers.Charge.PrincipalCharge());

                field.Augmentations.Add(aug);
            }

            return field;
        }

        /// <summary>
        /// Rule which will parse field variations.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Field with defined variation.</returns>
        protected ContentField Variation()
        {
            // first, the type of variation follows, then variation tinctures should be defined
            FieldVariationDefinition definition = PopDefinition<FieldVariationDefinition>(DefinitionType.Variation);
            FieldVariationType variationType = definition.VariationType;

            Filling filling;
            switch (Filling.TypeByVariation(variationType))
            {
                // todo: implement various field variation
                case FillingType.NPattern:
                    NumberDefinition numDef = PopDefinition<NumberDefinition>(DefinitionType.Number, NumberType.Cardinal);
                    filling = new PatternFilling(variationType, VariationFillings())
                    {
                        Number = numDef.Number.Value
                    };
                    break;
                case FillingType.Pattern:
                    filling = new PatternFilling(variationType, VariationFillings());
                    break;
                case FillingType.Seme:
                    // 
                    filling = new SemeFilling(null, Compilers.Charge.PrincipalCharge());
                    break;
                default:
                    throw new NotImplementedException("Filling type of " + variationType + " is not implemented");
            }


            return new ContentField { Background = filling };
        }

        /// <summary>
        /// Rule which will parse fillings for a variation.
        /// Basically: TINCTURE AND TINCTURE
        /// 
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns>List of defined tinctures.</returns>
        protected Tincture[] VariationFillings()
        {
            Tincture[] fillings = new Tincture[2];

            // first filling
            fillings[0] = Compilers.Tincture.Tincture();

            // and
            Token currentToken = PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

            // second filling
            fillings[1] = Compilers.Tincture.Tincture();

            return fillings;
        }

        /// <summary>
        /// Definition of quaterly division is parsed by this rule.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        protected DividedField QDivision()
        {
            Token currentToken = PeekToken();
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    // quaterly division is defined by tinctures
                    // load them and create field from them
                    Filling tincture1 = new SolidFilling(Compilers.Tincture.Tincture());
                    currentToken = PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

                    Filling tincture2 = new SolidFilling(Compilers.Tincture.Tincture());
                    return new QuaterlyDividedField(tincture1, tincture2);

                case DefinitionType.Number:
                    // quaterly definition can be also defined by sequence of number-coa pairs
                    // or number and number coa
                    currentToken = PopTokenAs(DefinitionType.Number);
                    int num = ((NumberDefinition)currentToken.Definition).Number.Value;

                    Dictionary<int, Field> subfields = new Dictionary<int, Field>();


                    // now either field definition or sequence of other numbers may follow
                    currentToken = PeekToken();
                    if (currentToken.Type != DefinitionType.KeyWord)
                    {
                        // if the token is not and, assume field definition follows
                        Field subField = Field();
                        subfields.Add(num, subField);
                    }
                    else
                    {
                        // 'and' is expected with more numbers following
                        PopTokenAs(DefinitionType.KeyWord, KeyWord.And);
                        List<int> nums = Nums();
                        nums.Add(num);
                        Field subField = Field();
                        foreach (int n in nums)
                        {
                            subfields.Add(n, subField);
                        }
                    }

                    // semicolon should follow now
                    currentToken = PopTokenAs(DefinitionType.Separator, Separator.Semicolon);

                    Dictionary<int, Field> otherSubfields = NumDef();
                    foreach (int fieldNum in otherSubfields.Keys)
                    {
                        subfields.Add(fieldNum, otherSubfields[fieldNum]);
                    }

                    // put it all together
                    QuaterlyDividedField qDef = new QuaterlyDividedField(subfields);
                    return qDef;
                default:
                    // todo: support more ways of specifying the division
                    return null;
            }

        }


        /// <summary>
        /// A rule for parsing sequences of numbers in this form:
        /// number and number and ... and number 
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        protected List<int> Nums()
        {
            List<int> numbers = new List<int>();
            Token currentToken = PopTokenAs(DefinitionType.Number);

            numbers.Add(((NumberDefinition)currentToken.Definition).Number.Value);

            // after the number token, either AND or something else is expected.
            currentToken = PeekToken();
            switch (currentToken.Type)
            {
                case DefinitionType.KeyWord:
                    currentToken = PopTokenAs(DefinitionType.KeyWord, KeyWord.And);
                    numbers.AddRange(Nums());

                    return numbers;
                default:
                    // continue with parsing
                    return numbers;
            }
        }

        /// <summary>
        /// Parsing rule for divisions defined by numbers (1 field, 2 field, 3 and 4 field, ...).
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns></returns>
        protected Dictionary<int, Field> NumDef()
        {
            List<int> numbers = Nums();
            Field f = Field();

            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach (int num in numbers)
            {
                fields.Add(num, f);
            }

            // if semicolon follows, more definitions are expected.
            // however, blazon may end here and in that case, next token will be null
            Token currentToken = PeekToken();
            if (TokenIs(currentToken, DefinitionType.Separator, Separator.Semicolon))
            {
                // this one contains semicolon
                PopToken();

                // this is the next token
                currentToken = PeekToken();

                // if null, coa definition ends here
                if (currentToken != null)
                {
                    Dictionary<int, Field> otherFields = NumDef();
                    foreach (int num in otherFields.Keys)
                    {
                        fields.Add(num, otherFields[num]);
                    }

                }
            }

            return fields;
        }
    }
}
