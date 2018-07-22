using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Program
{
    class TranslationStats
    {
        public DateTime ProcessStart { get; set; }

        public uint LexicalAnalysisDuration { get; set; }

        public uint SyntacticAnalysisDuration { get; set; }

        public uint RenderDuration { get; set; }


        public uint LexicalTokens { get; set; }
    }
}
