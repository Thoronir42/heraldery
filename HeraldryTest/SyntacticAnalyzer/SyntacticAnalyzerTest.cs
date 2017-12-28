using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heraldry.LexicalAnalysis;
using System.Collections.Generic;
using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.SyntacticAnalysis;
using Heraldry.Blazon.Structure;
using Heraldry.Blazon.Elements;
using Heraldry.Blazon.Charges;
using HeraldryTest.SyntacticAnalysis;

namespace HeraldryTest
{
    /// <summary>
    /// Unit test for syntactic analyzer.
    /// </summary>
    [TestClass]
    public class SyntacticAnalyzerTest
    {
        TokenCreator Token = new TokenCreator();

        /// <summary>
        /// Feed the analyzer with definition on tincture only coat of arms and see what happens.
        /// </summary>
        [TestMethod]
        public void TestTinctureOnly()
        {
            // prepare data
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.Tincture(TinctureType.Colour, "AZURE"));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
            Assert.IsInstanceOfType(blazon.CoatOfArms.Content, typeof(ContentField));

            ContentField content = blazon.CoatOfArms.Content as ContentField;

            Assert.IsNotNull(content.Background);
            CheckFillingColour(TinctureType.Colour, "AZURE", content.Background);
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision1()
        {
            // prepare data
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));

            // quaterly division qhich consists of two colours
            TinctureDefinition[] tinctures = {
                new TinctureDefinition { Text = "AZURE", TinctureType = TinctureType.Colour },
                new TinctureDefinition { Text = "OR", TinctureType = TinctureType.Colour },
                null, null
            };
            tinctures[2] = tinctures[1];
            tinctures[3] = tinctures[0];

