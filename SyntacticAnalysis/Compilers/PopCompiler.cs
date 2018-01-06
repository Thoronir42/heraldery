using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public class PopCompiler : BaseCompiler
    {
        internal PopCompiler(RootCompiler root) : base(root)
        {
        }

        public void KeyWord(KeyWord keyWord)
        {
            PopTokenAs(DefinitionType.KeyWord, keyWord);
        }

        public void Separartor(Separator separator)
        {
            PopTokenAs(DefinitionType.Separator, separator);
        }
    }
}
