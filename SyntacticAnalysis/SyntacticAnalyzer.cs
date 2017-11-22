using Heraldry.Blazon;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis
{
    public class SyntacticAnalyzer
    {
        /// <summary>
        /// Current position in tokens list.
        /// Initialized everytime new recursive descent is started.
        /// </summary>
        private int position;

        public BlazonInstance ParseTokens(List<LexicalAnalysis.Token> tokens)
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
        /// <param name="tokens">List of tokens to be parsed using recursive descent</param>
        /// <returns>Parsed coat of arms.</returns>
        protected CoatOfArms Coa(List<LexicalAnalysis.Token> tokens)
        {
            CoatOfArms coa = new CoatOfArms();
            Field content = Background(tokens);
            coa.Content = content;
            return coa;
        }

        /// <summary>
        /// Background of the shield of arms. Can be color, tincture, variation, ...
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed field.</returns>
        protected Field Background(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = SeekCurrentToken(tokens);
            Field f;
            // todo: support for variation
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    Filling fil = Tincture(tokens);
                    f = new Field(fil);
                    break;
                case DefinitionType.FieldDivision:
                    f = Division(tokens);
                    break;
                default:
                    f = null;
                    break;
            }
            return f;
        }

        /// <summary>
        /// Variation of the field. 
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        protected void Variation(List<LexicalAnalysis.Token> tokens)
        { 
            // todo: implement this
        }

        /// <summary>
        /// Division of the field.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Divided field.</returns>
        protected Field Division(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = PopCurrentToken(tokens);

            // first of all, type of the division is expected - quaterly, per pale, per fess, ...
            if (currentToken.Type == DefinitionType.FieldDivision)
            {
                FieldDivisionType divisionType = ((FieldDivisionDefinition)currentToken.Definition).Type;
                switch (divisionType)
                {
                    // todo: support line type definition
                    case FieldDivisionType.Quarterly:
                        return QDivision(tokens);
                    default:
                        return null;
                        // todo: throw exception when undefined division type is found
                }
            }
            else
            {
                return null;
                // todo: throw exception or something
            }
        }

        /// <summary>
        /// Definition of quaterly division is parsed by this rule.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        protected Field QDivision(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = SeekCurrentToken(tokens);
            Field res;
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    // quaterly division is defined by tinctures
                    // load them and create field from them
                    Filling tincture1 = Tincture(tokens);
                    currentToken = PopCurrentToken(tokens);
                    CheckTokenType(currentToken, DefinitionType.KeyWord, KeyWord.And);
                    Filling tincture2 = Tincture(tokens);
                    QuaterlyDivisionDefinition divDef = new QuaterlyDivisionDefinition(tincture1, tincture2);
                    res = new Field(divDef);
                    return res;
                case DefinitionType.Number:
                    // quaterly definition can be also defined by sequence of number-coa pairs
                    // or number and number coa
                    currentToken = PopCurrentToken(tokens);
                    CheckNumberToken(currentToken);
                    int num = ((NumberDefinition)currentToken.Definition).GetNumber();
                    Dictionary<int, Field> subfields = new Dictionary<int, Field>();
                    

                    // now either field definition or sequence of other numbers may follow
                    currentToken = SeekCurrentToken(tokens);
                    if(currentToken.Type != DefinitionType.KeyWord)
                    {
                        // if the token is not and, assume field definition follows
                        Field subField = Coa(tokens).Content;
                        subfields.Add(num, subField);
                    } else
                    {
                        // 'and' is expected with more numbers following
                        CheckAndToken(currentToken);
                        PopCurrentToken(tokens);
                        List<int> nums = Nums(tokens);
                        nums.Add(num);
                        Field subField = Coa(tokens).Content;
                        foreach(int n in nums)
                        {
                            subfields.Add(n, subField);
                        }
                    }

                    // semicolon should follow now
                    currentToken = PopCurrentToken(tokens);
                    CheckKeywordToken(currentToken, KeyWord.Semicolon);
                    Dictionary<int, Field> otherSubfields = NumDef(tokens);
                    foreach (int fieldNum in otherSubfields.Keys)
                    {
                        subfields.Add(fieldNum, otherSubfields[fieldNum]);
                    }

                    // put it all together
                    QuaterlyDivisionDefinition qDef = new QuaterlyDivisionDefinition(subfields);
                    res = new Field(qDef);
                    return res;
                default:
                    // todo: support more ways of specifying the division
                    return null;
            }
        }

        /// <summary>
        /// Tincture rule - tincture definition is expected to be at the current token.
        /// Fur definition can also start at current position.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed filling - tincture or fur.</returns>
        protected Filling Tincture(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = SeekCurrentToken(tokens);

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
            if (tinctureDef.Type == TinctureType.Colour || tinctureDef.Type == TinctureType.Metal)
            {
                PopCurrentToken(tokens);

                // process tincture
                TinctureDefinition def = new TinctureDefinition { Text = currentToken.Definition.Text };

                Filling filling = new Filling {
                    Layout = TinctureLayout.Solid,
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
        protected Filling Furs(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = PopCurrentToken(tokens);
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
        protected List<int> Nums(List<LexicalAnalysis.Token> tokens)
        {
            List<int> numbers = new List<int>();
            LexicalAnalysis.Token currentToken = PopCurrentToken(tokens);
            CheckNumberToken(currentToken);
            numbers.Add(((NumberDefinition)currentToken.Definition).GetNumber());

            // after the number token, either AND or something else is expected.
            currentToken = SeekCurrentToken(tokens);
            switch(currentToken.Type)
            {
                case DefinitionType.KeyWord:
                    currentToken = PopCurrentToken(tokens);
                    CheckAndToken(currentToken);
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
        protected Dictionary<int,Field> NumDef(List<LexicalAnalysis.Token> tokens)
        {
            List<int> numbers = Nums(tokens);
            Field f = Coa(tokens).Content;
            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach(int num in numbers)
            {
                fields.Add(num,f);
            }

            // if semicolon follows, more definitions are expected.
            LexicalAnalysis.Token currentToken = SeekCurrentToken(tokens);
            if(currentToken != null && currentToken.IsKeyWord(KeyWord.Semicolon))
            {
                // this one contains semicolon
                PopCurrentToken(tokens);

                // this is the next token
                currentToken = SeekCurrentToken(tokens);

                // if null, coa definition ends here
                if(currentToken != null)
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
        private LexicalAnalysis.Token PopCurrentToken(List<LexicalAnalysis.Token> tokens)
        {
            if (position == tokens.Count)
            {
                return null;
            }
            LexicalAnalysis.Token currentToken = tokens.ElementAt(position);
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
        private LexicalAnalysis.Token SeekCurrentToken(List<LexicalAnalysis.Token> tokens)
        {
            if(position == tokens.Count)
            {
                return null;
            }
            LexicalAnalysis.Token currentToken = tokens.ElementAt(position);
            return currentToken;
        }

        /// <summary>
        /// Checks whether the token is number and throws excpetion if not.
        /// If the type is number, but number is missing exception is also thrown.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        private void CheckNumberToken(LexicalAnalysis.Token token)
        {
            CheckTokenType(token, DefinitionType.Number);
            NumberDefinition numberDefinition = (NumberDefinition)token.Definition;
            if (numberDefinition.Text == null)
            {
                throw new Exception(String.Format("Number token at position {0} is missing its number value.",
                    position));
            }
        }

        /// <summary>
        /// Checks whether the token is AND connector and throws exception if not.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        private void CheckAndToken(LexicalAnalysis.Token token)
        {
            CheckKeywordToken(token, KeyWord.And);
        }

        /// <summary>
        /// Checks whether the token represents expected keyword and throws exception if not.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <param name="keyWord">Expected keyword.</param>
        private void CheckKeywordToken(LexicalAnalysis.Token token, KeyWord keyWord)
        {
            CheckTokenType(token, DefinitionType.KeyWord);
            KeyWordDefinition keyWordDefinition = (KeyWordDefinition)token.Definition;
            if (!token.IsKeyWord(keyWord))
            {
                throw new Exception(String.Format("Unexpected keyword token '{0}' at position {1}. Expected {2}.",
                    keyWordDefinition.KeyWord, position, KeyWord.And
                    ));
            }
        }

        /// <summary>
        /// Checks type of the token and thrown exception if it's wrong.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <param name="expectedType">Expected token type.</param>
        private void CheckTokenType(LexicalAnalysis.Token token, DefinitionType expectedType, object subtype = null)
        {
            if (token.Type != expectedType || (subtype != null && token.Subtype != subtype))
            {
                throw new UnexpectedTokenException(token, position, expectedType, subtype);
            }
        }

        /// <summary>
        /// Throws exception with formatted text:
        /// Unexpected token '{0}' at position {1}. Expected {2}.
        /// 
        /// </summary>
        /// <param name="unexpectedToken">Unexpected token ({0} parameter).</param>
        /// <param name="position">Position of token ({1} parameter).</param>
        /// <param name="expectedToken">String representation of expected token ({2} parameter).</param>
        private void UnexpectedTokenException(LexicalAnalysis.Token unexpectedToken, int position, String expectedToken)
        {
            throw new Exception(String.Format("Unexpected token '{0}' at position {1}. Expected {2}.",
                unexpectedToken, position, expectedToken));
        }
    }
}
