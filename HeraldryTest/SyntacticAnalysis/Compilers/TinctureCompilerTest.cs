using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.SyntacticAnalysis.Compilers;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.Blazon.Structure.Fillings;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    [TestClass]
    public class TinctureCompilerTest : CompilerTestBase
    {
        [TestMethod]
        public void SimpleTincture()
        {
            RootCompiler root = CreateRoot(
                Token.Tincture(TinctureType.Colour, "brick-red"),
                // anything else
                Token.Number(NumberType.Cardinal, 6)
                );

            TinctureCompiler tc = new TinctureCompiler(root);

            Tincture t = tc.Tincture();

            Assert.AreEqual(TinctureType.Colour, t.TinctureType);
            Assert.AreEqual("brick-red", t.Value);
        }

        [TestMethod]
        public void SimpleFur()
        {
            RootCompiler root = CreateRoot(
                Token.TinctureFur("ermine", "white", "black"),
                // anything else
                Token.Number(NumberType.Cardinal, 6)
                );

            TinctureCompiler tc = new TinctureCompiler(root);

            var t = tc.Tincture() as FurTincture;

            Assert.IsNotNull(t);
            Assert.AreEqual("ermine", t.Pattern);
            Assert.AreEqual("white", t.PrimaryColor);
            Assert.AreEqual("black", t.SecondaryColor);
        }
    }
}
