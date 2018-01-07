using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    public abstract class BasePrinter<T>
    {
        protected RootPrinter Print { get; }
        protected VocabularyDefiner Define { get { return Print.Define; } }

        public BasePrinter(RootPrinter printer)
        {
            this.Print = printer;
        }

        public abstract void P(T item);
    }
}
