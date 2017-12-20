using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    using Heraldry.Blazon;
    using Heraldry.Blazon.Vocabulary;
    using Heraldry.CLI;
    using Heraldry.LexicalAnalysis;
    using Heraldry.LexicalAnalysis.Numbers;
    using Heraldry.Rendering;
    using Heraldry.Rendering.Svg;
    using Heraldry.Rendering.Text;
    using Heraldry.SyntacticAnalysis;
    using System.IO;

    class Program
    {
        private static CliSettings ProcesArgs(params string[] args)
        {
            CliSettings settings = new CliSettings(args);

            // todo: remove debug settings initialization
            settings = new CliSettings("-v", "-l", "en_olde", "-r", "Text", ".\\resources\\input\\Arms of Churchil.txt");

            if (settings.Verbose)
            {
                Console.WriteLine("=== Initializing blazon convertor with: ");
                Console.WriteLine(" Language   : " + settings.Language);
                Console.WriteLine(" InputFile  : " + settings.InputFile);
                Console.WriteLine(" OutputFile : " + settings.OutputFile);
                Console.WriteLine(" RenderType : " + settings.RenderType);
            }

            return settings;
        }

        static void Main(string[] args)
        {
            var settings = ProcesArgs(args);

            Console.WriteLine("\n=== Loading blazon vocabulary from " + settings.Language);
            BlazonVocabulary vocabulary = VocabularyLoader.LoadFromDirectory(Environment.CurrentDirectory + "\\resources\\" + settings.Language + "\\");


            string input = File.ReadAllText(settings.InputFile);


            var renderer = RendererByType(settings.RenderType, vocabulary);
            renderer.PrintStream = File.Open(settings.OutputFile, FileMode.Create);
            try
            {
                var result = new ParseProcess<string>(input)
                .Then(new LexAnalyzer(vocabulary, new NumberParser_en_olde()), "Lexical analysis")
                .Pause()
                .Then(new SyntacticAnalyzer(), "Syntactic analysis")
                .Then(renderer, "Rendering").Value;

                if (result)
                {
                    Console.WriteLine("Rendition finished successfully.");
                }
                else
                {
                    Console.WriteLine("Error occurred during rendition");
                }
            }
            catch (UnexpectedTokenException ex)
            {
                Console.Error.WriteLine("Unexpected token: " + ex.TokenText + " at position " + ex.TokenPosition);
            }


            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }

        static CrestRenderer RendererByType(RenderType type, BlazonVocabulary vocabulary)
        {
            switch (type)
            {
                case RenderType.Svg: return new SvgRenderer();
                case RenderType.Text: return new TextRenderer(vocabulary.GetDefiner());
            }

            throw new ArgumentException("Render type " + type.ToString() + " is not supported yet");
        }
    }
}
