using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Rendering;

namespace Heraldry.CLI
{
    class CliSettings
    {
        private string outputFile = null;

        public string Language { get; set; } = "en_olde";
        public string InputFile { get; set; } = "Arms of Churchil.txt";
        public RenderType RenderType { get; set; } = RenderType.Svg;

        public string OutputFile
        {
            get
            {
                if(outputFile == null)
                {
                    // return input file with appropriate file extension
                }
                return outputFile;
            }
            set
            {
                outputFile = value;
            }
        }


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
