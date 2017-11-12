using Heraldry.Blazon;
using Heraldry.Blazon.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis
{
    class SyntacticAnalyzer
    {
        public BlazonInstance ParseTokens(List<LexicalAnalysis.Token> tokens)
        {
            foreach (var t in tokens)
            {
                Console.WriteLine(String.Format("{0} - {1}", t.Position, t.Type.ToString()));
            }
            return null;
        }
    }
}
