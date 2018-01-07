using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    public class ParseProcess : ParseProcess<object>
    {
        public ParseProcess(object val) : base(val)
        {

        }

        public static ParseProcess<T> Begin<T>(T input)
        {
            return new ParseProcess<T>(input);
        }
    }

    public class ParseProcess<ValueType>
    {
        public ValueType Value { get; }
        Boolean Verbose { get; set; }


        public ParseProcess(ValueType value, Boolean verbose = false)
        {
            this.Value = value;
            this.Verbose = Verbose;
        }

        public ParseProcess<ValueType> Pause()
        {
            Console.WriteLine("Paused.");
            Console.ReadLine();

            return this;
        }

        public ParseProcess<OutType> Then<OutType>(ParseStep<ValueType, OutType> step, string label = null)
        {
            if(this.Verbose && label != null)
            {
                Console.WriteLine("\n==== " + label);
            }

            return new ParseProcess<OutType>(step.Execute(this.Value)) { Verbose = this.Verbose };
        }

        public void Then(Action<ValueType> action)
        {
            action(this.Value);
        }
    }
}
