using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Api
{
    class ErrorResponse
    {
        public String RequestId { get; set; }

        public String ErrorMessage { get; set; }

        public List<Token> Tokens { get; set; }
    }
}
