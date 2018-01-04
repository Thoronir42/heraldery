using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Elements;

namespace Heraldry.LexicalAnalysis
{
    public class Token
    {
        public Definition Definition { get; }
        public DefinitionType Type
        {
            get { return Definition.GetTokenType(); }
        }
        public object Subtype
        {
            get { return Definition.GetSubtype(); }
        }

        public int Position { get; set; }

        

        public Token(Definition definition)
        {
            Definition = definition;
        }

        /// <summary>
        /// Constructor which takes position and definition as a parameter.
        /// Used mainly for testing.
        /// </summary>
        /// <param name="position">Token position.</param>
        /// <param name="definition">Token definition.</param>
        public Token(int position, Definition definition)
        {
            Definition = definition;
            Position = position;
        }

        public TokenType GetFullType()
        {
            return new TokenType(this.Type, this.Subtype);
        }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   EqualityComparer<Definition>.Default.Equals(Definition, token.Definition) &&
                   Position == token.Position;
        }

        public override int GetHashCode()
        {
            var hashCode = 1698227724;
            hashCode = hashCode * -1521134295 + EqualityComparer<Definition>.Default.GetHashCode(Definition);
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            return hashCode;
        }
    }
}
