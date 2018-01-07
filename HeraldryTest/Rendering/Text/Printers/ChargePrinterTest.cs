using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Charges;
using System.Collections.Generic;
using Heraldry.Blazon.Charges.Properties;
using HeraldryTest.Helpers;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Elements;
using Heraldry.Rendering.Text.Printers;
using System.IO;

namespace HeraldryTest.Rendering.Text.Printers
{
    [TestClass]
    public class ChargePrinterTest : PrinterTestBase
    {
        [TestMethod]
        public void TestFeatures()
        {
            var features = new List<FeatureProperty>()
            {
                new FeatureProperty(ChargeFeature.Crown, BlazonMock.SolidFilling("black")),
                new FeatureProperty(ChargeFeature.AntlersHorns, BlazonMock.SolidFilling("black")),
                new FeatureProperty(ChargeFeature.Hooves, new PatternFilling(FieldVariationType.Chequy, new Tincture("red"), new Tincture("blue"))),
            };

            var cp = new ChargePrinter(MockRoot());
            
            cp.PrintTincturedFeatures(features);

            Assert.AreEqual("crowned and attired sable unguled chequy gules and azure", GetPrintedText());
        }
    }
}
