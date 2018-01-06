using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public class SequenceLink
    {
        private RootCompiler Compilers { get; }

        private readonly SequenceLink parent;
        private readonly Action compileAction;

        public SequenceLink(RootCompiler root)
        {
            this.Compilers = root;
            this.compileAction = () => { };
        }

        private SequenceLink(RootCompiler root, SequenceLink parent, Action compileAction) : this(root)
        {
            this.parent = parent;
            this.compileAction = compileAction;
        }

        public SequenceLink Next<TResult>(Func<TResult> compileFunction, Action<TResult> callback)
        {
            return new SequenceLink(Compilers, this, () => { callback(compileFunction()); });
        }

        public void Compile()
        {
            this.Compile(0);
        }

        private void Compile(int n)
        {
            if (this.parent == null)
            {
                return;
            }

            this.parent.Compile(n + 1);
            compileAction();

            if (n == 1)
            {
                Compilers.Pop.KeyWord(KeyWord.And);
            }
            else if (n > 1)
            {
                Compilers.Pop.Separartor(Separator.Comma);
            }
        }
    }
}
