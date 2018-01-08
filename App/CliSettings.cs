using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Rendering;

namespace Heraldry
{
    public class CliSettings
    {
        private string outputFile = null;

        public string Language { get; set; }
        public string InputFile { get; set; }
        public RenderType RenderType { get; set; } = RenderType.Text;

        public bool Silent { get; set; } = false;
        public bool Verbose { get; set; } = false;
        public bool PromtExit { get; set; } = true;

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

                if (filename + '.' + ext != InputFile)
                {
                    return filename + '.' + ext;
                }
                else
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

        public CliSettings(params string[] args) : this()
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
                    case "-l":
                        this.Language = GetString(args, ++i);
                        break;
                    case "-r":
                        this.RenderType = (RenderType)Enum.Parse(typeof(RenderType), GetString(args, ++i));
                        break;
                    case "--verbose":
                    case "-v":
                        this.Verbose = true;
                        break;
                    case "--silent":
                    case "-s":
                        this.Silent = true;
                        break;
                    case "--no-prompt":
                        this.PromtExit = false;
                        break;
                    default:
                        if (n == 0)
                        {
                            this.InputFile = args[i];
                        }
                        if (n == 1)
                        {
                            this.OutputFile = args[i];
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

        public PrintSettings GetPrintSettings()
        {
            return new PrintSettings()
            {
                PrintSetupInfo = !Silent,
                PrintVocabularyLoadProgress = Verbose & !Silent,
                PrintLexTokens = Verbose & !Silent,
                PrintStepLabels = Verbose & !Silent,
                PrintResult = !Silent,
                PromptExit = PromtExit,
            };
        }
    }

    public class PrintSettings
    {

        public bool PrintSetupInfo { get; set; }

        public bool PrintVocabularyLoadProgress { get; set; }

        public bool PrintLexTokens { get; set; }

        public bool PrintStepLabels { get; set; }

        public bool PrintResult { get; set; }

        public bool PromptExit { get; set; }

        internal PrintSettings()
        {

        }
    }
}
