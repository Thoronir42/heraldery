using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class ShapeTypeDefinition : Definition<ShapeType>
    {
        public ShapeType ShapeType { get; }

        public ShapeTypeDefinition(ShapeType type = ShapeType.Solid) : base(DefinitionType.ShapeType, type)
        {
            this.ShapeType = type;
        }
    }
}
