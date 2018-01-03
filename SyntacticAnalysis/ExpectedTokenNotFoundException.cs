using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis
{
    class ExpectedTokenNotFoundException : Exception
    {
        public TokenType[] ExpectedTypes { get; }

        public ExpectedTokenNotFoundException(DefinitionType expectedType, object expectedSubtype)
            : this(new TokenType(expectedType, expectedType))
        {

        }

        public ExpectedTokenNotFoundException(params TokenType[] expectedTypes)
        {
            this.ExpectedTypes = expectedTypes;
        }
    }
}
