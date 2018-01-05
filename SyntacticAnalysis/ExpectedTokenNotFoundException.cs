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

        public ExpectedTokenNotFoundException(DefinitionType expectedType, object expectedSubtype, string message = null)
            : this(message, new TokenType(expectedType, expectedSubtype))
        {

        }

        public ExpectedTokenNotFoundException(params TokenType[] expectedTypes)
        {
            this.ExpectedTypes = expectedTypes;
        }

        public ExpectedTokenNotFoundException(string message, params TokenType[] expectedTypes) : base(message)
        {
            this.ExpectedTypes = expectedTypes;
        }
    }
}
