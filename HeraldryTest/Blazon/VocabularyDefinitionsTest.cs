using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary.Entries;

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
            var a = new NumberDefinition { Type = NumberType.Cardinal, Value = 37 };
            var b = new NumberDefinition { Type = NumberType.Cardinal, Value = 37 };

            Assert.AreEqual<Definition>(a, b);
        }


    }
}
