using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    public class CommandLineInterface
    {
        private List<CliOption> options = new List<CliOption>();

        private int requiredParameters = 0;
        private List<CliParam> parameters = new List<CliParam>();

        public CommandLineInterface()
        {
            this.Option("help", 0, args => PrintHelp());
        }

        public CommandLineInterface Option(string name, string shortcut, int argc, Action<string[]> action,
            string help = null)
        {
            this.options.Add(new CliOption(name, argc, action) { Shortcut = shortcut, Help = help });
            return this;
        }

        public CommandLineInterface Option(string name, int argc, Action<string[]> action,
            string help = null)
        {
            this.options.Add(new CliOption(name, argc, action) { Help = help });
            return this;
        }

        public CommandLineInterface Param(string name, Action<string> action, bool required = false)
        {
            if(required)
            {
                if(requiredParameters < parameters.Count)
                {
                    throw new ArgumentException("Can not define required parameters after optional");
                }

                requiredParameters++;
            }

            this.parameters.Add(new CliParam(name, action, required));
            return this;
        }

        public void Execute(string[] args)
        {
            int i = 0;
            int n = 0;
            while (i < args.Length)
            {
                var option = options.Find(opt => opt.IsMatch(args[i]));
                if(option != null) {
                    option.Action(args.Skip(i + 1).Take(option.Argc).ToArray());
                    i += option.Argc + 1;
                    continue;
                }

                parameters[n].Action(args[i]);
                i++;
                n++;
            }

            if(n < parameters.Count && parameters[n].Required)
            {
                throw new ArgumentException(String.Format("Not enough parameters specified. {0} given out of {1} required", n, requiredParameters));
            }
        }

        internal void PrintHelp()
        {
            string paramString = String.Join(" ", parameters.Select(param => param.Required ? param.Name : "[" + param.Name + "]"));
            string usage = String.Format("usage: \n {0} {1}",
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, paramString);

            Console.WriteLine(usage);
            foreach (var opt in options)
            {
                Console.WriteLine("\t--{0,12} -{1} {2}", opt.Name, opt.Shortcut, opt.Help);
            }
        }

        private class CliOption
        {
            public string Name { get; }
            public string Shortcut { get; set; }
            public string Help { get; set; }

            public int Argc { get; }

            public Action<string[]> Action { get; }

            public CliOption(string name, int argc, Action<string[]> action)
            {
                this.Name = name;
                this.Argc = argc;
                this.Action = action;
            }

            public bool IsMatch(string s)
            {
                return s == "--" + Name || s == "-" + Shortcut;
            }

        }

        private class CliParam {

            public string Name { get; }
            public Action<string> Action { get; }

            public bool Required { get; }

            public CliParam(string name, Action<string> action, bool required)
            {
                Name = name;
                Action = action;
                Required = required;
            }
        }
    }


}
