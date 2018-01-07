﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
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
        private static CliSettings ProcesArgs(params string[] args)
        {
            CliSettings settings = new CliSettings(args);

            // todo: remove debug settings initialization
            settings = new CliSettings("-v", "-l", "en_olde", 
                "-r", "Text", ".\\resources\\input\\Czech-SimpleSilesia.txt", ".\\..\\..\\out\\Czech-SimpleSilesia-rendered.txt");

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
            BlazonVocabulary vocabulary = VocabularyLoader.LoadFromDirectory(".\\resources\\" + settings.Language + "\\");


            string input = File.ReadAllText(settings.InputFile);

            Stream stream = File.Open(settings.OutputFile, FileMode.OpenOrCreate);
            //MemoryStream stream = new MemoryStream();

            var renderer = RendererByType(stream, settings.RenderType, vocabulary);

            try
            {
                ParseProcess.Begin(input)
                .Then(new LexAnalyzer(vocabulary), "Lexical analysis")
                .Then(new SyntacticAnalyzer(), "Syntactic analysis")
                .Then(renderer, "Rendering")
                .Then(result =>
                {
                    if (result)
                    {
                        Console.WriteLine("Rendition finished successfully.");

                        //stream.Position = 0;
                        //(new LexAnalyzer(vocabulary)).Execute((new StreamReader(stream).ReadToEnd()));
                    }
                    else
                    {
                        Console.WriteLine("Error occurred during rendition");
                    }
                });

                
            }
            catch (UnexpectedTokenException ex)
            {
                Console.Error.WriteLine(ex.Message);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(input.Substring(0, ex.TokenPosition));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(input.Substring(ex.TokenPosition, ex.TokenText.Length));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(input.Substring(ex.TokenPosition + ex.TokenText.Length));
            }


            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
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
