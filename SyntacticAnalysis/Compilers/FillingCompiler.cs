using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public class FillingCompiler : BaseCompiler
    {
        public FillingCompiler(RootCompiler root) : base(root)
        {
        }

        [SyntacticRule]
        public Filling Filling()
        {
            if(NextTokenIs(DefinitionType.Tincture))
            {
                return new SolidFilling(Tincture());
            }

            if(NextTokenIs(DefinitionType.Variation))
            {
                return VariatedFilling();
            }

            throw new ExpectedTokenNotFoundException(TokenType.Types(DefinitionType.Tincture, DefinitionType.Variation));
        }

        public Filling VariatedFilling()
        {
            // first, the type of variation follows, then variation tinctures should be defined
            FieldVariationDefinition definition = PopDefinition<FieldVariationDefinition>(DefinitionType.Variation);
            
           switch (Blazon.Structure.Filling.TypeByVariation(definition.VariationType))
            {
                // todo: implement various field variation
                case FillingType.NPattern:
                    NumberDefinition numDef = PopDefinition<NumberDefinition>(DefinitionType.Number, NumberType.Cardinal);
                    return new PatternFilling(definition.VariationType, numDef.Number.Value, VariationFillings());

                case FillingType.Pattern:
                    return new PatternFilling(definition.VariationType, VariationFillings());

                case FillingType.Seme:
                    return new SemeFilling(null, Compilers.Charge.PrincipalCharge());

                default:
                    throw new NotImplementedException("Filling type of " + definition.VariationType + " is not implemented");
            }

        }

        /// <summary>
        /// Rule which will parse fillings for a variation.
        /// Basically: TINCTURE AND TINCTURE
        /// 
        /// </summary>
        /// <param name="tokens">Tokens to be parsed.</param>
        /// <returns>List of defined tinctures.</returns>
        private Tincture[] VariationFillings()
        {
            Tincture[] fillings = new Tincture[2];

            // first filling
            fillings[0] = Compilers.Filling.Tincture();

            // and
            Token currentToken = PopTokenAs(DefinitionType.KeyWord, KeyWord.And);

            // second filling
            fillings[1] = Compilers.Filling.Tincture();

            return fillings;
        }


        /// <summary>
        /// Tincture rule - tincture definition is expected to be at the current token.
        /// Fur definition can also start at current position.
        /// 
        /// </summary>
        /// <returns>Parsed filling - tincture or fur.</returns>
        [SyntacticRule]
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
            FurTincture tincture = definition.Tincture as FurTincture;

            var nextToken = PeekToken();
            if(nextToken.Type == DefinitionType.Tincture)
            {
                tincture.PrimaryColor = NonFurTincture();
                PopTokenAs(DefinitionType.KeyWord, KeyWord.And);
                tincture.SecondaryColor = NonFurTincture();
            }

            return tincture;
        }

        protected Tincture NonFurTincture()
        {
            var tDef = PopDefinition<TinctureDefinition>(DefinitionType.Tincture);
            var tincture = tDef.Tincture;

            if (tincture.TinctureType == TinctureType.Fur)
            {
                TokenType[] expectedTypes = TokenType.Subtypes(DefinitionType.Tincture, TinctureType.Colour, TinctureType.Metal);
                throw new ExpectedTokenNotFoundException("Non-fur tincture definition is expected.");
            }

            return tincture;
        }
    }
}
