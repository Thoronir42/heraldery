using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class TinctureDefinition : Definition
    {
        public TinctureType Type { get; set; }

        /// <summary>
        /// Default constructor, does nothing.
        /// </summary>
        public TinctureDefinition()
        {

        }

        /// <summary>
        /// Constructor with tincture type and text specified.
        /// </summary>
        /// <param name="tinctureType">Type of the tincture</param>
        /// <param name="text">Tincture text.</param>
        public TinctureDefinition(TinctureType tinctureType, String text)
        {
            Type = tinctureType;
            Text = text;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Tincture;
        }

        public override string ToString()
        {
            return String.Format("Tincture {0}:{1}", this.Text, this.Type);
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != typeof(TinctureDefinition))
            {
                return false;
            }

            TinctureDefinition other = (TinctureDefinition)obj;

            return Type == other.Type && Text == other.Text;
        }
    }
}
