using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class KeyWordDefinition : Definition
    {
        public KeyWord KeyWord { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.KeyWord;
        }

        /// <summary>
        /// Default constructor. Does nothing.
        /// </summary>
        public KeyWordDefinition()
        {

        }
        
        /// <summary>
        /// Constructor with keyword specified.
        /// </summary>
        /// <param name="keyWord">Keyword to be used in this definition.</param>
        public KeyWordDefinition(KeyWord keyWord)
        {
            KeyWord = keyWord;
        }
    }
}
