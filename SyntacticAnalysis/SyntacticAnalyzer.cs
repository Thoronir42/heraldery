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
    public class SyntacticAnalyzer
    {
        public BlazonInstance ParseTokens(List<Token> tokens)
        {
            //foreach (var t in tokens)
            //{
            //    Console.WriteLine(String.Format("{0} - {1}", t.Position, t.Type.ToString()));
            //}
            //return null;

            RootCompiler root = new RootCompiler(tokens);
            return root.compile();
        }
        
    }
}
