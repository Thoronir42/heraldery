using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Elements;

namespace HeraldryTest.Blazon
{
    /// <summary>
    /// Heads up: this test set depends on the en_olde/ vocabulary
    /// </summary>
    [TestClass]
    public class VocabularyDefinerTest
    {
        [TestMethod]
        public void DefineDivisions()
        {
            var definer = MockVocabulary.Get().GetDefiner();

            var cases = new object[][]
            {
                new object[] { "quarterly", FieldDivisionType.Quarterly },
                new object[] { "party per bend", FieldDivisionType.PartyPerBend },
                new object[] { "gyrony of", FieldDivisionType.GyronyOfN },
                new object[] { "party per bend", FieldDivisionType.PartyPerBend },
            };

            foreach (object[] testCase in cases)
            {
                FieldDivisionType fdt = (FieldDivisionType)testCase[1];
                Assert.AreEqual(testCase[0], definer.FieldDivision(fdt));
            }
        }

        [TestMethod]
        public void DefineTinctures()
        {
            var definer = MockVocabulary.Get().GetDefiner();

            var cases = new object[][]
            {
                new object[] { "gules", "red", TinctureType.Colour },
                new object[] { "sable", "black", TinctureType.Colour },
                new object[] { "or", "gold", TinctureType.Metal },
            };

            foreach (object[] testCase in cases)
            {
                var type = (TinctureType)testCase[2];
                Assert.AreEqual(testCase[0], definer.Tincture((string)testCase[1], type));
            }
        }

        // todo: add more test cases?
    }
}
