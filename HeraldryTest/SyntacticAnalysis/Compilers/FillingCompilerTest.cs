using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.SyntacticAnalysis.Compilers;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.SyntacticAnalysis;
using Heraldry.Blazon.Vocabulary;
using HeraldryTest.Helpers;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    [TestClass]
    public class FillingCompilerTest : CompilerTestBase
    {
        [TestMethod]
        public void SimpleTincture()
        {
            RootCompiler root = CreateRoot(
                Token.Tincture(TinctureType.Colour, "brick-red"),
                // anything else
                Token.Number(NumberType.Cardinal, 6)
                );

            FillingCompiler tc = new FillingCompiler(root);

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

            FillingCompiler tc = new FillingCompiler(root);

            var t = tc.Tincture() as FurTincture;

            Assert.IsNotNull(t);
            Assert.AreEqual("ermine", t.Pattern);
            Assert.AreEqual("white", t.PrimaryColor.Value);
            Assert.AreEqual("black", t.SecondaryColor.Value);
        }

        [TestMethod]
        public void CustomTinctureFur()
        {
            var root = CreateRoot(
                Token.TinctureFur("ermine", "foo", "bar"),
                Token.Tincture(TinctureType.Colour, "pink"),
                Token.Keyword(KeyWord.And),
                Token.Tincture(TinctureType.Colour, "black")
                );

            FillingCompiler tc = new FillingCompiler(root);

            var tincture = tc.Tincture() as FurTincture;

            Assert.IsNotNull(tincture);

            Assert.AreEqual("ermine", tincture.Pattern);
            Assert.AreEqual(new Tincture(TinctureType.Colour, "pink"), tincture.PrimaryColor);
            Assert.AreEqual(new Tincture(TinctureType.Colour, "black"), tincture.SecondaryColor);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpectedTokenNotFoundException))]
        public void CustomTinctureFurException()
        {
            var root = CreateRoot(
                Token.TinctureFur("ermine", "foo", "bar"),
                Token.Tincture(TinctureType.Colour, "pink")
                );

            FillingCompiler tc = new FillingCompiler(root);

            tc.Tincture();
        }
    }
}
