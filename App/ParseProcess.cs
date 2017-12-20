using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.App
{
    public class ParseProcess<ValueType>
    {
        public ValueType Value { get; }
        Boolean Verbose { get; set; }


        public ParseProcess(ValueType value)
        {
            this.Value = value;
        }

        public ParseProcess<ValueType> Pause()
        {
            Console.WriteLine("Paused.");
            Console.ReadLine();

            return this;
        }

        public ParseProcess<OutType> Then<OutType>(ParseStep<ValueType, OutType> step)
        {
            return new ParseProcess<OutType>(step.Execute(this.Value)) { Verbose = this.Verbose };
        }

        public ParseProcess<OutType> Then<OutType>(ParseStep<ValueType, OutType> step, string label)
        {
            if(this.Verbose)
            {
                Console.WriteLine("\n==== " + label);
            }

            return this.Then(step);
        }
    }
}
