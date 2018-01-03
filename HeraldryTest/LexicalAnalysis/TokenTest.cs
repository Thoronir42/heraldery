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
            var a = new Token()
            {
                Definition = new TinctureDefinition(new Tincture(TinctureType.Colour, "periwinkle")),
                Position = 0,
            };

            var b = new Token()
            {
                Definition = new TinctureDefinition(new Tincture(TinctureType.Colour, "periwinkle")),
                Position = 0,
            };

            Assert.AreEqual(a, b);
        }
    }
}
