using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Rendering.Text.Printers
{
    class RootPrinter
    {
        private int symbolsWritten = 0;

        private StreamWriter Writer { get; }

        public VocabularyDefiner Define { get; }

        public FieldPrinter Field { get; }
        public FillingPrinter Filling { get; }

        public RootPrinter(StreamWriter writer, VocabularyDefiner definer)
        {
            Writer = writer;
            Define = definer;

            Field = new FieldPrinter(this);
            Filling = new FillingPrinter(this);
        }

        public void Print(BlazonInstance blazon)
        {
            Field.P(blazon.CoatOfArms.Content);
        }

        internal void Print(KeyWord keyword) => this.Write(Define.Keyword(keyword));

        internal void Write(object o)
        {
            Write(o.ToString());
        }

        internal void Write(String s)
        {
            if (symbolsWritten > 0)
            {
                this.Writer.Write(" ");
            }
            this.Writer.Write(s);
            symbolsWritten++;
        }
    }
}
