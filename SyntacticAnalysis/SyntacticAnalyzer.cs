using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Formulas;
using Heraldry.SyntacticAnalysis.Formulas.FieldDivisions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heraldry.SyntacticAnalysis
{
    public class SyntacticAnalyzer
    {
        /// <summary>
        /// Current position in tokens list.
        /// Initialized everytime new recursive descent is started.
        /// </summary>
        private int position;

        public BlazonInstance ParseTokens(List<Token> tokens)
        {

            //foreach (var t in tokens)
            //{
            //    Console.WriteLine(String.Format("{0} - {1}", t.Position, t.Type.ToString()));
            //}
            //return null;
            BlazonInstance bi = new BlazonInstance();
            Initialize();
            CoatOfArms coa = Coa(tokens);
            bi.CoatOfArms = coa;
            return bi;
        }

        /// <summary>
        /// Initialize parser. Called everytime before recursive descent starts.
        /// </summary>
        protected void Initialize()
        {
            position = 0;
        }

        /// <summary>
        /// Starting point of recursive descent.
        /// COA is an abbreviation for Coat Of Arms and this basically represents the whole shield of arms.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed using recursive descent.</param>
        /// <returns>Parsed coat of arms.</returns>
        protected CoatOfArms Coa(List<Token> tokens)
        {
            CoatOfArms coa = new CoatOfArms();
            Field content = Field(tokens);
            coa.Content = content;
            return coa;
        }

        /// <summary>
        /// Field rule. Field definition consists of backgound definition and optional principal charge.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed using recursive descent.</param>
        /// <returns>One field.</returns>
        protected Field Field(List<Token> tokens)
        {
            Field field = Background(tokens);
            Token nextToken = SeekCurrentToken(tokens);
            if(nextToken != null && IsTokenCharge(nextToken))
            {
                // charge definition follows
                Charge charge = PrincipalCharge(tokens);
                field.Charge = charge;
            }
            return field;
        }

        /// <summary>
        /// Rule which will parse principal charge.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Principal charge.</returns>
        protected Charge PrincipalCharge(List<Token> tokens)
        {
            return Ordinary(tokens);
        }

        /// <summary>
        /// Rule which will parse ordinary charges.
        /// Ordinates consist of ordinary type and tincture.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Ordinary charge.</returns>
        protected Charge Ordinary(List<Token> tokens)
        {
            Token currentToken = PopCurrentToken(tokens);
            CheckTokenType(currentToken, DefinitionType.Ordinary);
            Filling ordinaryFilling = Tincture(tokens);
            Ordinary ordinaryType = ((OrdinaryDefinition)currentToken.Definition).Type;
            OrdinarySize ordinarySize = ((OrdinaryDefinition)currentToken.Definition).Size;

            OrdinaryCharge charge = new OrdinaryCharge { OrdinaryType = ordinaryType, Filling = ordinaryFilling};
            return charge;
        }

        /// <summary>
        /// Background of the shield of arms. Can be color, tincture, variation, ...
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed field.</returns>
        protected Field Background(List<Token> tokens)
        {
            Token currentToken = SeekCurrentToken(tokens);
            Field f;
            // todo: support for variation
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    Filling fil = Tincture(tokens);
                    f = new Field { Background = fil };
                    break;
                case DefinitionType.FieldDivision:
                    f = Division(tokens);
                    break;
                case DefinitionType.Variation:
                    f = Variation(tokens);
                    break;
                default:
                    f = null;
                    break;
            }
            return f;
        }

        /// <summary>
        /// Division of the field.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Divided field.</returns>
        protected Field Division(List<Token> tokens)
        {
            Token currentToken = PopCurrentToken(tokens);

            // first of all, type of the division is expected - quaterly, per pale, per fess, ...
            if (currentToken.Type == DefinitionType.FieldDivision)
            {
                FieldDivisionType divisionType = ((FieldDivisionDefinition)currentToken.Definition).Type;
                if (divisionType == FieldDivisionType.Quarterly)
                {
                    return QDivision(tokens);
                } else if(divisionType.IsPartyPerDivision())
                {
                    Field f = PpDivision(tokens);
                    f.Division = divisionType;
                    return f;
                } else
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
        /// Rule which will parse field variations.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Field with defined variation.</returns>
        protected Field Variation(List<Token> tokens)
        {
            Token currentToken = PopCurrentToken(tokens);

            // first, the type of variation follows, then variation tinctures should be defined
            CheckTokenType(currentToken, DefinitionType.Variation, null);
            FieldVariationType variationType = ((FieldVariationDefinition)currentToken.Definition).VariationType;

            // for some types of variation, number (one number: number of stripes, waves, ...) definition may be expected
            // negative value cannot be used in blazon so it may be used here in code to determine whether or not 
            // was number defined
            currentToken = SeekCurrentToken(tokens);
            int number = Int32.MinValue; 
            if(ValidateTokenType(currentToken, DefinitionType.Number))
            {
                PopCurrentToken(tokens);
                number = ((NumberDefinition)currentToken.Definition).Value;
            } 

            // tinctures of a variation
            List<Filling> variationFillings = VariationFillings(tokens);
            
            FillingLayout fillingLayout = new FillingLayout();
            fillingLayout.SetVariationLayoutType(variationType);
            if(number != Int32.MinValue)
            {
                fillingLayout.Number = number;
            }
            Filling variatedFilling = new Filling();
            variatedFilling.Layout = fillingLayout;
            variatedFilling.AddTinctureDefinitions(variationFillings);


            return new Field { Background = variatedFilling };
        }

        /// <summary>
        /// Rule which will parse fillings for a variation.
        /// Basically: TINCTURE AND TINCTURE
        /// 
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns>List of defined tinctures.</returns>
        protected List<Filling> VariationFillings(List<Token> tokens)
        {
            List<Filling> fillings = new List<Filling>();
            
            // first filling
            fillings.Add(Tincture(tokens));

            // and
            Token currentToken = PopCurrentToken(tokens);
            CheckTokenType(currentToken, DefinitionType.KeyWord, KeyWord.And);

            // second filling
            fillings.Add(Tincture(tokens));

            return fillings;
        }

        /// <summary>
        /// Definition of quaterly division is parsed by this rule.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        protected Field QDivision(List<Token> tokens)
        {
            Token currentToken = SeekCurrentToken(tokens);
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    // quaterly division is defined by tinctures
                    // load them and create field from them
                    Filling tincture1 = Tincture(tokens);
                    currentToken = PopCurrentToken(tokens);
                    CheckTokenType(currentToken, DefinitionType.KeyWord, KeyWord.And);
                    Filling tincture2 = Tincture(tokens);
                    return new QuaterlyDividedField(tincture1, tincture2);
                case DefinitionType.Number:
                    // quaterly definition can be also defined by sequence of number-coa pairs
                    // or number and number coa
                    currentToken = PopCurrentToken(tokens);
                    CheckNumberToken(currentToken);
                    int num = ((NumberDefinition)currentToken.Definition).Value;
                    Dictionary<int, Field> subfields = new Dictionary<int, Field>();


                    // now either field definition or sequence of other numbers may follow
                    currentToken = SeekCurrentToken(tokens);
                    if (currentToken.Type != DefinitionType.KeyWord)
                    {
                        // if the token is not and, assume field definition follows
                        Field subField = Coa(tokens).Content;
                        subfields.Add(num, subField);
                    }
                    else
                    {
                        // 'and' is expected with more numbers following
                        CheckTokenType(currentToken, DefinitionType.KeyWord, KeyWord.And);
                        PopCurrentToken(tokens);
                        List<int> nums = Nums(tokens);
                        nums.Add(num);
                        Field subField = Field(tokens);
                        foreach (int n in nums)
                        {
                            subfields.Add(n, subField);
                        }
                    }

                    // semicolon should follow now
                    currentToken = PopCurrentToken(tokens);
                    CheckSemicolonToken(currentToken);
                    Dictionary<int, Field> otherSubfields = NumDef(tokens);
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
        /// Party per * division rule.
        /// 
        /// Party per * division is defined by two fields definitions.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Field defined by party per * division.</returns>
        protected Field PpDivision(List<Token> tokens)
        {
            Field field1 = Field(tokens);
            Token currentToken = PopCurrentToken(tokens);
            CheckAndToken(currentToken);
            Field field2 = Field(tokens);

            Field ppDividedField = new PartyPerDividedField(field1, field2);
            return ppDividedField;
        }

        /// <summary>
        /// Tincture rule - tincture definition is expected to be at the current token.
        /// Fur definition can also start at current position.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed filling - tincture or fur.</returns>
        protected Filling Tincture(List<Token> tokens)
        {
            Token currentToken = SeekCurrentToken(tokens);

            // check that current token is really a tincture
            CheckTokenType(currentToken, DefinitionType.Tincture);

            // 
            if (currentToken.Definition.GetType() != typeof(TinctureDefinition))
            {
                throw new Exception(String.Format("Unexpected type of definition '{0}' of token at position {1}. Expected {2}.",
                    currentToken.Definition.GetType(), position, typeof(TinctureDefinition)
                    ));
            }
            TinctureDefinition tinctureDef = (TinctureDefinition)currentToken.Definition;
            if (tinctureDef.TinctureType == TinctureType.Colour || tinctureDef.TinctureType == TinctureType.Metal)
            {
                PopCurrentToken(tokens);

                // process tincture
                TinctureDefinition def = new TinctureDefinition { Text = currentToken.Definition.Text };

                Filling filling = new Filling
                {
                    Layout = FillingLayout.Solid(),
                    Tinctures = new TinctureDefinition[] { def }
                };

                return filling;
            }
            else
            {
                // call fur rule
                return Furs(tokens);
            }
        }

        /// <summary>
        /// Furs definition - fur definition is expected to start at the current token.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed fur.</returns>
        protected Filling Furs(List<Token> tokens)
        {
            Token currentToken = PopCurrentToken(tokens);
            // todo: implement this
            return null;
        }

        /// <summary>
        /// A rule for parsing sequences of numbers in this form:
        /// number and number and ... and number 
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        protected List<int> Nums(List<Token> tokens)
        {
            List<int> numbers = new List<int>();
            Token currentToken = PopCurrentToken(tokens);
            CheckNumberToken(currentToken);
            numbers.Add(((NumberDefinition)currentToken.Definition).Value);

            // after the number token, either AND or something else is expected.
            currentToken = SeekCurrentToken(tokens);
            switch (currentToken.Type)
            {
                case DefinitionType.KeyWord:
                    currentToken = PopCurrentToken(tokens);
                    CheckTokenType(currentToken, DefinitionType.KeyWord, KeyWord.And);
                    numbers.AddRange(Nums(tokens));
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
        protected Dictionary<int, Field> NumDef(List<Token> tokens)
        {
            List<int> numbers = Nums(tokens);
            Field f = Coa(tokens).Content;
            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach (int num in numbers)
            {
                fields.Add(num, f);
            }

            // if semicolon follows, more definitions are expected.
            // however, blazon may end here and in that case, next token will be null
            Token currentToken = SeekCurrentToken(tokens);
            if (currentToken != null && ValidateSemicolonToken(currentToken))
            {
                // this one contains semicolon
                PopCurrentToken(tokens);

                // this is the next token
                currentToken = SeekCurrentToken(tokens);

                // if null, coa definition ends here
                if (currentToken != null)
                {
                    Dictionary<int, Field> otherFields = NumDef(tokens);
                    foreach (int num in otherFields.Keys)
                    {
                        fields.Add(num, otherFields[num]);
                    }

                }
            }

            return fields;
        }

        /// <summary>
        /// Returns the current token and increments the position counter.
        /// If no more tokens are available, null is returned.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens from which the current one will be returned.</param>
        /// <returns>Current token.</returns>
        private Token PopCurrentToken(List<Token> tokens)
        {
            if (position == tokens.Count)
            {
                return null;
            }
            Token currentToken = tokens.ElementAt(position);
            position += 1;
            return currentToken;
        }

        /// <summary>
        /// Return the current token but does not increment the position counter.
        /// If no more tokens are available, null is returned.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens from which the current one will be returned.</param>
        /// <returns>Current token.</returns>
        private Token SeekCurrentToken(List<Token> tokens)
        {
            if (position == tokens.Count)
            {
                return null;
            }
            Token currentToken = tokens.ElementAt(position);
            return currentToken;
        }

        /// <summary>
        /// Checks whether the token is number and throws excpetion if not.
        /// If the type is number, but number is missing exception is also thrown.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        private void CheckNumberToken(Token token)
        {
            CheckTokenType(token, DefinitionType.Number);
            NumberDefinition numberDefinition = (NumberDefinition)token.Definition;
        }



        /// <summary>
        /// Checks type of the token and thrown exception if it's wrong.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <param name="expectedType">Expected token type.</param>
        /// <param name="expSubtype">If specified, also matches token subtype</param>
        private void CheckTokenType(Token token, DefinitionType expectedType, object expSubtype = null)
        {
            if (!ValidateTokenType(token, expectedType, expSubtype))
            {
                throw new UnexpectedTokenException(token, position, expectedType, expSubtype);
            }
        }

        /// <summary>
        /// Check whether the token is a semicolon separator and throws exception if not.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        private void CheckSemicolonToken(Token token)
        {
            CheckTokenType(token, DefinitionType.Separator, Separator.Semicolon);
        }

        /// <summary>
        /// Checks whther the token is an and token and throws exception if not.
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        private void CheckAndToken(Token token)
        {
            CheckTokenType(token, DefinitionType.KeyWord, KeyWord.And);
        }

        /// <summary>
        /// Check whether the token is a semicolon separator.
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <returns>True if the token is semicolon.</returns>
        private bool ValidateSemicolonToken(Token token) 
        {
            return ValidateTokenType(token, DefinitionType.Separator, Separator.Semicolon);
        }

        /// <summary>
        /// Checks whether token type matches expected type and if provided, if tokens subtype matches expected subtype
        /// </summary>
        private bool ValidateTokenType(Token token, DefinitionType expectedType, object expSubtype = null)
        {
            return token.Type == expectedType && (expSubtype == null || expSubtype.Equals(token.Subtype));
        }

        /// <summary>
        /// Checks whether the definition of the token is charge: charge, ordinary, ...
        /// </summary>
        /// <param name="token">Token whose definition will be checked.</param>
        /// <returns>True if the token contains charge definition.</returns>
        private bool IsTokenCharge(Token token)
        {
            return token.Definition != null && (
                    token.Definition.GetType() == typeof(ChargeDefinition) ||
                    token.Definition.GetType() == typeof(OrdinaryDefinition)
                );
        }
    }
}
