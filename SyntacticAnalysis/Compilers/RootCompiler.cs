using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public class RootCompiler
    {
        private List<Token> tokens;
        private int position;

        internal FieldCompiler Field { get; }
        internal FillingCompiler Filling { get; }
        internal ChargeCompiler Charge { get; }
        internal NumberCompiler Numbers { get; }
        internal PopCompiler Pop { get; }

        /// <summary>
        /// Current position in tokens list.
        /// Initialized everytime new recursive descent is started.
        /// </summary>
        public int Position { get { return position; } }

        public RootCompiler (List<Token> tokens)
        {
            this.tokens = tokens;

            this.Field = new FieldCompiler(this);
            this.Filling = new FillingCompiler(this);
            this.Charge = new ChargeCompiler(this);
            this.Numbers = new NumberCompiler(this);
            this.Pop = new PopCompiler(this);
        }

        public BlazonInstance Compile()
        {
            BlazonInstance bi = new BlazonInstance();

            bi.CoatOfArms = Coa();

            return bi;
        }

        /// <summary>
        /// Starting point of recursive descent.
        /// COA is an abbreviation for Coat Of Arms and this basically represents the whole shield of arms.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed using recursive descent.</param>
        /// <returns>Parsed coat of arms.</returns>
        protected CoatOfArms Coa()
        {
            CoatOfArms coa = new CoatOfArms();
            Field content = this.Field.Field();
            coa.Content = content;

            return coa;
        }

        /// <summary>
        /// Returns the current token and increments the position counter.
        /// If no more tokens are available, null is returned.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens from which the current one will be returned.</param>
        /// <returns>Current token.</returns>
        internal Token PopToken()
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
        internal Token PeekToken(int lookahead = 0)
        {
            if(lookahead < 0)
            {
                throw new ArgumentException("Lookahead must be a positive number or zero");
            }

            if (position + lookahead >= tokens.Count)
            {
                return null;
            }
            
            Token currentToken = tokens.ElementAt(position + lookahead);
            return currentToken;
        }

        public SequenceLink Sequence()
        {
            return new SequenceLink(this);
        }
    }
}
