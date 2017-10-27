using Heraldry.Blazon;
using Heraldry.Blazon.Vocabulary;
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
            List<Token> list = new List<Token>();

            return list;
        }
    }
}
