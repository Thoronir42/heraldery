using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry {

    using Heraldery.LexicalAnalysis;
    using Heraldery.Rendering;
    using Heraldery.SyntacticAnalysis;

    class Program {
        static void Main(string[] args) {
            if(args.Length <= 0) {
                Console.Out.WriteLine("No parameters :(");
                //Environment.Exit(0);
            } else {
                string input = Console.In.ReadLine();
                Console.Out.WriteLine("Processing parameters . . .");

                //NOW I WILL TRY TO FORESEE THE FUTURE OF OUR APPLICATION
                //BEHOLD
                LexAnalyzer lex = new LexAnalyzer();
                var tokens = lex.ParseText(input);

                SyntacticAnalyzer synt = new SyntacticAnalyzer();
                var crest = synt.ParseTokens(tokens);

                CrestRenderer gen = new CrestRenderer();
                gen.Render(crest); // todo: return value
            }
        }
    }
}
