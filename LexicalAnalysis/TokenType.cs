using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.LexicalAnalysis
{
    public class TokenType
    {
        public DefinitionType Type { get; }
        public object Subtype { get; }

        public TokenType(DefinitionType type, object subtype = null)
        {
            Type = type;
            Subtype = subtype;
        }

        public override string ToString()
        {
            return Type.ToString() + (Subtype != null ? " - " + Subtype.ToString() : "");
        }

        public static TokenType[] Subtypes(DefinitionType type, params object[] subtypes)
        {
            if(subtypes.Length == 0)
            {
                return new TokenType[] { new TokenType(type) };
            }

            return subtypes
                .Select(subtype => new TokenType(type, subtype))
                .ToArray();
        }
    }
}
