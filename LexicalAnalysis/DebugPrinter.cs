using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    internal class DebugPrinter
    {
        internal void Print(string label, string text)
        {
            Console.WriteLine(label + "\n" + text);
        }
        internal void PrintSeparator()
        {
            Console.WriteLine("".PadRight(12, '-'));
        }

        internal void PrintTokens(string label, List<Token> tokens)
        {
            Console.WriteLine(label);
            ConsoleColor originalFront = Console.ForegroundColor;

            int lastPosition = 0;
            foreach (var t in tokens)
            {
                Console.ForegroundColor = ColorByType(t.Type);
                Console.Write("".PadLeft(t.Position - lastPosition));
                Console.Write(t.Definition.Text);
                lastPosition = t.Position + t.Definition.Text.Length;

                //tokenText = tokenText.Remove(t.Position, t.Definition.Text.Length).Insert(t.Position, t.Definition.Text);
            }

            Console.ForegroundColor = originalFront;
            Console.WriteLine();
        }

        static private ConsoleColor ColorByType(DefinitionType type)
        {
            switch(type)
            {
                case DefinitionType.Ordinary:
                case DefinitionType.Charge:
                    return ConsoleColor.Green;

                case DefinitionType.Subordinary:
                    return ConsoleColor.Red;

                case DefinitionType.FieldDivision:
                    return ConsoleColor.Blue;

                case DefinitionType.Number:
                    return ConsoleColor.Yellow;

                case DefinitionType.Tincture:
                    return ConsoleColor.Magenta;

                case DefinitionType.Separator:
                    return ConsoleColor.Gray;
                case DefinitionType.KeyWord:
                    return ConsoleColor.White;

                case DefinitionType.Position:
                    return ConsoleColor.DarkYellow;
                    
            }

            return ConsoleColor.White;
        }
    }
}
