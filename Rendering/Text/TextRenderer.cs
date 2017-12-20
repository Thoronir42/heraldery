using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Structure;
using System.IO;
using Heraldry.Rendering.Text.Printers;
using Heraldry.Blazon.Vocabulary;

namespace Heraldry.Rendering.Text
{
    class TextRenderer : CrestRenderer
    {
        private readonly VocabularyDefiner definer;

        public TextRenderer(VocabularyDefiner definer)
        {
            this.definer = definer;
        }

        public override bool Render(BlazonInstance instance, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                RootPrinter printer = new RootPrinter(writer, definer);
                printer.Print(instance);
            }

            return true;
        }
    }
}
