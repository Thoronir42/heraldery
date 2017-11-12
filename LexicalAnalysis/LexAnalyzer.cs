using Heraldry.Blazon;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    class LexAnalyzer
    {
        private BlazonVocabulary BlazonVocabulary { get; }

        public LexAnalyzer(BlazonVocabulary blazonVocabulary)
        {
            BlazonVocabulary = blazonVocabulary;
        }

        public List<Token> ParseText(string input) {
            List<Definition> definitions = BlazonVocabulary.GetAllDefinitions(true);

            /*foreach(var d in definitions)
            {
                Console.WriteLine(d.Text.PadRight(30, ' ') + d.GetType().Name);
            }*/

            input = input.ToLower();
            Console.WriteLine("Input text to be scanned:\n" + input);

            List<Token> tokens = new List<Token>();
            foreach (var def in definitions)
            {
                int i = 0;
                while((i = input.IndexOf(def.Text)) != -1)
                {
                    input = input.Remove(i, def.Text.Length).Insert(i, "".PadRight(def.Text.Length, ' '));
                    tokens.Add(CreateToken(def, i));
                }
            }

            Console.WriteLine("".PadRight(12, '-'));
            Console.WriteLine("Unprocessed text:\n" + input);

            tokens = tokens.OrderBy(t => t.Position).ToList();

            String tokenText = "".PadRight(input.Length, ' ');
            foreach(var t in tokens)
            {
                tokenText = tokenText.Remove(t.Position, t.Definition.Text.Length).Insert(t.Position, t.Definition.Text);
            }
            Console.WriteLine("".PadRight(12, '-'));
            Console.WriteLine("Token text:\n" + tokenText);

            return tokens;
        }

        private Token CreateToken(Definition def, int index)
        {
            switch(def.GetTokenType())
            {
                case DefinitionType.Tincture:
                    TinctureDefinition tdef = (TinctureDefinition)def;
                    return new Token() { Definition = def, Position = index };
            }

            return new Token() { Definition = def, Position = index };
        }
    }
}
