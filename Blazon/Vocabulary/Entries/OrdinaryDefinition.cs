using Heraldry.Blazon.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class OrdinaryDefinition : Definition
    {
        public Ordinary Type { get; set; }
        public OrdinarySize Size { get; set; } = OrdinarySize.Honourable;

        public override DefinitionType GetTokenType()
        {
            return DefinitionType.Ordinary;
        }

        public override object GetSubtype()
        {
            return base.GetSubtype();
        }

        public override bool Equals(object obj)
        {
            var definition = obj as OrdinaryDefinition;
            return definition != null &&
                   base.Equals(obj) &&
                   Type == definition.Type &&
                   Size == definition.Size;
        }

        public override int GetHashCode()
        {
            var hashCode = 543891697;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            return hashCode;
        }
    }
}
