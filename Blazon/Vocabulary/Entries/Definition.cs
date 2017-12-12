using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public abstract class Definition
    {
        public string Text { get; set; }

        abstract public DefinitionType GetTokenType();

        public virtual object GetSubtype()
        {
            return null;
        }

        public override bool Equals(object obj)
        {
            var definition = obj as Definition;
            return definition != null &&
                Object.Equals(GetType(), definition.GetType()) &&
                Object.Equals(GetSubtype(), definition.GetSubtype());
        }

        public override int GetHashCode()
        {
            var hashCode = 1265339359;
            hashCode = hashCode * -1521134295 + GetType().GetHashCode();
            var subtype = GetSubtype();
            if (subtype != null)
            {
                hashCode = hashCode * -1521134295 + subtype.GetHashCode();
            }
            return hashCode;
        }
    }
}
