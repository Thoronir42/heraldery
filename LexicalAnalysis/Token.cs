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

        public object Value { get; set; } // todo: generify Value type

        public int Position { get; set; }

        /// <summary>
        /// Default constructor, sets position to 0.
        /// </summary>
        public Token()
        {
            Position = 0;
        }

        /// <summary>
        /// Creates token with position and number definition specified.
        /// </summary>
        /// <param name="position">Position of the token.</param>
        /// <param name="number">Number for number definition.</param>
        public Token(int position, int number) : this(position, new NumberDefinition(number))
        {

        }

        /// <summary>
        /// Constructor with position and definition specified.
        /// </summary>
        /// <param name="position">Position of the token.</param>
        /// <param name="definition">Definition of the token.</param>
        public Token (int position, Definition definition)
        {
            Position = position;
            Definition = definition;
        }

        /// <summary>
        /// Constructor fot keyword tokens.
        /// 
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="keyWord">Keyword this token representa.</param>
        public Token(int position, KeyWord keyWord) :this(position, new KeyWordDefinition(keyWord))
        {

        }

        /// <summary>
        /// Helper method which will return true if the token represents expected keyword.
        /// </summary>
        /// <param name="expectedKeyWord">Expected keyword.</param>
        /// <returns>True if token represents expected keyword.</returns>
        public Boolean IsKeyWord(KeyWord expectedKeyWord)
        {
            return
                Type == DefinitionType.KeyWord &&
                ((KeyWordDefinition)Definition).KeyWord == expectedKeyWord;
        }
    }
}
