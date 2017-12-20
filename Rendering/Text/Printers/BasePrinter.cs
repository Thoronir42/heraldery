using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    abstract class BasePrinter<T>
    {
        protected RootPrinter Print { get; }

        public BasePrinter(RootPrinter printer)
        {
            this.Print = printer;
        }

        public abstract void P(T item);
    }
}
