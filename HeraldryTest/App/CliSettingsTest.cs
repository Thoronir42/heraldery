using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Rendering;
using Heraldry;
using Heraldry.App;

namespace HeraldryTest.App
{
    [TestClass]
    public class CliSettingsTest
    {
        [TestMethod]
        public void TestRenderType()
        {
            CliSettings settings = new CliSettings("-r", "Text", "in-file");
            Assert.AreEqual(RenderType.Text, settings.RenderType);

            settings = new CliSettings("-r", "Svg", "in-file");
            Assert.AreEqual(RenderType.Svg, settings.RenderType);
        }

        [TestMethod]
        public void TestCompleteSettings()
        {
            CliSettings settings = new CliSettings("-l", "en_olde", "-r", "Text", "Arms of Churchil.txt");

            Assert.AreEqual("en_olde", settings.Language);
            Assert.AreEqual(RenderType.Text, settings.RenderType);
            Assert.AreEqual("Arms of Churchil.txt", settings.InputFile);
        }

        [TestMethod]
        public void TestOutputFile()
        {
            CliSettings settings = new CliSettings("Peepo.txt")
            {
                RenderType = RenderType.Svg
            };

            Assert.AreEqual("Peepo.svg", settings.OutputFile);
        }

    }
}
