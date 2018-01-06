using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.Blazon.Vocabulary.Entries
{
    public class CommentDefinition : IDefinition
    {
        public DefinitionType TokenType { get { return DefinitionType.Comment; } }

        public object TokenObjSubtype { get { return null; } }

        public string Text { get; set; }

        public string Comment { get; }

        public CommentDefinition(String comment)
        {
            this.Comment = comment;
        }

    }
}
