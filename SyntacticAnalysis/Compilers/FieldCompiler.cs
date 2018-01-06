﻿using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Augmentations;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Attributes;
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
        [SyntacticRule]
        public DividedField Division()
        {
            var definition = PopDefinition<FieldDivisionDefinition>(DefinitionType.FieldDivision);

            // first of all, type of the division is expected - quaterly, per pale, per fess, ...

            FieldDivisionType divisionType = definition.Type;
            if (divisionType == FieldDivisionType.Quarterly)
            {
                return QDivision();
            }
            if (divisionType.IsPartyPerDivision())
            {
                DividedField f = PpDivision();
                f.Division = divisionType;
                return f;
            }

            throw new NotImplementedException("Field division " + divisionType.ToString() + " is not implemented");
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
        [SyntacticRule]
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

            if (field is ContentField && IsTokenCharge(nextToken))
            {
                (field as ContentField).Charge = Compilers.Charge.PrincipalCharge();
            }

            if (TokenIs(nextToken, DefinitionType.KeyWord, KeyWord.Overall))
            {
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
            if (NextTokenIs(DefinitionType.Tincture))
            {
                // quaterly division is defined by tinctures
                // load them and create field from them
                Filling tincture1 = new SolidFilling(Compilers.Tincture.Tincture());
                PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

                Filling tincture2 = new SolidFilling(Compilers.Tincture.Tincture());
                return new QuaterlyDividedField(tincture1, tincture2);
            }

            Dictionary<int, Field> subfields = new Dictionary<int, Field>();

            if (NextTokenIs(DefinitionType.Number))
            {
                // quaterly definition can be also defined by sequence of number-coa pairs
                // or number and number coa

                List<Number> nums = Compilers.Numbers.Nums(NumberType.Ordinal);
                Field subField = Field();

                foreach (var n in nums)
                {
                    subfields.Add(n.Value, subField);
                }

                // semicolon should follow now
                PopTokenAs(DefinitionType.Separator, Separator.Semicolon);

                Dictionary<int, Field> otherSubfields = NumDef();
                foreach (int fieldNum in otherSubfields.Keys)
                {
                    subfields.Add(fieldNum, otherSubfields[fieldNum]);
                }

                // put it all together
                QuaterlyDividedField qDef = new QuaterlyDividedField(subfields);
                return qDef;
            }

            throw new ExpectedTokenNotFoundException(TokenType.Types(DefinitionType.Number, DefinitionType.Tincture));
        }


        /// <summary>
        /// Parsing rule for divisions defined by numbers (1 field, 2 field, 3 and 4 field, ...).
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns></returns>
        protected Dictionary<int, Field> NumDef()
        {
            List<Number> numbers = Compilers.Numbers.Nums();
            Field f = Field();

            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach (var num in numbers)
            {
                fields.Add(num.Value, f);
            }

            // if semicolon follows, more definitions are expected.
            // however, blazon may end here and in that case, next token will be null
            if (NextTokenIs(DefinitionType.Separator, Separator.Semicolon))
            {
                // this one contains semicolon
                PopToken();

                // if null, coa definition ends here
                if (PeekToken() != null)
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
