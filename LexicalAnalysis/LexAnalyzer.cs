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
    public class LexAnalyzer : ParseProcess.Step<string, List<Token>>
    {
        private BlazonVocabulary BlazonVocabulary { get; }

        private PrintSettings printSettings;

        public LexAnalyzer(BlazonVocabulary blazonVocabulary, PrintSettings settings = null)
        {
            this.BlazonVocabulary = blazonVocabulary;

            this.printSettings = settings ?? new PrintSettings();
        }

        delegate List<Token> InputEditFunction<T>(T input, out T output);

        public override List<Token> Execute(string input)
        {
            DebugPrinter debug = new DebugPrinter();

            List<Token> tokens = new List<Token>();

            input = NormalizeInput(input);
            if (printSettings.PrintLexTokens)
                debug.PrintText("Input text to be scanned:", input);



            InputEditFunction<string>[] tokenizerFunctions = {
                SepareSeparators,
                ParseNumberTokens,
                FindDefinedTokens,
                CaptureComments,
                CollectRemainingTokensAsCharges,
            };

            foreach (var func in tokenizerFunctions)
            {
                string output;

                List<Token> found = func(input, out output);
                tokens.AddRange(found);

                input = output;
            }

            tokens = tokens.OrderBy(t => t.Position).ToList();

            if (printSettings.PrintLexTokens)
            {
                debug.PrintSeparator();
                debug.PrintText("Unprocessed text:\n", input);
                debug.PrintSeparator();
                debug.PrintTokens("Token text:", tokens);
                debug.PrintSeparator();
            }

            

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



        private List<Token> SepareSeparators(String input, out String output)
        {
            var separators = new List<Token>();
            String text = input;

            var separatorRegex = "(\\.|,|:|;)";

            Match match;

            while ((match = Regex.Match(text, separatorRegex)).Success)
            {
                var def = new SeparatorDefinition(BlazonVocabulary.Separator(match.Captures[0].Value)) { Text = match.Value };
                separators.Add(new Token(match.Index, def));
                text = text.Remove(match.Index, match.Value.Length)
                           .Insert(match.Index, "".PadRight(match.Value.Length));
            }

            output = text;
            return separators;
        }

        private List<Token> CaptureComments(String input, out String output)
        {
            string commentRegexp = "\\( ?(\\w+) ?\\)";

            var separators = new List<Token>();
            String text = input;

            Match match;

            while ((match = Regex.Match(text, commentRegexp)).Success)
            {

                var def = new CommentDefinition(match.Groups[1].Value) { Text = match.Value };
                separators.Add(new Token(match.Index, def));
                text = text.Remove(match.Index, match.Value.Length)
                           .Insert(match.Index, "".PadRight(match.Value.Length));
            }

            output = text;
            return separators;
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
                    tokens.Add(new Token(i + 1, def));
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
                var definition = new ChargeDefinition(new GenericCharge(chargeText)) { Text = chargeText };

                tokens.Add(new Token(match.Index, definition));

                input = input.Remove(match.Index, match.Length).Insert(match.Index, "".PadRight(match.Length, ' '));
            }

            output = input;
            return tokens;
        }
    }
}
