using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Heraldry.Rendering.Text;
using HeraldryTest.Blazon;
using Heraldry.Blazon.Structure;
using HeraldryTest.Helpers;
using HeraldryTest.Rendering.Text.Printers;
using Heraldry.Blazon.Elements;

namespace HeraldryTest.Rendering.Text
{
    [TestClass]
    public class TextRendererTest : PrinterTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public void PrintDividedField()
        {
            TextRenderer renderer = new TextRenderer(MockVocabulary.Get().GetDefiner(), GetStream());

            BlazonInstance instance = new BlazonInstance();
            CoatOfArms coa = instance.CoatOfArms = new CoatOfArms();
            coa.Content = new DividedField(FieldDivisionType.PartyPerFess)
            {
                Subfields =
                new Field[] {
                    BlazonMock.SolidColorField("blue", Heraldry.Blazon.Elements.TinctureType.Colour),
                    BlazonMock.SolidColorField("gold", Heraldry.Blazon.Elements.TinctureType.Metal),
                }
            };

            renderer.Execute(instance);

            var text = GetPrintedText();
            Assert.AreEqual("party per fess azure and or.\r\n", text);
        }

        [TestMethod]
        public void PrintChargeFeatures()
        {

        }
    }
}
