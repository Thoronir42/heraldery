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

    class Program
    {
        static void Main(string[] args)
        {
            CliSettings settings = new CliSettings(args);

            // Todo: fix resource loading
            BlazonVocabulary blazon = new BlazonVocabulary(Environment.CurrentDirectory + "\\resources\\" + settings.Language + "\\");

            //NOW I WILL TRY TO FORESEE THE FUTURE OF OUR APPLICATION
            //BEHOLD
            LexAnalyzer lex = new LexAnalyzer(blazon);
            var tokens = lex.ParseText("Todo: Todd, add real input. Wtf?");

            SyntacticAnalyzer synt = new SyntacticAnalyzer();
            var crest = synt.ParseTokens(tokens);

            CrestRenderer gen = new CrestRenderer();
            gen.Render(crest);

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
