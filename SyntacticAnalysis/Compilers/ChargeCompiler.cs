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

            Charge charge = Charge();

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
                var list = PopList(DefinitionType.ChargeProperty, PropertyType.Feature, (FeaturePropertyDefinition def) => def.Feature);

                filling = Compilers.Filling.Filling();

                foreach (var feature in list)
                {
                    charge.Features.Add(new FeatureProperty(feature, filling));
                }
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

        private Charge Charge()
        {
            var nextToken = PeekToken();
            switch (nextToken.Type)
            {
                case DefinitionType.Ordinary:
                    var ordDef = PopDefinition<OrdinaryDefinition>(DefinitionType.Ordinary);

                    return new OrdinaryCharge(ordDef.Type, ordDef.Size);

                case DefinitionType.Charge:
                    var def = PopDefinition<ChargeDefinition>(DefinitionType.Charge);

                    return def.Charge;
            }

            throw new ExpectedTokenNotFoundException(new TokenType(DefinitionType.Charge), new TokenType(DefinitionType.Ordinary));
        }
    }
}
