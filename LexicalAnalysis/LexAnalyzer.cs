﻿using Heraldry.Blazon;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Heraldry.LexicalAnalysis
{
    class LexAnalyzer
    {
        private BlazonVocabulary BlazonVocabulary { get; }
        private NumberParser NumberParser { get; }

        public LexAnalyzer(BlazonVocabulary blazonVocabulary, NumberParser numberParser)
        {
            this.BlazonVocabulary = blazonVocabulary;
            this.NumberParser = numberParser;
        }

        public List<Token> ParseText(string input)
        {
            List<Token> tokens = new List<Token>();

            input = NormalizeInput(input);
            Console.WriteLine("Input text to be scanned:\n" + input);

            var separatos = SepareSeparators(input, out input);
            tokens.AddRange(separatos);

            var numbers = ParseNumberTokens(input, out input);
            tokens.AddRange(numbers);

            List<Definition> definitions = BlazonVocabulary.GetAllDefinitions(true);
            foreach (var def in definitions)
            {
                var search = " " + def.Text + " ";
                int i = 0;
                while ((i = input.IndexOf(search)) != -1)
                {
                    input = input.Remove(i + 1, def.Text.Length).Insert(i + 1, "".PadRight(def.Text.Length, ' '));
                    tokens.Add(CreateToken(def, i + 1));
                }
            }

            Console.WriteLine("".PadRight(12, '-'));
            Console.WriteLine("Unprocessed text:\n" + input);

            tokens = tokens.OrderBy(t => t.Position).ToList();

            String tokenText = "".PadRight(input.Length, ' ');
            foreach (var t in tokens)
            {
                tokenText = tokenText.Remove(t.Position, t.Definition.Text.Length).Insert(t.Position, t.Definition.Text);
            }
            Console.WriteLine("".PadRight(12, '-'));
            Console.WriteLine("Token text:\n" + tokenText);

            return tokens;
        }

        /// <summary>
        /// Transforms input to lowercase
        /// Removes trailing dot if present
        /// Wraps input in spaces
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string NormalizeInput(string input)
        {
            input = input.ToLower();
            return " " + input + " ";
        }

        private Token CreateToken(Definition def, int index)
        {
            switch (def.GetTokenType())
            {
                case DefinitionType.Tincture:
                    TinctureDefinition tdef = (TinctureDefinition)def;
                    return new Token() { Definition = def, Position = index };
            }

            return new Token() { Definition = def, Position = index };
        }

        private List<Token> SepareSeparators(String input, out String output)
        {
            var separators = new List<Token>();
            String text = input;

            var separatorRegex = "(\\.|,|;)";

            Match match;
            var i = 0;
            while ((match = Regex.Match(text, separatorRegex)).Success)
            {
                var def = new SeparatorDefinition() { Text = match.Value };
                separators.Add(new Token() { Definition = def, Position = match.Index });
                text = text.Remove(match.Index, match.Value.Length)
                           .Insert(match.Index, "".PadRight(match.Value.Length));
                if (++i > 10)
                {
                    break;
                }
            }

            output = text;
            return separators;
        }

        private List<Token> ParseNumberTokens(String input, out String output)
        {
            List<Token> tokens = new List<Token>();
            String text = input;

            Token numTok;
            while ((numTok = NumberParser.FindNumber(text)) != null)
            {
                text = text.Remove(numTok.Position, numTok.Definition.Text.Length)
                           .Insert(numTok.Position, "".PadLeft(numTok.Definition.Text.Length));
                tokens.Add(numTok);
            }

            output = text;
            return tokens;
        }
    }
}
