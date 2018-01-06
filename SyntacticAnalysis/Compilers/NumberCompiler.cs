using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public class NumberCompiler : BaseCompiler
    {
        public NumberCompiler(RootCompiler root) : base(root)
        {
        }

        /// <summary>
        /// A rule for parsing sequences of numbers in this form:
        /// number and number and ... and number 
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Number> Nums(NumberType? type = null)
        {
            if(!type.HasValue)
            {
                return PopList(DefinitionType.Number, (NumberDefinition def) => def.Number);
            } else
            {
                return PopList(DefinitionType.Number, type.Value, (NumberDefinition def) => def.Number);
            }
        }

        public Number Ordinal()
        {
            return (PopDefinition<NumberDefinition>(DefinitionType.Number, NumberType.Ordinal)).Number;
        }

        public Number Cardinal()
        {
            return (PopDefinition<NumberDefinition>(DefinitionType.Number, NumberType.Cardinal)).Number;
        }
    }
}
