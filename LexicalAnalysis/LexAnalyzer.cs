using Heraldry.App;
using Heraldry.Blazon;
using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Heraldry.LexicalAnalysis
{
    public class LexAnalyzer : ParseStep<string, List<Token>>
    {
        private DebugPrinter debug;

        private BlazonVocabulary BlazonVocabulary { get; }

        public LexAnalyzer(BlazonVocabulary blazonVocabulary)
        {
            this.BlazonVocabulary = blazonVocabulary;
            this.debug = new DebugPrinter();
        }

        public override List<Token> Execute(string input)
        {
            List<Token> tokens = new List<Token>();

            input = NormalizeInput(input);
            debug.Print("Input text to be scanned:", input);


            var separators = SepareSeparators(input, out input);
            tokens.AddRange(separators);

            var numbers = ParseNumberTokens(input, out input);
            tokens.AddRange(numbers);

            var definedTokens = FindDefinedTokens(input, out input);
            tokens.AddRange(definedTokens);

            var charges = CollectRemainingTokensAsCharges(input, out input);
            tokens.AddRange(charges);


            debug.PrintSeparator();
            debug.Print("Unprocessed text:\n", input);

            tokens = tokens.OrderBy(t => t.Position).ToList();

            debug.PrintTokens("Token text:", tokens);
            
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

        private Token CreateToken(IDefinition def, int index)
        {
            switch (def.TokenType)
            {
                case DefinitionType.Tincture:
                    TinctureDefinition tdef = (TinctureDefinition)def;
                    return new Token(index, def);
            }

            return new Token(index, def);
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

                var def = new SeparatorDefinition(StringToSeparator(match.Captures[0].Value)) { Text = match.Value };
                separators.Add(new Token(match.Index, def));
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

            Number number;
            while ((number = BlazonVocabulary.NumberVocabulary.FindInText(text, out int position, out int length)) != null)
            {
                var definition = new NumberDefinition(number) { Text = text.Substring(position, length) };
                var numTok = new Token(position, definition);
                tokens.Add(numTok);

                text = text.Remove(position, length)
                           .Insert(position, "".PadLeft(length));
            }

            output = text;
            return tokens;
        }


        private List<Token> FindDefinedTokens(String input, out String output)
        {
            var definitions = BlazonVocabulary.GetAllDefinitions(true);
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
                tokens.Add(CreateToken(new ChargeDefinition<GenericCharge>(new GenericCharge(chargeText)) { Text = chargeText }, match.Index));
            }

            output = input;
            return tokens;
        }
    }
}
