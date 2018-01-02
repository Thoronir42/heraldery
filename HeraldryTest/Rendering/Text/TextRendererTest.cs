using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Heraldry.Rendering.Text;
using HeraldryTest.Blazon;
using Heraldry.Blazon.Structure;
using HeraldryTest.Helpers;

namespace HeraldryTest.Rendering.Text
{
    [TestClass]
    public class TextRendererTest
    {
        [TestMethod]
        public void PrintDividedField()
        {
            MemoryStream stream = new MemoryStream();

            TextRenderer renderer = new TextRenderer(MockVocabulary.Get().GetDefiner())
            {
                PrintStream = stream
            };

            BlazonInstance instance = new BlazonInstance();
            CoatOfArms coa = instance.CoatOfArms = new CoatOfArms();
            coa.Content = new DividedField()
            {
                Division = Heraldry.Blazon.Elements.FieldDivisionType.PartyPerFess,
                Subfields =
                new Field[] {
                    BlazonMock.SolidColorField("blue", Heraldry.Blazon.Elements.TinctureType.Colour),
                    BlazonMock.SolidColorField("gold", Heraldry.Blazon.Elements.TinctureType.Metal),
                }
            };

            renderer.Execute(instance);

            stream.Position = 0;
            String s = (new StreamReader(stream)).ReadToEnd();
            Assert.AreEqual("party per fess azure and or", s);
        }
    }
}
