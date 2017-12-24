using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary.Numbers;

namespace HeraldryTest.Blazon.Numbers
{
    [TestClass]
    public class EnglistNumberVocabularyTest
    {
        private object[][] GetData()
        {
            return new object[][]
            {
                 new object[]{ " 2th 1", 2, NumberType.Ordinal, 1, 3, },
                 new object[]{ "     1", 1, NumberType.Cardinal, 5, 1, },
            };
        }

        [TestMethod]
        public void TestNumbers()
        {
            var numVocabulary = new EnglishNumberVocabulary();

            foreach(var testCase in GetData() )
            {
                var number = numVocabulary.FindInText((string) testCase[0], out int index, out int length);

                Assert.IsNotNull(number);
                Assert.AreEqual(testCase[1], number.Value);
                Assert.AreEqual(testCase[2], number.Type);

                Assert.AreEqual(testCase[3], index);
                Assert.AreEqual(testCase[4], length);
            }
        }

        [TestMethod]
        public void TestFormat()
        {
            var numVocabulary = new EnglishNumberVocabulary();

            Assert.AreEqual("1st", numVocabulary.FormatDigital(1, NumberType.Ordinal));
            Assert.AreEqual("1", numVocabulary.FormatDigital(1, NumberType.Cardinal));

            Assert.AreEqual("9002nd", numVocabulary.FormatDigital(9002, NumberType.Ordinal));
        }
    }
}
