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
        public TinctureType TinctureType { get; set; }
        public String Value { get; set; }

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
            TinctureType = tinctureType;
            Text = text;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Tincture;
        }

        public override string ToString()
        {
            return String.Format("Tincture {0}:{1}", this.Text, this.TinctureType);
        }

        public override bool Equals(object obj)
        {
            var definition = obj as TinctureDefinition;
            return definition != null &&
                   base.Equals(obj) &&
                   TinctureType == definition.TinctureType &&
                   Value == definition.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -1556832619;
            hashCode = hashCode * -1521134295 + TinctureType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
