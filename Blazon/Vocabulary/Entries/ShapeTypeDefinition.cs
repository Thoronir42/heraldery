using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class ShapeTypeDefinition : Definition
    {
        public ShapeType ShapeType { get; }

        public ShapeTypeDefinition(ShapeType type = ShapeType.Solid)
        {
            this.ShapeType = type;
        }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.ShapeType;
        }

        public override object GetSubtype()
        {
            return ShapeType;
        }
    }
}
