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
    public class RootPrinter
    {
        private int symbolsWritten = 0;

        private StreamWriter Writer { get; }

        public VocabularyDefiner Define { get; }

        public FieldPrinter Field { get; }
        public FillingPrinter Filling { get; }
        public ChargePrinter Charge { get; }

        public RootPrinter(StreamWriter writer, VocabularyDefiner definer)
        {
            Writer = writer;
            Define = definer;

            Field = new FieldPrinter(this);
            Filling = new FillingPrinter(this);
            Charge = new ChargePrinter(this);
        }

        public void Print(BlazonInstance blazon)
        {
            Field.P(blazon.CoatOfArms.Content);

            Write(Separator.Dot, SpaceRule.Never);
        }

        internal void Write(KeyWord keyword) => this.Write(Define.Keyword(keyword));
        internal void Write(Separator separator, SpaceRule space = SpaceRule.Auto) => this.Write(Define.Separator(separator), space);

        internal void Format(string format, params string[] args)
        {
            Write(String.Format(format, args));
        }

        [Obsolete("Use Write(string) if possible")]
        internal void Write(object o)
        {
            Write(o.ToString());
        }

        internal void Write(string s, SpaceRule space = SpaceRule.Auto)
        {
            if ((space == SpaceRule.Auto && symbolsWritten > 0) || space == SpaceRule.Always)
            {
                this.Writer.Write(" ");
            }
            this.Writer.Write(s);
            symbolsWritten++;
        }

        internal void WriteList(List<string> list)
        {
            int length = list.Count;
            for (int i = 0; i < length; i++)
            {
                Write(list[i]);
                int remainingItems = length - (i + 1);
                if(remainingItems > 1)
                {
                    Write(Separator.Comma, SpaceRule.Never);
                } else if (remainingItems == 1)
                {
                    Write(KeyWord.And);
                }
            }
        }
    }

    enum SpaceRule
    {
        Auto, Never, Always,
    }
}
