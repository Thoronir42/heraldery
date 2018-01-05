using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    class PositionDefinition : Definition<PositionType>
    {
        public PositionType Type { get; }
        public Position Position { get; }

        public PositionDefinition(Position position, PositionType type)
            : base(DefinitionType.Position, type)
        {
            this.Position = position;
            this.Type = type;
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
