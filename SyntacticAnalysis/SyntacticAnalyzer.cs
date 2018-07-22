using Heraldry.Cli;
using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Compilers;
using Heraldry.SyntacticAnalysis.Formulas;
using Heraldry.SyntacticAnalysis.Formulas.FieldDivisions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heraldry.SyntacticAnalysis
{
    public class SyntacticAnalyzer : ParseProcess.Step<List<Token>, BlazonInstance>
    {
        private readonly bool formatExceptions;

        public SyntacticAnalyzer(bool formatExceptions = true)
        {
            this.formatExceptions = formatExceptions;
        }

        public override BlazonInstance Execute(List<Token> tokens)
        {
            RootCompiler root = new RootCompiler(tokens);
            if(!formatExceptions)
            {
                return root.Compile();
            }

            try
            {
                return root.Compile();
            }
            catch (ExpectedTokenNotFoundException ex)
            {
                Token currentToken = root.PeekToken();
                string expectedTokenTypes = String.Join(", ", ex.ExpectedTypes.Select(type => type.ToString()).ToArray());
                string currentTokenString = currentToken != null ? currentToken.GetFullType().ToString() : "null";

                string message = String.Format("Token {0} was not one of the expected tokens [{1}]",
                    currentTokenString, expectedTokenTypes);

                throw new UnexpectedTokenException(currentToken, message, ex);
            }

        }

    }
}
