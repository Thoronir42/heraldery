using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public interface IDefinition
    {
        DefinitionType TokenType { get; }

        object TokenObjSubtype { get; }

        string Text { get; set; }
    }


    public abstract class Definition<TSubtype> : IDefinition
    {
        public string Text { get; set; }

        public DefinitionType TokenType { get; }
        public virtual TSubtype TokenSubtype { get; }
        public object TokenObjSubtype { get { return TokenSubtype; } }

        protected Definition(DefinitionType type, TSubtype subtype)
        {
            TokenType = type;
            TokenSubtype = subtype;
        }

        public override bool Equals(object obj)
        {
            var definition = obj as Definition<TSubtype>;
            return definition != null &&
                Object.Equals(TokenType, definition.TokenType) &&
                Object.Equals(TokenSubtype, definition.TokenSubtype);
        }

        public override int GetHashCode()
        {
            var hashCode = 1265339359;
            hashCode = hashCode * -1521134295 + GetType().GetHashCode();
            if (TokenSubtype != null)
            {
                hashCode = hashCode * -1521134295 + TokenType.GetHashCode();
            }
            return hashCode;
        }
    }
}
