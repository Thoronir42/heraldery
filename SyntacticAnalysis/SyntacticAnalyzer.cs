using Heraldry.App;
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
    public class SyntacticAnalyzer : ParseStep<List<Token>, BlazonInstance>
    {
        public override BlazonInstance Execute(List<Token> tokens)
        {   
            RootCompiler root = new RootCompiler(tokens);

            return root.Compile();
        }
        
    }
}
