using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis
{
    class UnexpectedTokenException : Exception
    {
        private Token token;

        public int TokenPosition
        {
            get { return token.Position; }
        }
        public String TokenText
        {
            get { return token.Definition.Text; }
        }

        public UnexpectedTokenException(Token token, string message, Exception ex = null) : base(message, ex)
        {
            this.token = token;
        }

        public UnexpectedTokenException(Token token, TokenType expectedType, int iThToken = -1)
            : this(token, FormatMessage(token, expectedType, iThToken))
        {

        }

        private static String FormatMessage(Token token, TokenType expectedType, int iThToken = -1)
        {
            if (token == null)
            {
                return String.Format("Token of type {0} was expected but no tokens found.",
                    expectedType.ToString());
            }

            String iStr = iThToken != -1 ? " at position " + iThToken + " " : "";

            return String.Format("Token of type {0}{1} did not met by expected token type of {2}",
                    token.GetFullType().ToString(), iStr, expectedType.ToString());
        }
    }
}
