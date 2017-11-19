using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.LexicalAnalysis;
using System.Collections.Generic;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.SyntacticAnalysis;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Elements;

namespace HeraldryTest
{
    /// <summary>
    /// Unit test for syntactic analyzer.
    /// </summary>
    [TestClass]
    public class SyntacticAnalyzerTest
    {
        /// <summary>
        /// Feed the analyzer with definition on tincture only coat of arms and see what happens.
        /// </summary>
        [TestMethod]
        public void TestTinctureOnly()
        {
            // prepare data
            List<Token> tokens = new List<Token>();
            Token background = new Token();
            TinctureDefinition tincture = new TinctureDefinition();
            tincture.Text = "AZURE";
            tincture.Type = TinctureType.Colour;
            background.Definition = tincture;
            tokens.Add(background);

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Background);
            Filling bg = blazon.CoatOfArms.Content.Background;
            CheckFillingColour(TinctureType.Colour, tincture.Text, bg);
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision1()
        {
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition();
            divisionDefinition.Type = FieldDivisionType.Quarterly;
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of two colours
            TinctureDefinition tincture1 = new TinctureDefinition();
            tincture1.Text = "AZURE";
            tincture1.Type = TinctureType.Colour;
            TinctureDefinition tincture2 = new TinctureDefinition();
            tincture1.Text = "OR";
            tincture2.Type = TinctureType.Colour;
            tokens.Add(new Token(1, tincture1));
            tokens.Add(new Token(2, new KeyWordDefinition(KeyWord.And)));
            tokens.Add(new Token(3,tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
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

            CheckFillingColour(TinctureType.Colour, tincture1.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t4);
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by four colours.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision2()
        {
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition();
            divisionDefinition.Type = FieldDivisionType.Quarterly;
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of two colours
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(new Token(1, 1));
            tokens.Add(new Token(2, tincture1));
            tokens.Add(new Token(3, KeyWord.Semicolon));
            tokens.Add(new Token(4, 2));
            tokens.Add(new Token(5, tincture2));
            tokens.Add(new Token(6, KeyWord.Semicolon));
            tokens.Add(new Token(7, 3));
            tokens.Add(new Token(8, tincture3));
            tokens.Add(new Token(9, KeyWord.Semicolon));
            tokens.Add(new Token(10, 4));
            tokens.Add(new Token(11, tincture4));
            tokens.Add(new Token(12, KeyWord.Semicolon));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
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

            CheckFillingColour(TinctureType.Colour, tincture1.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture3.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t4);
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
            Assert.AreEqual(expectedType, tDef.Type);
            Assert.AreEqual(expectedText, tDef.Text);
        }
    }
}
