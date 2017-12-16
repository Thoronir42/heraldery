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
            Assert.IsInstanceOfType(blazon.CoatOfArms.Content, typeof(ContentField));

            ContentField content = blazon.CoatOfArms.Content as ContentField;
            Assert.IsNotNull(content.Background);
            Filling bg = content.Background;
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

            string[] expectedTinctures = new String[] { "azure", "or", "or", "azure" };
            string input = "Quarterly azure and or.";

            List<Token> parsedText = analyzer.ParseText(input);
            Assert.AreEqual(5, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.ParseTokens(parsedText);

            // check results
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsInstanceOfType(blazon.CoatOfArms.Content, typeof(DividedField));
            
            DividedField coa = blazon.CoatOfArms.Content as DividedField;

            Assert.IsNotNull(coa.Division);
            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            for(int i = 0; i < 4; i++)
            {
                Assert.IsInstanceOfType(coa.Subfields[i], typeof(ContentField));
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, expectedTinctures[i], subfield.Background);
            }
        }

        /// <summary>
        /// Test quarterly division defined by four colors.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDividedBlazon2()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary(), CreateNumberParser());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string[] expectedTinctures = { "azure", "gules", "or", "sable" };
            string input = "Quarterly 1st azure; 3rd or; 2nd gules; 4th sable.";

            List<Token> parsedText = analyzer.ParseText(input);
            Assert.AreEqual(13, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.ParseTokens(parsedText);

            // check results
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsInstanceOfType(blazon.CoatOfArms.Content, typeof(DividedField));

            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            for (int i = 0; i < 4; i++)
            {
                Assert.IsInstanceOfType(coa.Subfields[i], typeof(ContentField));
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, expectedTinctures[i], subfield.Background);
            }
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
