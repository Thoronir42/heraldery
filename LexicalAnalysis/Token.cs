using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    public class Token
    {
        public Definition Definition { get; set; }
        public DefinitionType Type
        {
            get { return Definition.GetTokenType(); }
        }
        public object Subtype
        {
            get { return Definition.GetSubtype(); }
        }

        //public object Value { get; set; } // todo: generify Value type

        public int Position { get; set; }

        public Token() { }

        /// <summary>
        /// Constructor which takes position and definition as a parameter.
        /// Used mainly for testing.
        /// </summary>
        /// <param name="position">Token position.</param>
        /// <param name="definition">Token definition.</param>
        public Token(int position, Definition definition)
        {
            Definition = definition;
            Position = position;
        }
    }
}
