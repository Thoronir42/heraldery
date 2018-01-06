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

            var token = PopTokenAs(DefinitionType.ChargeProperty);
            var def = token.Definition as ChargePropertyDefinition;
            do
            {
                switch (def.TokenSubtype)
                {
                    case PropertyType.Tail:
                        var styleDef = PopDefinition<TailStylePropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.TailStyle);
                        list.Add(new TailProperty(styleDef.Style, new SolidFilling(Compilers.Tincture.Tincture())));
                        break;
                    case PropertyType.Attitude:
                        var attitudeDef = def as AttitudePropertyDefinition;
                        var direction = AttitudeDirection.Forward;
                        if(NextTokenIs(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection))
                        {
                            var attDirDef = PopDefinition<AttitudeDirectionPropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection);
                            direction = attDirDef.Direction;
                        }
                        list.Add(new AttitudeProperty(attitudeDef.Attitude, direction, attitudeDef.ExclusiveTo));
                        break;
                    case PropertyType.Feature:
                        var featureDef = def as FeaturePropertyDefinition;
                        list.Add(new FeatureProperty(featureDef.Feature, new SolidFilling(Compilers.Tincture.Tincture())));
                        break;
                    default:
                        throw new UnexpectedTokenException(token, "Unexpected property of type " + def.TokenSubtype.ToString());
                }

                
            } while (NextTokenIs(DefinitionType.ChargeProperty));
            

            return list;
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

            var ordinaryFilling = new SolidFilling(Compilers.Tincture.Tincture());

            OrdinaryDefinition def = currentToken.Definition as OrdinaryDefinition;

            OrdinaryCharge charge = new OrdinaryCharge { OrdinaryType = def.Type, Filling = ordinaryFilling, OrdinarySize = def.Size };
            return charge;
        }
    }
}
