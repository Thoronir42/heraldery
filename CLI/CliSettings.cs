using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.CLI
{
    class CliSettings
    {
        public string Language { get; set; } = "en_olde";
        public string InputFile { get; set; } = "Arms of Churchil.txt";

        public CliSettings()
        {

        }

        public CliSettings(string[] args)
        {
            this.ProcessArguments(args);
        }

        public void ProcessArguments(string[] args)
        {
            // todo: Todd, process arguments please!
        }
    }
}
