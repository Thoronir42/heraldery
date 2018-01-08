using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using Heraldry.SyntacticAnalysis;
using System.Collections.Generic;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Vocabulary.Entries;
using HeraldryTest.Blazon;
using HeraldryTest.Helpers;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Rendering.Text;
using System.IO;

namespace HeraldryTest.App
{
    /// <summary>
    /// This unit test is aimed to test combined functionality of app's components - namely lexical and syntactic analyzer.
    /// </summary>
    [TestClass]
    public class AppTest
    {
        /// <summary>
        /// Just single color background.
        /// </summary>
        [TestMethod]
        public void TestSimpleBlazon1()
        {
            LexAnalyzer analyzer = new LexAnalyzer(MockVocabulary.Get());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string input = "Or";

            List<Token> parsedText = analyzer.Execute(input);
            Assert.AreEqual(1, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.Execute(parsedText);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsInstanceOfType(blazon.CoatOfArms.Content, typeof(ContentField));

            ContentField content = blazon.CoatOfArms.Content as ContentField;
            Assert.IsNotNull(content.Background);

            var expectedFilling = new SolidFilling(new Tincture(TinctureType.Metal, "gold"));
            Assert.AreEqual(expectedFilling, content.Background);
        }

        /// <summary>
        /// Simple quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDividedBlazon1()
        {
            LexAnalyzer analyzer = new LexAnalyzer(MockVocabulary.Get());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

            string[] expectedTinctures = new String[] { "blue", "gold", "gold", "blue" };
            TinctureType[] expectedTypes = { TinctureType.Colour, TinctureType.Metal, TinctureType.Metal, TinctureType.Colour };
            string input = "Quarterly azure and or.";

            List<Token> parsedText = analyzer.Execute(input);
            Assert.AreEqual(5, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.Execute(parsedText);

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

            for (int i = 0; i < 4; i++)
            {
                Assert.IsInstanceOfType(coa.Subfields[i], typeof(ContentField));
                ContentField subfield = coa.Subfields[i] as ContentField;

                var expectedFilling = new SolidFilling(new Tincture(expectedTypes[i], expectedTinctures[i]));
                Assert.AreEqual(expectedFilling, subfield.Background);
            }
        }

        /// <summary>
        /// Test quarterly division defined by four colors.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDividedBlazon2()
        {
            LexAnalyzer analyzer = new LexAnalyzer(MockVocabulary.Get());
            SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer(false);

            string[] expectedTinctures = { "blue", "red", "gold", "black" };
            TinctureType[] expectedTypes = { TinctureType.Colour, TinctureType.Colour, TinctureType.Metal, TinctureType.Colour };
            string input = "Quarterly 1st azure; 3rd or; 2nd gules; 4th sable.";

            List<Token> parsedText = analyzer.Execute(input);
            Assert.AreEqual(13, parsedText.Count);

            BlazonInstance blazon = syntacticAnalyzer.Execute(parsedText);

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

                var expectedFilling = new SolidFilling(new Tincture(expectedTypes[i], expectedTinctures[i]));
                Assert.AreEqual(expectedFilling, subfield.Background);
            }
        }


        [Ignore][TestMethod]
        public void TestReLexical()
        {
            var testInput = "Quarterly: first and fourth gules, a lion rampant queue forchée argent armed, langued and crowned Or (Bohemia); second azure, an eagle displayed chequé gules and argent armed, langued and crowned Or (Moravia); third Or, an eagle displayed sable armed and langued gules crowned or (Silesia).";

            MemoryStream stream = new MemoryStream();

            var vocabulary = MockVocabulary.Get();

            var lex = new LexAnalyzer(vocabulary);
            var tokens = lex.Execute(testInput);

            var synt = new SyntacticAnalyzer();
            var instance = synt.Execute(tokens);

            var rend = new TextRenderer(vocabulary.GetDefiner(), stream);
            var result = rend.Execute(instance);

            if (!result.Success)
            {
                Assert.Inconclusive("Rendition failed");
            }

            stream.Position = 0;
            var reTokens = (new LexAnalyzer(vocabulary)).Execute((new StreamReader(stream).ReadToEnd()));

            Assert.AreEqual(tokens.Count, reTokens.Count);

            Assert.AreEqual(tokens, reTokens);
        }
    }
}
