using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class PositionDefinition : Definition
    {
        public PositionType Type { get; set; }
        public Position Position { get; set; }

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Position;
        }

        public override bool Equals(object obj)
        {
            var definition = obj as PositionDefinition;
            return definition != null &&
                   base.Equals(obj) &&
                   Type == definition.Type &&
                   EqualityComparer<Position>.Default.Equals(Position, definition.Position);
        }

        public override int GetHashCode()
        {
            var hashCode = 1554171551;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Position>.Default.GetHashCode(Position);
            return hashCode;
        }
    }
}
