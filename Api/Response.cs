using Heraldry.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Api
{
    class Response
    {
        public String RequestId { get; set; }

        public List<ApiToken> Tokens { get; set; } = new List<ApiToken>();

        public TranslationStats Result {get;set;}

        public String ResultUrl { get; set; }
    }
}