            tokens.Add(new Token(10, tinctures[0]));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(new Token(30, tinctures[1]));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            Assert.IsNotNull(coa.Division);

            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            for (int i = 0; i < 4; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tinctures[i].Text, subfield.Background);
            }
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
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));

            // quaterly division qhich consists of four colours
            TinctureDefinition[] tinctures = {
                new TinctureDefinition(TinctureType.Colour, "AZURE"),
                new TinctureDefinition(TinctureType.Colour, "OR"),
                new TinctureDefinition(TinctureType.Colour, "GULES"),
                new TinctureDefinition(TinctureType.Colour, "SABLE"),
            };

            tokens.Add(Token.Number(1));
            tokens.Add(new Token(2, tinctures[0]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(2));
            tokens.Add(new Token(5, tinctures[1]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(3));
            tokens.Add(new Token(8, tinctures[2]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(4));
            tokens.Add(new Token(11, tinctures[3]));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            Assert.IsNotNull(coa.Division);

            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            for (int i = 0; i < 4; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tinctures[i].Text, subfield.Background);
            }
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
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));

            // quaterly division qhich consists of four colours
            TinctureDefinition[] tinctures = {
                new TinctureDefinition(TinctureType.Colour, "AZURE"),
                new TinctureDefinition(TinctureType.Colour, "OR"),
                new TinctureDefinition(TinctureType.Colour, "GULES"),
                new TinctureDefinition(TinctureType.Colour, "SABLE"),
            };

            tokens.Add(Token.Number(4));
            tokens.Add(new Token(2, tinctures[0]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(2));
            tokens.Add(new Token(5, tinctures[1]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(new Token(8, tinctures[2]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(3));
            tokens.Add(new Token(11, tinctures[3]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            Assert.IsNotNull(coa.Division);

            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            int[] ttn = { 2, 1, 3, 0 };
            for (int i = 0; i < 4; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tinctures[ttn[i]].Text, subfield.Background);
            }
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by mixed combinations of numbers and colours.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision4()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 colour
            TinctureDefinition[] tinctures = {
                new TinctureDefinition(TinctureType.Colour, "AZURE"),
                new TinctureDefinition(TinctureType.Colour, "OR"),
                null, null
            };
            tinctures[3] = tinctures[2] = tinctures[0];

            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(new Token(6, tinctures[0]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(new Token(9, tinctures[1]));
            //tokens.Add(new Token{Position = 10, Definition = KeyWord.Semicolon});

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            Assert.IsNotNull(coa.Division);

            Assert.AreEqual(FieldDivisionType.Quarterly, coa.Division);
            Assert.IsNotNull(coa.Subfields);
            Assert.AreEqual(4, coa.Subfields.Length);

            int[] ttn = { 1, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tinctures[ttn[i]].Text, subfield.Background);
            }
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by mixed combinations of numbers and colours and nested quaterly division.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision5()
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 quaterly 4 and 3 and 2 colour; 1 colour
            // note that the nested division is in the 1st field
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(new Token(6, tincture1));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));
            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(new Token(15, tincture1));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(new Token(18, tincture2));
            //tokens.Add(new Token{Position = 19, Definition = KeyWord.Semicolon});
            //tokens.Add(new Token{Position = 20, Definition = KeyWord.Semicolon});

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            CheckFieldDivision(coa, FieldDivisionType.Quarterly, 4);

            ContentField[] subfields = coa.Subfields as ContentField[];

            // check colors in quaterly division
            for (int i = 1; i < 4; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tincture1.Text, subfield.Background);
            }

            // check nested quaterly division
            DividedField f1 = coa.Subfields[0] as DividedField;
            Assert.IsNotNull(f1.Subfields);
            Assert.AreEqual(4, f1.Subfields.Length);

            CheckFillingColour(TinctureType.Colour, tincture2.Text, (f1.Subfields[0] as ContentField).Background);
            for (int i = 1; i < 4; i++)
            {
                ContentField subfield = f1.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tincture1.Text, subfield.Background);
            }

        }

        /// <summary>
        /// Feed the parser with some basic variation and check the output.
        /// </summary>
        [TestMethod]
        public void TestVariation1()
        {
            List<Token> tokens = new List<Token>();

            // paly of three azure and or
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(Token.FieldVariation(FieldVariationType.PalyOf));
            tokens.Add(Token.Number(3));
            tokens.Add(new Token(2, tincture1));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(new Token(4, tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            ContentField field = blazon.CoatOfArms.Content as ContentField;
            Assert.IsNotNull(field.Background);
            Filling variatedBackground = field.Background;

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
        /// Create one tincture field with ordinary charge, feed it to parser and check the results.
        /// </summary>
        [TestMethod]
        public void TestSimpleFiledWithOrdinaryCharge1()
        {
            List<Token> tokens = new List<Token>();

            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");

            // create token list
            // blazon: azure honourable bend or
            tokens.Add(new Token(0, tincture1));
            tokens.Add(Token.Ordinary(Ordinary.Bend, OrdinarySize.Honourable));
            tokens.Add(new Token(30, tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            ContentField content = blazon.CoatOfArms.Content as ContentField;
            CheckFillingColour(tincture1.TinctureType, tincture1.Text, content.Background);
            CheckOrdinaryCharge(content, Ordinary.Bend, OrdinarySize.Honourable, new Filling { Layout = FillingLayout.Solid(), Tinctures = new TinctureDefinition[] { tincture2 } });
        }

        /// <summary>
        /// Create variated field with ordinary charge, feed it to parser and check the results.
        /// </summary>
        [TestMethod]
        public void TestVariatedFieldWithOrdinaryCharge()
        {
            List<Token> tokens = new List<Token>();

            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "OR");

            int variationNumber = 3;

            // create token list
            // blazon: azure honourable bend or
            TokenListBuilder tokenBuilder = new TokenListBuilder()
                .Add(new FieldVariationDefinition { VariationType = FieldVariationType.BarryOf })
                .Add(new NumberDefinition { Value = variationNumber })
                .Add(tincture1)
                .Add(new KeyWordDefinition { KeyWord = KeyWord.And })
                .Add(tincture2)
                .Add(new OrdinaryDefinition { Type = Ordinary.Bend, Size = OrdinarySize.Honourable })
                .Add(tincture3);
            tokens = tokenBuilder.Build();


            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);
            Filling expectedFilling = new Filling
            {
                Layout = new FillingLayout { FillingLayoutType = FillingLayoutType.BarryOf, Number = variationNumber, Charge = null },
                Tinctures = new TinctureDefinition[] { tincture1, tincture2 }
            };

            // check the result
            CheckBlazonInstanceContent(blazon);
            ContentField content = blazon.CoatOfArms.Content as ContentField;
            Assert.IsNotNull(content.Background);
            CheckFilling(content.Background, expectedFilling);
            CheckOrdinaryCharge(content, Ordinary.Bend, OrdinarySize.Honourable, new Filling { Layout = FillingLayout.Solid(), Tinctures = new TinctureDefinition[] { tincture3 } });
        }

        /// <summary>
        /// Create divided field with ordinary charge, feed it to parser and check the results.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void TestDividedFieldWithOrdinaryCharge()
        {
            List<Token> tokens = new List<Token>();

            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "OR");
            FieldDivisionDefinition fieldDivisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            OrdinaryDefinition ordinaryDefinition = new OrdinaryDefinition { Type = Ordinary.Bend, Size = OrdinarySize.Honourable };

            // create token list
            // blazon: azure honourable bend or
            TokenListBuilder tokenBuilder = new TokenListBuilder()
                .Add(fieldDivisionDefinition)
                .Add(tincture1)
                .Add(new KeyWordDefinition { KeyWord = KeyWord.And })
                .Add(tincture2)
                .Add(ordinaryDefinition)
                .Add(tincture3);
            tokens = tokenBuilder.Build();


            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the results
            CheckBlazonInstanceContent(blazon);
            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            CheckFieldDivision(coa, FieldDivisionType.Quarterly, 4);

            /*Filling t1 = coa.Subfields[0].Background;
            Filling t2 = coa.Subfields[1].Background;
            Filling t3 = coa.Subfields[2].Background;
            Filling t4 = coa.Subfields[3].Background;

            CheckFillingColour(TinctureType.Colour, tincture1.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture2.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture1.Text, t4);*/

            Assert.Inconclusive("Todo: field is expected to be both divided and contentful");
            //CheckOrdinaryCharge(coa, ordinaryDefinition, new Filling { Layout = FillingLayout.Solid(), Tinctures = new TinctureDefinition[] { tincture3 } });
        }

        /// <summary>
        /// Creates a field with party per * division defined by two colors, parses it a checks results.
        /// </summary>
        /// <param name="ppType">Particular pp type to be tested.</param>
        private void TestPartyPerSomethingTwoColoursDivision(FieldDivisionType ppType)
        {
            Assert.IsTrue(ppType.IsPartyPerDivision());
            List<Token> tokens = new List<Token>();

            TinctureDefinition[] tinctures ={
                new TinctureDefinition(TinctureType.Colour, "AZURE"),
                new TinctureDefinition(TinctureType.Colour, "OR"),
            };

            // create token list
            tokens.Add(Token.FieldDivision(ppType));
            tokens.Add(new Token(10, tinctures[0]));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(new Token(30, tinctures[1]));


            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            CheckFieldDivision(coa, ppType, 2);

            for (int i = 0; i < 2; i++)
            {
                ContentField subfield = coa.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tinctures[i].Text, subfield.Background);
            }
        }

        /// <summary>
        /// Creates a field with party per * division defined by two various fields.
        /// </summary>
        /// <param name="ppType">Particular pp type to be tested.</param>
        private void TestPartyPerSomethinComplexDivision(FieldDivisionType ppType)
        {
            Assert.IsTrue(ppType.IsPartyPerDivision());

            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");

            // create token list
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.FieldDivision(ppType));

            // first field will be variated: paly of three azure and or
            tokens.Add(Token.FieldVariation(FieldVariationType.PalyOf));
            tokens.Add(Token.Number(3));
            tokens.Add(new Token(3, tincture1));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(new Token(5, tincture2));

            tokens.Add(Token.Keyword(KeyWord.And));

            // second field will be quarterly divided
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));
            tokens.Add(new Token(8, tincture3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(new Token(10, tincture4));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            CheckFieldDivision(coa, ppType, 2);

            // variated field
            ContentField f1 = coa.Subfields[0] as ContentField;
            Filling variatedBackground = f1.Background;

            Assert.AreEqual(FillingLayoutType.PalyOf, variatedBackground.Layout.FillingLayoutType);
            Assert.AreEqual(3, variatedBackground.Layout.Number);
            Assert.AreEqual(2, variatedBackground.Tinctures.Length);
            Assert.AreEqual(tincture1, variatedBackground.Tinctures[0]);
            Assert.AreEqual(tincture2, variatedBackground.Tinctures[1]);

            // quarterly divided field
            DividedField f2 = coa.Subfields[1] as DividedField;
            Assert.IsNotNull(f2.Subfields);
            Assert.AreEqual(4, f2.Subfields.Length);

            /*Filling t1 = f2.Subfields[0].Background;
            Filling t2 = f2.Subfields[1].Background;
            Filling t3 = f2.Subfields[2].Background;
            Filling t4 = f2.Subfields[3].Background;
            CheckFillingColour(TinctureType.Colour, tincture3.Text, t1);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t2);
            CheckFillingColour(TinctureType.Colour, tincture4.Text, t3);
            CheckFillingColour(TinctureType.Colour, tincture3.Text, t4);*/
        }

        /// <summary>
        /// Checks if the field's charge isn't null, if it's expected type, has expected size and color.
        /// 
        /// </summary>
        /// <param name="field">Field to be checked.</param>
        /// <param name="expectedOrdinary">Definition containing expected type and size.</param>
        /// <param name="expectedFilling">Expected filling.</param>
        private void CheckOrdinaryCharge(ContentField field, Ordinary type, OrdinarySize size, Filling expectedFilling)
        {
            Assert.IsNotNull(field.Charge);
            Assert.IsTrue(field.Charge.GetType() == typeof(OrdinaryCharge));
            OrdinaryCharge ordinaryCharge = (OrdinaryCharge)field.Charge;

            Assert.AreEqual(type, ordinaryCharge.OrdinaryType);
            Assert.AreEqual(size, ordinaryCharge.OrdinarySize);

            CheckFilling(ordinaryCharge.Filling, expectedFilling);
        }

        /// <summary>
        /// Checks the filling.
        /// </summary>
        /// <param name="filling">Filling to be checked.</param>
        /// <param name="expectedVariation">Filling which contains expected variation details.</param>
        private void CheckFilling(Filling filling, Filling expectedFilling)
        {
            Assert.IsNotNull(filling);
            Assert.AreEqual(expectedFilling.Layout, filling.Layout);

            Assert.IsNotNull(filling.Tinctures);
            TinctureDefinition[] expectedTinctures = expectedFilling.Tinctures;
            TinctureDefinition[] tinctures = filling.Tinctures;
            Assert.AreEqual(expectedTinctures.Length, tinctures.Length);
            for (int i = 0; i < expectedFilling.Tinctures.Length; i++)
            {
                Assert.AreEqual(expectedTinctures[i], tinctures[i]);
            }
        }

        /// <summary>
        /// Checks if the division of the field isn't null, if it's the right type and has expected number of subfields.
        /// 
        /// </summary>
        /// <param name="field">Field to be checked.</param>
        /// <param name="expectedDivisionType">Expected type of division.</param>
        /// <param name="expectedSubfieldCount">Expected number of subfields.</param>
        private void CheckFieldDivision(DividedField field, FieldDivisionType expectedDivisionType, int expectedSubfieldCount)
        {
            Assert.IsNotNull(field.Division);
            Assert.AreEqual(expectedDivisionType, field.Division);
            Assert.IsNotNull(field.Subfields);
            Assert.AreEqual(expectedSubfieldCount, field.Subfields.Length);
        }

        /// <summary>
        /// Checks if blazon instance, its coat of arms and its content aren't null.
        /// </summary>
        /// <param name="blazon">Blazon object to be checked.</param>
        private void CheckBlazonInstanceContent(BlazonInstance blazon)
        {
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);
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

    /// <summary>
    /// Helper class to build lists of tokens.
    /// </summary>
    class TokenListBuilder
    {
        /// <summary>
        /// List of tokens which will builded by this class.
        /// </summary>
        private List<Token> tokens;

        /// <summary>
        /// Position of the token which was added last.
        /// 
        /// </summary>
        private int lastPos;

        public TokenListBuilder()
        {
            this.tokens = new List<Token>();
            lastPos = -1;
        }

        /// <summary>
        /// Add token to  the result list.
        /// </summary>
        /// <param name="token">Token to be added.</param>
        /// <returns>Instance of this builder.</returns>
        public TokenListBuilder Add(Token token)
        {
            tokens.Add(token);
            lastPos = token.Position;
            return this;
        }

        /// <summary>
        /// Creates a new token with provided definition. Position of the last inserted token incremented by 1 (or zero if this is
        /// the first token) is used as a position for this new token.
        /// </summary>
        /// <param name="definition">Definition to be added.</param>
        /// <returns>Instance of this builder.</returns>
        public TokenListBuilder Add(Definition definition)
        {
            return Add(new Token(++lastPos, definition));
        }

        /// <summary>
        /// Returns the list of added tokens.
        /// </summary>
        /// <returns>List of tokens.</returns>
        public List<Token> Build()
        {
            return tokens;
        }
    }
}
