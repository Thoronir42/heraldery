using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    /// <summary>
    /// Used for tokens representing numbers.
    /// </summary>
    public class NumberDefinition : Definition
    {
   
        /// <summary>
        /// Default constructor, does nothing.
        /// </summary>
        public NumberDefinition()
        {

        }

        /// <summary>
        /// Constructor for a particular number.
        /// </summary>
        /// <param name="number">Number.</param>
        public NumberDefinition(int number)
        {
            Text = number.ToString();
        }

        /// <summary>
        /// Returns number. If no number was set, exception will be thrown.
        /// </summary>
        /// <returns>Number.</returns>
        public int GetNumber()
        {
            return Int32.Parse(Text);
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Number;
        }
        
    }
}
