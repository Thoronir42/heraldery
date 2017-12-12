using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    /// <summary>
    /// Token with this definition represents variation. 
    /// </summary>
    public class FieldVariationDefinition : Definition
    {
        public FieldVariationType VariationType { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Variation;
        }

        public override object GetSubtype()
        {
            return VariationType;
        }
    }
}
