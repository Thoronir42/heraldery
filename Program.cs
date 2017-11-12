using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry
{
    using Heraldry.Blazon;
    using Heraldry.Blazon.Vocabulary;
    using Heraldry.CLI;
    using Heraldry.LexicalAnalysis;
    using Heraldry.Rendering;
    using Heraldry.SyntacticAnalysis;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            CliSettings settings = new CliSettings(args);

            // Todo: fix resource loading
            Console.WriteLine("=== Loading blazon vocabulary from " + settings.Language);
            BlazonVocabulary blazon = new BlazonVocabulary(Environment.CurrentDirectory + "\\resources\\" + settings.Language + "\\");

            // todo: editable input source
            String input = File.ReadAllText(Environment.CurrentDirectory + "\\resources\\input\\" + settings.InputFile);

            //NOW I WILL TRY TO FORESEE THE FUTURE OF OUR APPLICATION
            //BEHOLD
            Console.WriteLine("=== Lexical analysis");
            LexAnalyzer lex = new LexAnalyzer(blazon);
            var tokens = lex.ParseText(input);

            Console.WriteLine("=== Syntactic analysis");
            SyntacticAnalyzer synt = new SyntacticAnalyzer();
            var crest = synt.ParseTokens(tokens);

            Console.WriteLine("=== Rendering");
            CrestRenderer gen = new CrestRenderer();
            gen.Render(crest);

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
