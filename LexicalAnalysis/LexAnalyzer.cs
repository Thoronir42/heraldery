using Heraldry.Blazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    class LexAnalyzer
    {
        private BlazonDefinition BlazonDefinition { get; }

        public LexAnalyzer(BlazonDefinition blazonDefinition)
        {
            BlazonDefinition = blazonDefinition;
        }

        public List<Token> ParseText(string input) {
            List<Token> list = new List<Token>();

            return list;
        }
    }
}
