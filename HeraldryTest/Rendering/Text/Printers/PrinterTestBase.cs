using Heraldry.Rendering.Text.Printers;
using HeraldryTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.Rendering.Text.Printers
{
    public class PrinterTestBase
    {
        private MemoryStream stream;

        [TestInitialize]
        public virtual void Initialize()
        {
            stream = new MemoryStream();
        }

        protected Stream GetStream()
        {
            return stream;
        }

        protected string GetPrintedText()
        {
            var oldPosition = stream.Position;

            stream.Flush();

            stream.Position = 0;
            var result = (new StreamReader(stream)).ReadToEnd();

            stream.Position = oldPosition;

            return result;
        }

        // fixme: again, not actual moc, wahhh
        protected RootPrinter MockRoot()
        {
            var writer = new StreamWriter(GetStream()) { AutoFlush = true };
            return new RootPrinter(writer, MockVocabulary.Get().GetDefiner());
        }

    }
}
