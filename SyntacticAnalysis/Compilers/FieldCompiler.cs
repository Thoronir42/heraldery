using Heraldry.Blazon.Elements;
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

            switch (currentToken.Type)
            {
                case DefinitionType.FieldDivision:
                    field = DividedField();
                    break;

                case DefinitionType.Tincture:
                    goto case DefinitionType.Variation;
                case DefinitionType.Variation:
                    field = ContentField();
                    break;

                default:
                    throw new ExpectedTokenNotFoundException(TokenType.Types(DefinitionType.FieldDivision, DefinitionType.Tincture, DefinitionType.Variation));
            }

            if (NextTokenIs(DefinitionType.Comment))
            {
                var commentDef = PopDefinition<CommentDefinition>(DefinitionType.Comment);
                field.Comment = commentDef.Comment;
            }

            return field;
        }


        public ContentField ContentField()
        {
            ContentField field;

            Token nextToken = PeekToken();

            switch (nextToken.Type)
            {
                case DefinitionType.Tincture:
                    var tincture = Compilers.Filling.Tincture();

                    if (TokenIs(PeekToken(1), DefinitionType.Variation, FieldVariationType.SemeOf))
                    {
                        throw new NotImplementedException("Seme variation not implemented");
                    }
                    else
                    {
                        field = new ContentField(new SolidFilling(tincture));
                    }

                    break;

                case DefinitionType.Variation:
                    field = new ContentField(Compilers.Filling.Filling());
                    break;

                default:
                    throw new ExpectedTokenNotFoundException(DefinitionType.Tincture, DefinitionType.Variation);
            }


            if (NextTokenIs(DefinitionType.Separator, Separator.Comma))
            {
                PopToken();
            }

            if (NextTokenIs(DefinitionType.KeyWord, KeyWord.Determiner) || 
                NextTokenIs(TokenType.Types(DefinitionType.Charge, DefinitionType.Ordinary)))
            {
                field.Charge = Compilers.Charge.PrincipalCharge();
            }

            return field;
        }


        protected DividedField DividedField()
        {
            var definition = PopDefinition<FieldDivisionDefinition>(DefinitionType.FieldDivision);

            
            DividedField field = null;
            if (definition.Type == FieldDivisionType.Quarterly)
            {
                field = QDivision();
            }

            if (definition.Type.IsPartyPerDivision())
            {
                field = PpDivision(definition.Type);
            }

            if(field == null)
            {
                throw new NotImplementedException("Field division " + definition.Type.ToString() + " is not implemented");
            }

            if (NextTokenIs(DefinitionType.KeyWord, KeyWord.Overall))
            {
                PopToken();
                var aug = new FieldAugmentation(Compilers.Charge.PrincipalCharge(), FieldAugType.Overall);

                field.Augmentations.Add(aug);
            }

            return field;
        }

        /// <summary>
        /// Party per * division rule.
        /// 
        /// Party per * division is defined by two fields definitions.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Field defined by party per * division.</returns>
        protected DividedField PpDivision(FieldDivisionType divisionType)
        {
            Field field1 = Field();

            PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

            Field field2 = Field();

            DividedField ppDividedField = new PartyPerDividedField(divisionType, field1, field2);
            return ppDividedField;
        }

        /// <summary>
        /// Definition of quaterly division is parsed by this rule.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        protected DividedField QDivision()
        {
            if (NextTokenIs(DefinitionType.Separator, Separator.Colon))
            {
                PopToken();
            }

            if (NextTokenIs(DefinitionType.Tincture))
            {
                // quaterly division is defined by tinctures
                // load them and create field from them

                Field field = Compilers.Field.Field();
                PopTokenAs(DefinitionType.KeyWord, KeyWord.And);
                Field field1 = Compilers.Field.Field();

                return new QuaterlyDividedField(field, field1);
            }

            Dictionary<int, Field> subfields = new Dictionary<int, Field>();

            int groupsParsed = 0;
            while (NextTokenIs(DefinitionType.Number, NumberType.Ordinal))
            {
                foreach (var n in NumsField())
                {
                    subfields.Add(n.Key, n.Value);
                }

                if (NextTokenIs(DefinitionType.Separator, Separator.Semicolon))
                {
                    PopToken();
                }
                else
                {
                    if (groupsParsed == 0)
                    {
                        throw new ExpectedTokenNotFoundException(DefinitionType.Separator, Separator.Semicolon);
                    }
                }

                groupsParsed++;
            }

            // put it all together
            return new QuaterlyDividedField(subfields);

            //throw new ExpectedTokenNotFoundException(TokenType.Types(DefinitionType.Number, DefinitionType.Tincture));
        }


        /// <summary>
        /// Parsing rule for divisions defined by numbers (1 field, 2 field, 3 and 4 field, ...).
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns></returns>
        protected Dictionary<int, Field> NumsField()
        {
            List<Number> numbers = Compilers.Numbers.Nums();
            Field f = Field();

            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach (var num in numbers)
            {
                fields.Add(num.Value, f);
            }

            return fields;
        }
    }
}
