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
    public class FieldVariationDefinition : Definition<FieldVariationType>
    {
        public FieldVariationType VariationType { get; }

        public FieldVariationDefinition(FieldVariationType type) : base(DefinitionType.Variation, type)
        {
            this.VariationType = type;
        }
    }
}
