using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Rendering;

namespace Heraldry.CLI
{
    public class CliSettings
    {
        private string outputFile = null;

        public string Language { get; set; }
        public string InputFile { get; set; }
        public RenderType RenderType { get; set; } = RenderType.Text;
        public Boolean Verbose { get; set; } = false;

        public string OutputFile
        {
            get
            {
                if (outputFile != null)
                {
                    return outputFile;
                }

                // return input file with appropriate file extension
                int idx = InputFile.LastIndexOf('.');
                String filename = InputFile.Substring(0, idx);
                String ext = Extension(RenderType);
                
                if(filename + '.' + ext != InputFile)
                {
                    return filename + '.' + ext;
                } else
                {
                    return filename + "-out." + ext;
                }
            }
            set
            {
                outputFile = value;
            }
        }


        public CliSettings()
        {

        }

        public CliSettings(params string[] args)
        {
            this.ProcessArguments(args);
        }

        public void ProcessArguments(string[] args)
        {
            int i = 0;
            int n = 0;
            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-o":
                        this.OutputFile = GetString(args, ++i);
                        break;
                    case "-l":
                        this.Language = GetString(args, ++i);
                        break;
                    case "-r":
                        this.RenderType = (RenderType)Enum.Parse(typeof(RenderType), GetString(args, ++i));
                        break;
                    case "-v":
                        this.Verbose = true;
                        break;
                    default:
                        if (n == 0)
                        {
                            this.InputFile = args[i];
                        }
                        n++;
                        break;
                }

                i++;
            }
        }

        private String GetString(string[] args, int i)
        {
            if (i < 0 || i >= args.Length)
            {
                return null;
            }
            return args[i];
        }

        private String Extension(RenderType type)
        {
            switch (type)
            {
                case RenderType.Svg:
                    return "svg";
                default:
                case RenderType.Text:
                    return "txt";
            }

        }
    }
}
