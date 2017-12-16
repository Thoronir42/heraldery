using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    class TinctureCompiler : BaseCompiler
    {
        internal TinctureCompiler(RootCompiler root) : base(root)
        {

        }

        /// <summary>
        /// Tincture rule - tincture definition is expected to be at the current token.
        /// Fur definition can also start at current position.
        /// 
        /// </summary>
        /// <returns>Parsed filling - tincture or fur.</returns>
        public Filling Tincture()
        {
            Token currentToken = PeekToken();

            // check that current token is really a tincture
            EnsureTokenIs(currentToken, DefinitionType.Tincture);

            // 
            if (currentToken.Definition.GetType() != typeof(TinctureDefinition))
            {
                throw new Exception(String.Format("Unexpected type of definition '{0}' of token at position {1}. Expected {2}.",
                    currentToken.Definition.GetType(), Compilers.Position, typeof(TinctureDefinition)
                    ));
            }
            TinctureDefinition tinctureDef = (TinctureDefinition)currentToken.Definition;
            if (tinctureDef.TinctureType == TinctureType.Colour || tinctureDef.TinctureType == TinctureType.Metal)
            {
                PopToken();

                // process tincture
                TinctureDefinition def = new TinctureDefinition { Text = currentToken.Definition.Text };

                Filling filling = new Filling
                {
                    Layout = FillingLayout.Solid(),
                    Tinctures = new TinctureDefinition[] { def }
                };

                return filling;
            }
            else
            {
                // call fur rule
                return Furs();
            }
        }

        /// <summary>
        /// Furs definition - fur definition is expected to start at the current token.
        /// 
        /// </summary>
        /// <param name="tokens">List of tokens to be parsed.</param>
        /// <returns>Parsed fur.</returns>
        protected Filling Furs()
        {
            Token currentToken = PopToken();
            // todo: implement this
            return null;
        }
    }
}
