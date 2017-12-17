using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    public abstract class CompilerTestBase
    {
        private TokenCreator tokenCreator = new TokenCreator();

        protected TokenCreator Token { get { return tokenCreator; } }

        protected RootCompiler CreateRoot(params Token[] tokens)
        {
            List<Token> list = new List<Token>(tokens);

            return new RootCompiler(list);
        }
    }
}
