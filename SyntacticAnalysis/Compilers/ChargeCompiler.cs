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
    public class ChargeCompiler : BaseCompiler
    {
        public ChargeCompiler(RootCompiler root) : base(root)
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

            Charge charge = null;
            if (NextTokenIs(DefinitionType.Ordinary))
            {
                charge = OrdinaryCharge();
            }

            if (NextTokenIs(DefinitionType.Charge))
            {
                charge = PropertyfullCharge();
            }

            if (charge == null)
            {
                throw new ExpectedTokenNotFoundException(new TokenType(DefinitionType.Charge), new TokenType(DefinitionType.Ordinary));
            }

            return charge;
        }

        [SyntacticRule]
        public AttitudeProperty Attitude()
        {
            var def = PopDefinition<AttitudePropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.Attitude);

            var direction = AttitudeDirection.Forward;

            if (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection))
            {
                var dirDef = PopDefinition<AttitudeDirectionPropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.AttitudeDirection);
                direction = dirDef.Direction;
            }

            return new AttitudeProperty(def.Attitude, direction, def.ExclusiveTo);
        }

        [SyntacticRule]
        public TailProperty Tail()
        {
            if (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.Tail))
            {
                PopToken();
            }

            var def = PopDefinition<TailStylePropertyDefinition>(DefinitionType.ChargeProperty, PropertyType.TailStyle);

            return new TailProperty(def.Style);
        }

        public OrdinaryCharge OrdinaryCharge()
        {
            var definition = PopDefinition<OrdinaryDefinition>(DefinitionType.Ordinary);

            var content = Compilers.Field.ContentField();

            return new OrdinaryCharge(definition.Type, content, definition.Size);
        }

        public Charge PropertyfullCharge()
        {
            var def = PopDefinition<ChargeDefinition>(DefinitionType.Charge);

            var charge = new GenericCharge((def.Charge as GenericCharge).Value);

            if (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.Attitude))
            {
                charge.Attitude = Attitude();
            }

            if (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.Tail) || NextTokenIs(DefinitionType.ChargeProperty, PropertyType.TailStyle))
            {
                charge.Tail = Tail();
            }

            var filling = charge.Filling = Compilers.Filling.Filling();

            while (NextTokenIs(DefinitionType.ChargeProperty, PropertyType.Feature))
            {
                var list = PopList(DefinitionType.ChargeProperty, PropertyType.Feature, (FeaturePropertyDefinition d) => d.Feature);

                filling = Compilers.Filling.Filling();

                foreach (var feature in list)
                {
                    charge.Features.Add(new FeatureProperty(feature, filling));
                }
            }

            return charge;

        }
    }
}
