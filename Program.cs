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
    using Heraldry.LexicalAnalysis.Numbers;
    using Heraldry.Rendering;
    using Heraldry.SyntacticAnalysis;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            CliSettings settings = new CliSettings(args);

            // todo: remove debug settings initialization
            settings = new CliSettings("-l", "en_olde", "-r", "Text", ".\\resources\\input\\Arms of Churchil.txt");

            Console.WriteLine("=== Initializing blazon convertor with: ");
            Console.WriteLine(" Language   : " + settings.Language);
            Console.WriteLine(" InputFile  : " + settings.InputFile);
            Console.WriteLine(" OutputFile : " + settings.OutputFile);
            Console.WriteLine(" RenderType : " + settings.RenderType);

            // Todo: fix resource loading
            Console.WriteLine("\n=== Loading blazon vocabulary from " + settings.Language);
            BlazonVocabulary vocabulary = VocabularyLoader.LoadFromDirectory(Environment.CurrentDirectory + "\\resources\\" + settings.Language + "\\");


            String input = File.ReadAllText(settings.InputFile);

            //NOW I WILL TRY TO FORESEE THE FUTURE OF OUR APPLICATION
            //BEHOLD

            Console.WriteLine("\n=== Lexical analysis");
            LexAnalyzer lex = new LexAnalyzer(vocabulary, new NumberParser_en_olde());
            var tokens = lex.ParseText(input);

            Console.WriteLine("\n=== Syntactic analysis");
            SyntacticAnalyzer synt = new SyntacticAnalyzer();
            var crest = synt.ParseTokens(tokens);

            Console.WriteLine("\n=== Rendering");
            CrestRenderer gen = CrestRenderer.GetByType(settings.RenderType);

            FileStream output = File.Open(settings.OutputFile, FileMode.Create);
            if(gen.Render(crest, output))
            {
                Console.WriteLine("Rendition finished successfully.");
            } else
            {
                Console.WriteLine("Error occurred during rendition");
            }

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
