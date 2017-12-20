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
        public StreamWriter Writer { get; }
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
    }
}
