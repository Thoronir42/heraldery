using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Rendering;

namespace Heraldry.Cli
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

        private CommandLineInterface cli;

        public CliSettings()
        {
            this.cli = new CommandLineInterface()
                .Option("language", "l", 1, args => Language = args[0], "sets language")
                .Option("render", "r", 1, args => RenderType = (RenderType)Enum.Parse(typeof(RenderType), args[0]), "sets renderer")
                .Option("verbose", "v", 0, args => Verbose = true, "verbose output")
                .Option("silent", "s", 0, args => Silent = true, "mutes output")
                .Option("no-prompt", 0, args => PromtExit = false, "disables exit prompts")
                .Param("Input file", param => InputFile = param, true)
                .Param("Output file", param => OutputFile = param);
        }

        public void PrintHelp()
        {
            this.cli.PrintHelp();
        }

        public CliSettings(params string[] args) : this()
        {
            this.ProcessArguments(args);
        }

        public void ProcessArguments(params string[] args)
        {
            this.cli.Execute(args);
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
