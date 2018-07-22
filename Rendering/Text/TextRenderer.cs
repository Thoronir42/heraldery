using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Structure;
using System.IO;
using Heraldry.Rendering.Text.Printers;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Cli;

namespace Heraldry.Rendering.Text
{
    public class TextRenderer : CrestRenderer
    {
        private readonly VocabularyDefiner definer;

        public TextRenderer(VocabularyDefiner definer, Stream printStream) : base(printStream)
        {
            this.definer = definer;
        }

        public override ParseProcess.Result Render(BlazonInstance instance, Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);

            RootPrinter printer = new RootPrinter(writer, definer);
            printer.Print(instance);

            writer.WriteLine();
            writer.Flush();

            return new ParseProcess.Result(true);
        }
    }
}
