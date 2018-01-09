using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis
{
    public class ExpectedTokenNotFoundException : Exception
    {
        public TokenType[] ExpectedTypes { get; }

        public ExpectedTokenNotFoundException(DefinitionType expectedType, object expectedSubtype = null, string message = null)
            : this(message, new TokenType(expectedType, expectedSubtype))
        {

        }

        public ExpectedTokenNotFoundException(params TokenType[] expectedTypes) :base()
        {
            this.ExpectedTypes = expectedTypes;
        }

        public ExpectedTokenNotFoundException(string message, params TokenType[] expectedTypes) : base(message ?? FormatTypes(expectedTypes))
        {
            this.ExpectedTypes = expectedTypes;
        }


        private static string FormatTypes(TokenType[] types)
        {
            return "Expected types: " + String.Join(", ", types.Select(t => t.ToString()).ToArray());
        }
    }
}
