using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    public class ParseProcess : ParseProcess<object>
    {
        public ParseProcess(object val, CliSettings settings) : base(val, settings)
        {
        }

        public static ParseProcess<T> Begin<T>(T input, CliSettings settings)
        {
            return new ParseProcess<T>(input, settings);
        }

        public abstract class Step<InType, OutType>
        {
            public abstract OutType Execute(InType input);
        }

        public class Result
        {
            public bool Success { get; }
            public string Error { get; }

            public Result(bool success)
            {
                Success = success;
            }

            public Result(string error) : this(false)
            {
                Error = error;
            }
        }
    }

    public class ParseProcess<ValueType>
    {
        protected CliSettings settings;

        public ValueType Value { get; }



        public ParseProcess(ValueType value, CliSettings settings)
        {
            this.Value = value;
            this.settings = settings;
        }

        public ParseProcess<ValueType> Pause()
        {
            Console.WriteLine("Paused.");
            Console.ReadLine();

            return this;
        }

        public ParseProcess<OutType> Then<OutType>(ParseProcess.Step<ValueType, OutType> step, string label = null)
        {
            if (label != null && settings.GetPrintSettings().PrintStepLabels)
            {
                Console.WriteLine("\n==== " + label);
            }

            return new ParseProcess<OutType>(step.Execute(this.Value), this.settings);
        }

        public void Then(Action<ValueType> action)
        {
            action(this.Value);
        }
    }
}
