using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Entries.ChargeProperties;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    class ChargeCompiler : BaseCompiler
    {
        internal ChargeCompiler(RootCompiler root) : base(root)
        {

        }

        /// <summary>
        /// Rule which will parse principal charge.
        /// 
        /// </summary>
        /// <returns>Principal charge.</returns>
        [SyntacticRule]
        public Charge PrincipalCharge()
        {
            if (NextTokenIs(DefinitionType.KeyWord, KeyWord.Determiner))
            {
                PopToken();
            }

            Charge charge = Charge();

            var nextToken = PeekToken();
            if(TokenIs(nextToken, DefinitionType.ChargeProperty))
            {
                charge.Properties.AddRange(Properties());
            }

            return charge;
        }

        [SyntacticRule]
        public List<ChargeProperty> Properties()
        {
            List<ChargeProperty> list = new List<ChargeProperty>();

            do
            {
                if(NextTokenIs(DefinitionType.ChargeProperty, PropertyType.Feature))
                {
                    var features = PopList(DefinitionType.ChargeProperty, PropertyType.Feature, (FeaturePropertyDefinition d) => d.Feature);
                    var filling = Compilers.Filling.Filling();

                    list.AddRange(features.Select(feature => new FeatureProperty(feature, filling)));

                    continue;
                }
                list.Add(Property());
                
            } while (NextTokenIs(DefinitionType.ChargeProperty));
            

            return list;
        }

        [SyntacticRule]
        public ChargeProperty Property(PropertyType? expectedType = null)
        {
            var token = PopTokenAs(DefinitionType.ChargeProperty);
            var def = token.Definition as ChargePropertyDefinition;

            if(expectedType.HasValue && def.TokenSubtype != expectedType.Value)
            {
                throw new ExpectedTokenNotFoundException(DefinitionType.ChargeProperty, expectedType.Value);
            }

            switch (def.TokenSubtype)
            {
                case PropertyType.TailStyle:
                    var styleDef = def as TailStylePropertyDefinition;
                    return new TailProperty(styleDef.Style, Compilers.Filling.Filling());

                case PropertyType.Attitude:
                    var attitudeDef = def as AttitudePropertyDefinition;
                    var direction = AttitudeDirection.Forward;
                    if (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection))
                    {
                        var attDirDef = PopDefinition<AttitudeDirectionPropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection);
                        direction = attDirDef.Direction;
                    }
                    return new AttitudeProperty(attitudeDef.Attitude, direction, attitudeDef.ExclusiveTo);

                default:
                    throw new UnexpectedTokenException(token, "Unexpected property of type " + def.TokenSubtype.ToString());
            }
        }

        private Charge Charge()
        {
            var nextToken = PeekToken();
            switch (nextToken.Type)
            {
                case DefinitionType.Ordinary:
                    return Ordinary();
                case DefinitionType.Charge:
                    var def = PopDefinition<ChargeDefinition>(DefinitionType.Charge);

                    return def.Charge;
            }

            throw new ExpectedTokenNotFoundException(new TokenType(DefinitionType.Charge), new TokenType(DefinitionType.Ordinary));
        }

        /// <summary>
        /// Rule which will parse ordinary charges.
        /// Ordinates consist of ordinary type and tincture.
        /// 
        /// </summary>
        /// <returns>Ordinary charge.</returns>
        private Charge Ordinary()
        {
            Token currentToken = PopTokenAs(DefinitionType.Ordinary);

            var ordinaryFilling = Compilers.Filling.Filling();

            OrdinaryDefinition def = currentToken.Definition as OrdinaryDefinition;

            OrdinaryCharge charge = new OrdinaryCharge { OrdinaryType = def.Type, Filling = ordinaryFilling, OrdinarySize = def.Size };
            return charge;
        }
    }
}
