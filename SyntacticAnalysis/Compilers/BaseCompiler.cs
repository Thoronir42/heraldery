using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public abstract class BaseCompiler
    {
        private RootCompiler root;

        protected RootCompiler Compilers { get { return root; }  }

        internal BaseCompiler(RootCompiler root)
        {
            this.root = root;
        }

        protected Token PopToken()
        {
            return root.PopToken();
        }

        protected Token PopTokenAs(DefinitionType type, object subtype = null)
        {
            Token token = PopToken();
            EnsureTokenIs(token, type, subtype);

            return token;
        }

        protected TDefinition PopDefinition<TDefinition>(DefinitionType type, object subtype = null) where TDefinition : Definition
        {
            Token token = PopTokenAs(type, subtype);
            TDefinition definition = token.Definition as TDefinition;
            
            return definition;
        }

        protected Token PeekToken(int offset = 0)
        {
            return root.PeekToken(offset);
        }


        /// <summary>
        /// Checks type of the token and thrown exception if it's wrong.
        /// 
        /// </summary>
        /// <param name="token">Token to be checked.</param>
        /// <param name="expectedType">Expected token type.</param>
        /// <param name="expSubtype">If specified, also matches token subtype</param>
        protected void EnsureTokenIs(Token token, DefinitionType expectedType, object expSubtype = null)
        {
            if (!TokenIs(token, expectedType, expSubtype))
            {
                throw new UnexpectedTokenException(token, root.Position, expectedType, expSubtype);
            }
        }

        /// <summary>
        /// Checks whether token type matches expected type and if provided, if tokens subtype matches expected subtype
        /// </summary>
        protected bool TokenIs(Token token, DefinitionType expectedType, object expSubtype = null)
        {
            return token != null && token.Type == expectedType && (expSubtype == null || expSubtype.Equals(token.Subtype));
        }

        /// <summary>
        /// Checks whether the definition of the token is charge: charge, ordinary, ...
        /// </summary>
        /// <param name="token">Token whose definition will be checked.</param>
        /// <returns>True if the token contains charge definition.</returns>
        protected bool IsTokenCharge(Token token)
        {
            if(token == null)
            {
                return false;
            }
            switch(token.Type)
            {
                case DefinitionType.Charge:
                case DefinitionType.Ordinary:
                    return true;
            }

            return false;
        }
    }
}
