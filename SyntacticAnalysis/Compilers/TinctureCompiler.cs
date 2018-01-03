using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
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
    public class TinctureCompiler : BaseCompiler
    {
        public TinctureCompiler(RootCompiler root) : base(root)
        {

        }

        /// <summary>
        /// Tincture rule - tincture definition is expected to be at the current token.
        /// Fur definition can also start at current position.
        /// 
        /// </summary>
        /// <returns>Parsed filling - tincture or fur.</returns>
        public Tincture Tincture()
        {
            TinctureDefinition tinctureDef = PopDefinition<TinctureDefinition>(DefinitionType.Tincture);
            var tincture = tinctureDef.Tincture;

            if (tincture.TinctureType == TinctureType.Colour || tincture.TinctureType == TinctureType.Metal)
            {
                return tincture;
            }
            if (tincture.TinctureType == TinctureType.Fur)
            {
                // call fur rule
                return Fur(tinctureDef);
            }

            throw new ArgumentException("TinctureType " + tincture.TinctureType + " not recognized");
        }

        /// <summary>
        /// Furs definition - fur definition is expected to start at the current token.
        /// 
        /// </summary>
        /// <returns>Parsed fur.</returns>
        protected Tincture Fur(TinctureDefinition definition)
        {
            return definition.Tincture;
            
            // FurFilling filling = new FurFilling(definition.Value);

            /* custom color furs are not supported yet
            Token nextToken = PeekToken();
            if(!TokenIs(nextToken, DefinitionType.Tincture))
            {
                return filling;
            }


            filling.PrimaryColor = NonFurTincture();
            PopTokenAs(DefinitionType.KeyWord, KeyWord.And);
            filling.SecondaryColor = NonFurTincture();
            */
        }

        protected TinctureDefinition NonFurTincture()
        {
            var tDef = PopDefinition<TinctureDefinition>(DefinitionType.Tincture);
            var tincture = tDef.Tincture;

            if (tincture.TinctureType == TinctureType.Fur)
            {
                throw new UnexpectedTokenException("Non-fur tincture definition is expected.");
            }

            return tDef;
        }
    }
}
