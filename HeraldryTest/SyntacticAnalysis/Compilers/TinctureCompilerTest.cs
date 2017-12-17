using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Structure;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    [TestClass]
    public class TinctureCompilerTest : CompilerTestBase
    {
        [TestMethod]
        public void SimpleTincture()
        {
            RootCompiler root = CreateRoot(
                Token.Tincture(TinctureType.Colour, "or"),
                // anything else
                Token.Number(NumberType.Cardinal, 6)
                );

            TinctureCompiler tc = new TinctureCompiler(root);

            Filling t = tc.Tincture();

            Assert.AreEqual(FillingLayoutType.Solid, t.Layout.FillingLayoutType);
            Assert.AreEqual(1, t.Tinctures.Length);
            Assert.AreEqual(new TinctureDefinition { TinctureType = TinctureType.Colour, Text = "or" }, t.Tinctures[0]);
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

            FurFilling t = tc.Tincture() as FurFilling;

            Assert.IsNotNull(t);
            Assert.AreEqual("ermine", t.Pattern);
            Assert.AreEqual("white", t.PrimaryColor.Value);
            Assert.AreEqual("black", t.SecondaryColor.Value);
        }
    }
}
