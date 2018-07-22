using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Api
{
    class Request
    {
        public String RequestId { get; set; }

        public DateTime Issued { get; set; }

        public String Text { get; set; }
    }
}
