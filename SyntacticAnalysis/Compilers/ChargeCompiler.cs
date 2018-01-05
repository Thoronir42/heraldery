using Heraldry.Blazon.Charges;
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
        public Charge PrincipalCharge()
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
        protected Charge Ordinary()
        {
            Token currentToken = PopTokenAs(DefinitionType.Ordinary);

            var ordinaryFilling = new SolidFilling(Compilers.Tincture.Tincture());

            OrdinaryDefinition def = currentToken.Definition as OrdinaryDefinition;

            OrdinaryCharge charge = new OrdinaryCharge { OrdinaryType = def.Type, Filling = ordinaryFilling, OrdinarySize = def.Size };
            return charge;
        }
    }
}
