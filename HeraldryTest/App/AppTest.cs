using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis.Numbers;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis;
using System.Collections.Generic;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;

namespace HeraldryTest.App
{
    /// <summary>
    /// This unit test is aimed to test combined functionality of app's components - namely lexical and syntactic analyzer.
    /// </summary>
    [TestClass]
    public class AppTest
    {
        private BlazonVocabulary CreateVocabulary()
        {
            var sourcesDirectory = Environment.CurrentDirectory + "\\..\\..\\..";
            return VocabularyLoader.LoadFromDirectory(sourcesDirectory + "\\resources\\en_olde\\");
        }

        private NumberParser CreateNumberParser()
        {
            return new NumberParser_en_olde();
        }

        /// <summary>
        /// Just single color background.
        /// </summary>
        [TestMethod]
        public void TestSimpleBlazon1 ()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary(), CreateNumberParser());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string input = "Or";

            List<Token> parsedText = analyzer.ParseText(input);
            Assert.AreEqual(1, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.ParseTokens(parsedText);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Background);
            Filling bg = blazon.CoatOfArms.Content.Background;
            CheckFillingColour(TinctureType.Colour, "or", bg);
        }

        /// <summary>
        /// Simple quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDividedBlazon1()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary(), CreateNumberParser());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string tincture1 = "azure";
            string tincture2 = "or";
            string input = "Quarterly azure and or.";

            List<Token> parsedText = analyzer.ParseText(input);
            Assert.AreEqual(5, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.ParseTokens(parsedText);

            // check results
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Division);
            Field coa = blazon.CoatOfArms.Content;
            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            Filling t1 = coa.Subfields[0].Background;
            Filling t2 = coa.Subfields[1].Background;
            Filling t3 = coa.Subfields[2].Background;
            Filling t4 = coa.Subfields[3].Background;
            
            CheckFillingColour(TinctureType.Colour, tincture1, t1);
            CheckFillingColour(TinctureType.Colour, tincture2, t2);
            CheckFillingColour(TinctureType.Colour, tincture2, t3);
            CheckFillingColour(TinctureType.Colour, tincture1, t4);
        }

        /// <summary>
        /// Test quarterly division defined by four colors.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDividedBlazon2()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary(), CreateNumberParser());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string tincture1 = "azure";
            string tincture2 = "gules";
            string tincture3 = "or";
            string tincture4 = "sable";
            string input = "Quarterly 1st azure; 3rd or; 2nd gules; 4th sable.";

            List<Token> parsedText = analyzer.ParseText(input);
            Assert.AreEqual(13, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.ParseTokens(parsedText);

            // check results
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Division);
            Field coa = blazon.CoatOfArms.Content;
            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            Filling t1 = coa.Subfields[0].Background;
            Filling t2 = coa.Subfields[1].Background;
            Filling t3 = coa.Subfields[2].Background;
            Filling t4 = coa.Subfields[3].Background;

            CheckFillingColour(TinctureType.Colour, tincture1, t1);
            CheckFillingColour(TinctureType.Colour, tincture2, t2);
            CheckFillingColour(TinctureType.Colour, tincture3, t3);
            CheckFillingColour(TinctureType.Colour, tincture4, t4);
        }

        /// <summary>
        /// Checks whether the filling contains exactly one tincture and has expected type and value.
        /// </summary>
        /// <param name="expectedType">Expected type of filling.</param>
        /// <param name="expectedText">Expected text of filling (probably colour).</param>
        /// <param name="filling">Filling to be checked.</param>
        private void CheckFillingColour(TinctureType expectedType, String expectedText, Filling filling)
        {
            Assert.IsNotNull(filling.Tinctures);
            Assert.AreEqual(1, filling.Tinctures.Length);
            TinctureDefinition tDef = filling.Tinctures[0];
            Assert.AreEqual(expectedType, tDef.TinctureType);
            Assert.AreEqual(expectedText, tDef.Text);
        }
    }
}
