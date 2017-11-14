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
            CoatOfArms coa = Coa(tokens);
            bi.CoatOfArms = coa;
            return bi;
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
            position = 0;
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
                FieldDivisionType divisionType = (FieldDivisionType)currentToken.Value;
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
            switch (currentToken.Type)
            {
                case DefinitionType.Tincture:
                    // quaterly division is defined by tinctures
                    // load them and create field from them
                    Filling tincture1 = Tincture(tokens);
                    currentToken = PopCurrentToken(tokens);
                    CheckTokenType(currentToken, DefinitionType.And);
                    Filling tincture2 = Tincture(tokens);
                    QuaterlyDivisionDefinition divDef = new QuaterlyDivisionDefinition(tincture1, tincture2);
                    Field f = new Field(divDef);
                    return f;
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
                Filling filling = new Filling();
                filling.Layout = TinctureLayout.Solid;
                TinctureDefinition def = new TinctureDefinition();
                def.Text = currentToken.Definition.Text;
                filling.Tinctures = new TinctureDefinition[] {def};
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
        /// Returns the current token and increments the position counter.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens from which the current one will be returned.</param>
        /// <returns>Current token.</returns>
        private LexicalAnalysis.Token PopCurrentToken(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = tokens.ElementAt(position);
            position += 1;
            return currentToken;
        }

        /// <summary>
        /// Return the current token but does not increment the position counter.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens from which the current one will be returned.</param>
        /// <returns>Current token.</returns>
        private LexicalAnalysis.Token SeekCurrentToken(List<LexicalAnalysis.Token> tokens)
        {
            LexicalAnalysis.Token currentToken = tokens.ElementAt(position);
            return currentToken;
        }

        /// <summary>
        /// Check type of the token and thrown exception if it's wrong.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <param name="expectedType">Expected token type.</param>
        private void CheckTokenType(LexicalAnalysis.Token token, DefinitionType expectedType)
        {
            if (token.Type != expectedType)
            {
                throw new Exception(String.Format("Unexpected type of token '{0}' at position {1}. Expected {2}.",
                    token.Type, position, expectedType
                    ));
            }
        }
    }
}
