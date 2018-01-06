using Heraldry.Blazon.Charges;
using Heraldry.Blazon.Charges.Properties;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.SyntacticAnalysis.Compilers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    [TestClass]
    public class ChargeCompilerTest : CompilerTestBase
    {
        [TestMethod]
        public void TestOrdinary()
        {
            var root = CreateRoot(
                Token.Ordinary(Ordinary.Chief, OrdinarySize.Honourable),
                Token.Tincture(TinctureType.Metal, "titan")
                );

            var cc = new ChargeCompiler(root);

            var charge = cc.PrincipalCharge();
        }

        [TestMethod]
        public void TestLionTailTinctured()
        {
            var root = CreateRoot(
                Token.GenericCharge("lion"),
                Token.ChargeProperty(PropertyType.Tail),
                Token.TailStyleProperty(TailStyle.Fourche),
                Token.Tincture(TinctureType.Colour, "black")
                );

            var cc = new ChargeCompiler(root);

            var expectedFilling = new SolidFilling(new Tincture(TinctureType.Colour, "black"));


            var charge = cc.PrincipalCharge() as GenericCharge;

            Assert.IsNotNull(charge);

            Assert.AreEqual(expectedFilling, charge.Filling);

            Assert.AreEqual(1, charge.Properties.Count);

            var tailProp = charge.Properties[0] as TailProperty;

            Assert.IsNotNull(tailProp);

            Assert.AreEqual(TailStyle.Fourche, tailProp.Style);
        }

        [TestMethod]
        public void TestMammothAttitudeTinctured()
        {
            var root = CreateRoot(
                Token.GenericCharge("mammoth"),
                Token.AttitudeProperty(Attitude.Passant),
                Token.AttitudeProperty(AttitudeDirection.Regardant),
                Token.Tincture(TinctureType.Colour, "silvery")
                );

            var cc = new ChargeCompiler(root);

            var expectedFilling = new SolidFilling(new Tincture(TinctureType.Colour, "silvery"));

            var charge = cc.PrincipalCharge() as GenericCharge;

            Assert.IsNotNull(charge);

            Assert.AreEqual(new AttitudeProperty(Attitude.Passant, AttitudeDirection.Regardant), charge.Properties[0]);

            Assert.AreEqual(expectedFilling, charge.Filling);

            
        }
    }
}
