using Heraldry.Blazon;
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
    public class LexAnalyzer
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
            DebugPrinter.Print("Input text to be scanned:", input);

            var separatos = SepareSeparators(input, out input);
            tokens.AddRange(separatos);

            var numbers = ParseNumberTokens(input, out input);
            tokens.AddRange(numbers);

            var definedTokens = FindDefinedTokens(input, out input);
            tokens.AddRange(definedTokens);

            var charges = CollectRemainingTokensAsCharges(input, out input);
            tokens.AddRange(charges);


            DebugPrinter.PrintSeparator();
            DebugPrinter.Print("Unprocessed text:\n", input);

            tokens = tokens.OrderBy(t => t.Position).ToList();

            DebugPrinter.PrintTokens("Token text:", tokens);
            
            // tokens are found on space-padded input - align back to original text
            foreach (var token in tokens)
            {
                token.Position--;
            }

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
            //input = input.ToLower();
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

                var def = new SeparatorDefinition() { Text = match.Value, Separator = StringToSeparator(match.Captures[0].Value) };
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

        private Separator StringToSeparator(String s)
        {
            switch (s)
            {
                case ".": return Separator.Dot;
                case ",": return Separator.Comma;
                case ";": return Separator.Semicolon;
            }

            return Separator.Other;
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


        private List<Token> FindDefinedTokens(String input, out String output)
        {
            List<Definition> definitions = BlazonVocabulary.GetAllDefinitions(true);
            List<Token> tokens = new List<Token>();

            foreach (var def in definitions)
            {
                var search = " " + def.Text + " ";
                int i = 0;
                while ((i = input.IndexOf(search, StringComparison.CurrentCultureIgnoreCase)) != -1)
                {
                    input = input.Remove(i + 1, def.Text.Length).Insert(i + 1, "".PadRight(def.Text.Length, ' '));
                    tokens.Add(CreateToken(def, i + 1));
                }
            }

            output = input;
            return tokens;
        }

        private List<Token> CollectRemainingTokensAsCharges(String input, out String output)
        {
            List<Token> tokens = new List<Token>();

            Regex pattern = new Regex("([-\\w]+( [\\w]+)?)");


            Match match;
            while ((match = pattern.Match(input)).Success)
            {
                var chargeText = match.Captures[0].Value;

                input = input.Remove(match.Index, match.Length).Insert(match.Index, "".PadRight(match.Length, ' '));
                tokens.Add(CreateToken(new ChargeDefinition() { Text = chargeText }, match.Index));
            }

            output = input;
            return tokens;
        }
    }
}
