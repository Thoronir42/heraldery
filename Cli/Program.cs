﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Cli
{
    using Heraldry.Blazon;
    using Heraldry.Blazon.Vocabulary;
    using Heraldry.LexicalAnalysis;
    using Heraldry.Rendering;
    using Heraldry.Rendering.Svg;
    using Heraldry.Rendering.Text;
    using Heraldry.SyntacticAnalysis;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            CliSettings settings = new CliSettings();
            try
            {
                settings.ProcessArguments(args);
            }
            catch (Exception ex)
            {
                if(!(ex is ShowHelpException))
                {
                    Console.Error.WriteLine(ex.Message);
                }

                settings.PrintHelp();

                if(!(ex is ShowHelpException))
                {
                    Console.WriteLine("Press enter to exit");
                    Console.ReadLine();
                }

                return;
            }

            var print = settings.GetPrintSettings();

            if (print.PrintSetupInfo)
            {
                Console.WriteLine("=== Initializing blazon convertor with: ");
                Console.WriteLine(" Language   : " + settings.Language);
                Console.WriteLine(" InputFile  : " + Path.GetFullPath(settings.InputFile));
                Console.WriteLine(" OutputFile : " + Path.GetFullPath(settings.OutputFile));
                Console.WriteLine(" RenderType : " + settings.RenderType);
            }
            if (print.PrintVocabularyLoadProgress)
            {
                Console.WriteLine("\n=== Loading blazon vocabulary from " + settings.Language);
            }

            BlazonVocabulary vocabulary;
            string input;
            try
            {
                var blazonDirectory = ".\\resources\\" + settings.Language + "\\";
                vocabulary = (new VocabularyLoader(blazonDirectory) { PrintSettings = settings.GetPrintSettings() }).Load();
                input = File.ReadAllText(settings.InputFile);
            }
            catch(IOException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return;
            }

            Stream stream = File.Open(settings.OutputFile, FileMode.Create);

            var renderer = RendererByType(stream, settings.RenderType, vocabulary);
            renderer.CloseWhenDone = true;

            try
            {
                ParseProcess.Begin(input, settings)
                .Then(new LexAnalyzer(vocabulary, settings.GetPrintSettings()), "Lexical analysis")
                .Then(new SyntacticAnalyzer(), "Syntactic analysis")
                .Then(renderer, "Rendering")
                .Then(result =>
                {
                    if (result.Success)
                    {
                        if (print.PrintResult)
                        {
                            Console.WriteLine("Rendition to file '{0}' finished successfully.",
                                Path.GetFullPath(settings.OutputFile));
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Error occurred during rendition: " + result.Error);
                    }
                });


            }
            catch (UnexpectedTokenException ex)
            {
                Console.Error.WriteLine(ex.Message + "\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(input.Substring(0, ex.TokenPosition));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(input.Substring(ex.TokenPosition, ex.TokenText.Length));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(input.Substring(ex.TokenPosition + ex.TokenText.Length));
            }

            if (print.PromptExit)
            {
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
        }

        static CrestRenderer RendererByType(Stream stream, RenderType type, BlazonVocabulary vocabulary)
        {
            switch (type)
            {
                case RenderType.Svg: return new SvgRenderer(stream);
                case RenderType.Text: return new TextRenderer(vocabulary.GetDefiner(), stream);
            }

            throw new ArgumentException("Render type " + type.ToString() + " is not supported yet");
        }
    }
}
