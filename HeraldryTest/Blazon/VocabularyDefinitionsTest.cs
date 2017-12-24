using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.Blazon.Vocabulary.Numbers;

namespace HeraldryTest.Blazon
{
    [TestClass]
    public class VocabularyDefinitionsTest
    {
        [TestMethod]
        public void FieldDivisionDefinitionEquals()
        {
            var a = new FieldDivisionDefinition { Type = Heraldry.Blazon.Elements.FieldDivisionType.PerSaltire };
            var b = new FieldDivisionDefinition { Type = Heraldry.Blazon.Elements.FieldDivisionType.PerSaltire };

            Assert.AreEqual<Definition>(a, b);
        }

        [TestMethod]
        public void NumberDefinitionEquals()
        {
            var a = new NumberDefinition(37, NumberType.Cardinal);
            var b = new NumberDefinition(37, NumberType.Cardinal);

            Assert.AreEqual<Definition>(a, b);
        }

    }
}
