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

        public UnexpectedTokenException(Token token, int iThToken = -1, DefinitionType? expectedType = null, object subtype = null)
            : base(FormatMessage(token, iThToken, expectedType, subtype))
        {
            this.token = token;
        }

        public UnexpectedTokenException(Token token, string message) : base(message)
        {
            this.token = token;
        }

        public UnexpectedTokenException(string message) : base(message)
        {
        }

        public UnexpectedTokenException(ExpectedTokenNotFoundException ex) : base("", ex)
        {
            
        }

        private static String FormatMessage(Token token, int iThToken = -1, DefinitionType? expectedType = null, object subtype = null)
        {
            String iStr = iThToken != -1 ? iThToken + ": " : "";
            String typeError = "";

            bool subtypeMismatch = (subtype != null && token.Subtype != subtype);
            if(expectedType != token.Type || subtypeMismatch)
            {
                String expectedTypeStr = "" + expectedType;
                String actualTypeStr = "" + token.Type;
                if(subtypeMismatch)
                {
                    expectedTypeStr += "-" + subtype;
                    actualTypeStr += "-" + token.Subtype;
                    typeError = " of type " + actualTypeStr+ ". Expecting type " + expectedTypeStr;
                }
            }
            return String.Format("Unexpected token {0}\"{1}\"{2} at char position {3}", iStr, token.Definition.Text, typeError, token.Position);
        }
    }
}
