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

            if (tinctureDef.TinctureType == TinctureType.Colour || tinctureDef.TinctureType == TinctureType.Metal)
            {
                return tinctureDef.Tincture;
            }
            if (tinctureDef.TinctureType == TinctureType.Fur)
            {
                // call fur rule
                return Fur(tinctureDef);
            }

            throw new ArgumentException("TinctureType " + tinctureDef.TinctureType + " not recognized");
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
            Token token = PopTokenAs(DefinitionType.Tincture);
            TinctureDefinition tDef = token.Definition as TinctureDefinition;

            if (tDef.TinctureType == TinctureType.Fur)
            {
                throw new UnexpectedTokenException(token, "Non-fur tincture definition is expected.");
            }

            return tDef;
        }
    }
}
