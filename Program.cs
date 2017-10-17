using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldery {

    using LexAnalyzer;
    using SyntAnalyzer;
    using CrestGenerator;

    class Program {
        static void Main(string[] args) {
            if(args.Length <= 0) {
                Console.Out.WriteLine("No parameters :(");
                //Environment.Exit(0);
            } else {
                string input = Console.In.ReadLine();
                Console.Out.WriteLine("Processing parameters . . .");

                //NOW I WILL TRY TO FORESEE THE FUTURE OF OUR APPLICATION
                //BEHOLD
                LexAnalyzer lex = new LexAnalyzer();
                lex.AnalyzeStuff(input);

                SyntAnalyzer synt = new SyntAnalyzer();
                synt.AnalyzeStuff(input);

                CrestGenerator gen = new CrestGenerator();
                gen.CreateCrest();
            }
        }
    }
}

namespace LexAnalyzer {
    //this should be moved to its own project eventually
    class LexAnalyzer {
        public void AnalyzeStuff(string input) { }
    }
}

namespace SyntAnalyzer {
    //this should be moved to its own project eventually
    class SyntAnalyzer {
        public void AnalyzeStuff(string input) { }
    }
}

namespace CrestGenerator {
    //this should be moved to its own project eventually
    class CrestGenerator {
        public void CreateCrest() { }
    }
}
