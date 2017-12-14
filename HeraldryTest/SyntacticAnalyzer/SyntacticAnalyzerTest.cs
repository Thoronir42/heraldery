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
            TinctureDefinition tincture = new TinctureDefinition { Text = "AZURE", TinctureType = TinctureType.Colour };
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
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of two colours
            TinctureDefinition tincture1 = new TinctureDefinition { Text = "AZURE", TinctureType = TinctureType.Colour };
            TinctureDefinition tincture2 = new TinctureDefinition();
            tincture1.Text = "OR";
            tincture2.TinctureType = TinctureType.Colour;
            tokens.Add(new Token(10, tincture1));
            tokens.Add(new Token(16, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(30, tincture2));

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
        /// Division is defined by four 'number-colour' pairs.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision2()
        {
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of four colours
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(new Token(1, new NumberDefinition { Value = 1 }));
            tokens.Add(new Token(2, tincture1));
            tokens.Add(SemicolonToken(3));
            tokens.Add(new Token(4, new NumberDefinition { Value = 2 }));
            tokens.Add(new Token(5, tincture2));
            tokens.Add(SemicolonToken(6));
            tokens.Add(new Token(7, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(8, tincture3));
            tokens.Add(SemicolonToken(7));
            tokens.Add(new Token(10, new NumberDefinition { Value = 4 }));
            tokens.Add(new Token(11, tincture4));

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
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by four 'number-colour' pairs but without any order.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision3()
        {
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of four colours
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(new Token(1, new NumberDefinition { Value = 4 }));
            tokens.Add(new Token(2, tincture1));
            tokens.Add(SemicolonToken(3));
            tokens.Add(new Token(4, new NumberDefinition { Value = 2 }));
            tokens.Add(new Token(5, tincture2));
            tokens.Add(SemicolonToken(6));
            tokens.Add(new Token(7, new NumberDefinition { Value = 1 }));
            tokens.Add(new Token(8, tincture3));
            tokens.Add(SemicolonToken(9));
            tokens.Add(new Token(10, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(11, tincture4));
            tokens.Add(SemicolonToken(12));

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

            CheckFillingColour(TinctureType.Colour, tincture3.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t4);
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by mixed combinations of numbers and colours.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision4()
        {
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 colour
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token(1, new NumberDefinition { Value = 4 }));
            tokens.Add(new Token(2, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(3, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(4, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(5, new NumberDefinition { Value = 2 }));
            tokens.Add(new Token(6, tincture1));
            tokens.Add(SemicolonToken(7));
            tokens.Add(new Token(8, new NumberDefinition { Value = 1 }));
            tokens.Add(new Token(9, tincture2));
            //tokens.Add(new Token{Position = 10, Definition = KeyWord.Semicolon});

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

            CheckFillingColour(TinctureType.Colour, tincture2.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t4);
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by mixed combinations of numbers and colours and nested quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision5()
        {
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(0, divisionDefinition));

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 quaterly 4 and 3 and 2 colour; 1 colour
            // note that the nested division is in the 1st field
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token(1, new NumberDefinition { Value = 4 }));
            tokens.Add(new Token(2, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(3, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(4, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(5, new NumberDefinition { Value = 2 }));
            tokens.Add(new Token(6, tincture1));
            tokens.Add(SemicolonToken(7));
            tokens.Add(new Token(8, new NumberDefinition { Value = 1 }));
            tokens.Add(new Token(9, divisionDefinition));
            tokens.Add(new Token(10, new NumberDefinition { Value = 4 }));
            tokens.Add(new Token(11, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(12, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(13, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(14, new NumberDefinition { Value = 2 }));
            tokens.Add(new Token(15, tincture1));
            tokens.Add(SemicolonToken(16));
            tokens.Add(new Token(17, new NumberDefinition { Value = 1 }));
            tokens.Add(new Token(18, tincture2));
            //tokens.Add(new Token{Position = 19, Definition = KeyWord.Semicolon});
            //tokens.Add(new Token{Position = 20, Definition = KeyWord.Semicolon});

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

            // check colors in quaterly division
            Filling t2 = coa.Subfields[1].Background;
            Filling t3 = coa.Subfields[2].Background;
            Filling t4 = coa.Subfields[3].Background;
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t4);

            // check nested quaterly division
            Field f1 = coa.Subfields[0];
            Assert.IsNotNull(f1.Subfields);
            Assert.AreEqual(4, f1.Subfields.Length);
            Filling nt1 = f1.Subfields[0].Background;
            Filling nt2 = f1.Subfields[1].Background;
            Filling nt3 = f1.Subfields[2].Background;
            Filling nt4 = f1.Subfields[3].Background;
            CheckFillingColour(TinctureType.Colour, tincture2.Text, nt1);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, nt2);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, nt3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, nt4);

        }

        /// <summary>
        /// Feed the parser with some basic variation and check the output.
        /// </summary>
        [TestMethod]
        public void TestVariation1()
        {
            List<Token> tokens = new List<Token>();
            FieldVariationDefinition variationDefinition = new FieldVariationDefinition { VariationType = FieldVariationType.PalyOf };

            // paly of three azure and or
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token(0, variationDefinition));
            tokens.Add(new Token(1, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(2, tincture1));
            tokens.Add(new Token(3, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(4, tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Background);
            Filling variatedBackground = blazon.CoatOfArms.Content.Background;

            Assert.AreEqual(FillingLayoutType.PalyOf, variatedBackground.Layout.FillingLayoutType);
            Assert.AreEqual(3, variatedBackground.Layout.Number);
            Assert.AreEqual(2, variatedBackground.Tinctures.Length);
            Assert.AreEqual(tincture1, variatedBackground.Tinctures[0]);
            Assert.AreEqual(tincture2, variatedBackground.Tinctures[1]);
        }


        /// <summary>
        /// Feed the parser with some basic party per division definitions.
        /// </summary>
        [TestMethod]
        public void TestPartyPerDivision1()
        {
            // test all pp types for two colors
            TestPartyPerSomethingTwoColoursDivision(FieldDivisionType.PartyPerBend);
            TestPartyPerSomethingTwoColoursDivision(FieldDivisionType.PartyPerPale);
            TestPartyPerSomethingTwoColoursDivision(FieldDivisionType.PartyPerBendSinister);
            TestPartyPerSomethingTwoColoursDivision(FieldDivisionType.PartyPerChevron);
            TestPartyPerSomethingTwoColoursDivision(FieldDivisionType.PartyPerFess);
        }

        /// <summary>
        /// Feed the parser with party per division definitions with nested fields.
        /// </summary>
        [TestMethod]
        public void TestPartyPerDivision2()
        {
            // test all pp types for nested fields
            TestPartyPerSomethinComplexDivision(FieldDivisionType.PartyPerBend);
            TestPartyPerSomethinComplexDivision(FieldDivisionType.PartyPerPale);
            TestPartyPerSomethinComplexDivision(FieldDivisionType.PartyPerBendSinister);
            TestPartyPerSomethinComplexDivision(FieldDivisionType.PartyPerChevron);
            TestPartyPerSomethinComplexDivision(FieldDivisionType.PartyPerFess);
        }

        /// <summary>
        /// Creates a field with party per * division defined by two colors, parses it a checks results.
        /// </summary>
        /// <param name="ppType">Particular pp type to be tested.</param>
        private void TestPartyPerSomethingTwoColoursDivision(FieldDivisionType ppType)
        {
            Assert.IsTrue(ppType.IsPartyPerDivision());
            List<Token> tokens = new List<Token>();

            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = ppType };

            // create token list
            tokens.Add(new Token(0, divisionDefinition));
            tokens.Add(new Token(10, tincture1));
            tokens.Add(new Token(16, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(30, tincture2));


            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Division);
            Field coa = blazon.CoatOfArms.Content;
            Assert.AreEqual(ppType, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(2, coa.Subfields.Length);

            Filling f1 = coa.Subfields[0].Background;
            Filling f2 = coa.Subfields[1].Background;
            CheckFillingColour(TinctureType.Colour, tincture1.Text, f1);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, f2);
        }

        /// <summary>
        /// Creates a field with party per * division defined by two various fields.
        /// </summary>
        /// <param name="ppType">Particular pp type to be tested.</param>
        private void TestPartyPerSomethinComplexDivision(FieldDivisionType ppType)
        {
            Assert.IsTrue(ppType.IsPartyPerDivision());
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = ppType };

            // create token list
            tokens.Add(new Token(0, divisionDefinition));

            // first field will be variated: paly of three azure and or
            FieldVariationDefinition variationDefinition = new FieldVariationDefinition { VariationType = FieldVariationType.PalyOf };
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token(1, variationDefinition));
            tokens.Add(new Token(2, new NumberDefinition { Value = 3 }));
            tokens.Add(new Token(3, tincture1));
            tokens.Add(new Token(4, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(5, tincture2));

            tokens.Add(new Token(6, new KeyWordDefinition { KeyWord = KeyWord.And }));

            // second field will be quarterly divided
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            FieldDivisionDefinition quarterlyDivsion = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token(7, quarterlyDivsion));
            tokens.Add(new Token(8, tincture3));
            tokens.Add(new Token(9, new KeyWordDefinition { KeyWord = KeyWord.And }));
            tokens.Add(new Token(10, tincture4));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.ParseTokens(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsNotNull(blazon.CoatOfArms.Content.Division);
            Field coa = blazon.CoatOfArms.Content;
            Assert.AreEqual(ppType, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(2, coa.Subfields.Length);

            // variated field
            Field f1 = coa.Subfields[0];
            Filling variatedBackground = f1.Background;
            Assert.AreEqual(FillingLayoutType.PalyOf, variatedBackground.Layout.FillingLayoutType);
            Assert.AreEqual(3, variatedBackground.Layout.Number);
            Assert.AreEqual(2, variatedBackground.Tinctures.Length);
            Assert.AreEqual(tincture1, variatedBackground.Tinctures[0]);
            Assert.AreEqual(tincture2, variatedBackground.Tinctures[1]);

            // quarterly divided field
            Field f2 = coa.Subfields[1];
            Assert.IsNotNull(f2.Subfields);
            Assert.AreEqual(4, f2.Subfields.Length);
            Filling t1 = f2.Subfields[0].Background;
            Filling t2 = f2.Subfields[1].Background;
            Filling t3 = f2.Subfields[2].Background;
            Filling t4 = f2.Subfields[3].Background;
            CheckFillingColour(TinctureType.Colour, tincture3.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture3.Text, t4);
        }


        /// <summary>
        /// Creates a new semicolon token and returns it.
        /// </summary>
        /// <param name="position">Token's position.</param>
        /// <returns></returns>
        private Token SemicolonToken(int position)
        {
            return new Token(position, new SeparatorDefinition { Separator = Separator.Semicolon });
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
