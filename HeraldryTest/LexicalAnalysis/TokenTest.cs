using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.LexicalAnalysis;
using Heraldry.Blazon.Vocabulary.Entries;

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
                Definition = new TinctureDefinition()
                {
                    TinctureType = Heraldry.Blazon.Elements.TinctureType.Colour,
                    Value = "periwinkle",
                },
                Position = 0,
            };
            var b = new Token()
            {
                Definition = new TinctureDefinition()
                {
                    TinctureType = Heraldry.Blazon.Elements.TinctureType.Colour,
                    Value = "periwinkle",
                },
                Position = 0,
            };

            Assert.AreEqual(a, b);
        }
    }
}
