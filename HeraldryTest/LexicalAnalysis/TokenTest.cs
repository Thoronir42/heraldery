using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.LexicalAnalysis;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Elements;

namespace HeraldryTest.LexicalAnalysis
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void TestTokenEquals()
        {
            var a = new Token(0, new TinctureDefinition(new Tincture(TinctureType.Colour, "periwinkle")));

            var b = new Token(0, new TinctureDefinition(new Tincture(TinctureType.Colour, "periwinkle")));

            Assert.AreEqual(a, b);
        }
    }
}
