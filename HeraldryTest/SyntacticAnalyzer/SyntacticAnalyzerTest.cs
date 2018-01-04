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
using HeraldryTest.Helpers;
using Heraldry.Blazon.Structure.Fillings;
using Heraldry.Blazon.Vocabulary.Numbers;

namespace HeraldryTest
{
    /// <summary>
    /// Unit test for syntactic analyzer.
    /// </summary>
    [TestClass]
    public class SyntacticAnalyzerTest
    {
        TokenCreator Token;

        [TestInitialize]
        public void InitTest()
        {
            Token = new TokenCreator();
        }

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
            Tincture[] tinctures = {
                new Tincture(TinctureType.Colour, "AZURE"),
                new Tincture(TinctureType.Colour, "OR"),
                null, null
            };
            tinctures[2] = tinctures[1];
            tinctures[3] = tinctures[0];

            tokens.Add(Token.Tincture(tinctures[0]));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Tincture(tinctures[1]));

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
                CheckFillingColour(TinctureType.Colour, tinctures[i].Value, subfield.Background);
            }
        }

        /// <summary>
        /// Feed the analyzer with definition of coa with quaterly division.
        /// Division is defined by four 'number-colour' pairs.
        /// </summary>
        [TestMethod]
        public void TestQuaterlyDivision2()
        {
            // quaterly division qhich consists of four colours
            Tincture[] tinctures = {
                new Tincture(TinctureType.Colour, "AZURE"),
                new Tincture(TinctureType.Colour, "OR"),
                new Tincture(TinctureType.Colour, "GULES"),
                new Tincture(TinctureType.Colour, "SABLE"),
            };

            // prepare data
            List<Token> tokens = new List<Token>
            {
                Token.FieldDivision(FieldDivisionType.Quarterly),

                Token.Number(1),
                Token.Tincture(tinctures[0]),
                Token.Separator(Separator.Semicolon),

                Token.Number(2),
                Token.Tincture(tinctures[1]),
                Token.Separator(Separator.Semicolon),

                Token.Number(3),
                Token.Tincture(tinctures[2]),
                Token.Separator(Separator.Semicolon),

                Token.Number(4),
                Token.Tincture(tinctures[3])
            };

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
                CheckFillingColour(TinctureType.Colour, tinctures[i].Value, subfield.Background);
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
            Tincture[] tinctures = {
                new Tincture(TinctureType.Colour, "AZURE"),
                new Tincture(TinctureType.Colour, "OR"),
                new Tincture(TinctureType.Colour, "GULES"),
                new Tincture(TinctureType.Colour, "SABLE"),
            };

            tokens.Add(Token.Number(4));
            tokens.Add(Token.Tincture(tinctures[0]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(2));
            tokens.Add(Token.Tincture(tinctures[1]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(Token.Tincture(tinctures[2]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(3));
            tokens.Add(Token.Tincture(tinctures[3]));
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
                CheckFillingColour(TinctureType.Colour, tinctures[ttn[i]].Value, subfield.Background);
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
            Tincture[] tinctures = {
                new Tincture(TinctureType.Colour, "AZURE"),
                new Tincture(TinctureType.Colour, "OR"),
                null, null
            };
            tinctures[3] = tinctures[2] = tinctures[0];

            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(Token.Tincture(tinctures[0]));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(Token.Tincture(tinctures[1]));
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
                CheckFillingColour(TinctureType.Colour, tinctures[ttn[i]].Value, subfield.Background);
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
            Tincture tincture1 = new Tincture(TinctureType.Colour, "AZURE");
            Tincture tincture2 = new Tincture(TinctureType.Colour, "OR");
            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(Token.Tincture(tincture1));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));
            tokens.Add(Token.Number(4));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Number(2));
            tokens.Add(Token.Tincture(tincture1));
            tokens.Add(Token.Separator(Separator.Semicolon));

            tokens.Add(Token.Number(1));
            tokens.Add(Token.Tincture(tincture2));
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
                CheckFillingColour(TinctureType.Colour, tincture1.Value, subfield.Background);
            }

            // check nested quaterly division
            DividedField f1 = coa.Subfields[0] as DividedField;
            Assert.IsNotNull(f1.Subfields);
            Assert.AreEqual(4, f1.Subfields.Length);

            CheckFillingColour(TinctureType.Colour, tincture2.Value, (f1.Subfields[0] as ContentField).Background);
            for (int i = 1; i < 4; i++)
            {
                ContentField subfield = f1.Subfields[i] as ContentField;
                CheckFillingColour(TinctureType.Colour, tincture1.Value, subfield.Background);
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
            Tincture tincture1 = new Tincture(TinctureType.Colour, "AZURE");
            Tincture tincture2 = new Tincture(TinctureType.Colour, "OR");
            tokens.Add(Token.FieldVariation(FieldVariationType.PalyOf));
            tokens.Add(Token.Number(4));
            tokens.Add(Token.Tincture(tincture1));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Tincture(tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            Assert.IsNotNull(blazon);
            Assert.IsNotNull(blazon.CoatOfArms);
            Assert.IsNotNull(blazon.CoatOfArms.Content);

            ContentField field = blazon.CoatOfArms.Content as ContentField;
            Assert.IsInstanceOfType(field.Background, typeof(PatternFilling));
            PatternFilling variatedBackground = field.Background as PatternFilling;

            Assert.AreEqual(FieldVariationType.PalyOf, variatedBackground.Type);
            Assert.AreEqual(4, variatedBackground.Number);
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

            Tincture tincture1 = new Tincture(TinctureType.Colour, "AZURE");
            Tincture tincture2 = new Tincture(TinctureType.Colour, "OR");

            // create token list
            // blazon: azure honourable bend or
            tokens.Add(Token.Tincture(tincture1));
            tokens.Add(Token.Ordinary(Ordinary.Bend, OrdinarySize.Honourable));
            tokens.Add(Token.Tincture(tincture2));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            ContentField content = blazon.CoatOfArms.Content as ContentField;
            CheckFillingColour(tincture1.TinctureType, tincture1.Value, content.Background);
            CheckOrdinaryCharge(content, Ordinary.Bend, OrdinarySize.Honourable, BlazonMock.SolidFilling(tincture2));
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
                .Add(new FieldVariationDefinition(FieldVariationType.BarryOf))
                .Add(new NumberDefinition(new Number(variationNumber)))
                .Add(tincture1)
                .Add(new KeyWordDefinition(KeyWord.And))
                .Add(tincture2)
                .Add(new OrdinaryDefinition(Ordinary.Bend, OrdinarySize.Honourable))
                .Add(tincture3);
            tokens = tokenBuilder.Build();


            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);
            var expectedFilling = new PatternFilling(FieldVariationType.BarryOf, variationNumber, tincture1.Tincture, tincture2.Tincture);

            // check the result
            CheckBlazonInstanceContent(blazon);
            ContentField content = blazon.CoatOfArms.Content as ContentField;
            Assert.IsNotNull(content.Background);
            Assert.AreEqual(expectedFilling, content.Background);
            CheckOrdinaryCharge(content, Ordinary.Bend, OrdinarySize.Honourable, new SolidFilling(tincture3.Tincture));
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
            FieldDivisionDefinition fieldDivisionDefinition = new FieldDivisionDefinition(FieldDivisionType.Quarterly);
            OrdinaryDefinition ordinaryDefinition = new OrdinaryDefinition(Ordinary.Bend, OrdinarySize.Honourable);

            // create token list
            // blazon: azure honourable bend or
            TokenListBuilder tokenBuilder = new TokenListBuilder()
                .Add(fieldDivisionDefinition)
                .Add(tincture1)
                .Add(new KeyWordDefinition(KeyWord.And))
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

            Tincture[] tinctures ={
                new Tincture(TinctureType.Colour, "AZURE"),
                new Tincture(TinctureType.Colour, "OR" ),
            };

            // create token list
            tokens.Add(Token.FieldDivision(ppType));
            tokens.Add(Token.Tincture(tinctures[0]));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Tincture(tinctures[1]));


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
                CheckFillingColour(TinctureType.Colour, tinctures[i].Value, subfield.Background);
            }
        }

        /// <summary>
        /// Creates a field with party per * division defined by two various fields.
        /// </summary>
        /// <param name="ppType">Particular pp type to be tested.</param>
        private void TestPartyPerSomethinComplexDivision(FieldDivisionType ppType)
        {
            Assert.IsTrue(ppType.IsPartyPerDivision());

            Tincture tincture1 = new Tincture(TinctureType.Colour, "AZURE");
            Tincture tincture2 = new Tincture(TinctureType.Colour, "OR");

            // create token list
            List<Token> tokens = new List<Token>();
            tokens.Add(Token.FieldDivision(ppType));

            // first field will be variated: paly of three azure and or
            tokens.Add(Token.FieldVariation(FieldVariationType.PalyOf));
            tokens.Add(Token.Number(3));
            tokens.Add(Token.Tincture(tincture1));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Tincture(tincture2));

            tokens.Add(Token.Keyword(KeyWord.And));

            // second field will be quarterly divided
            Tincture tincture3 = new Tincture(TinctureType.Colour, "GULES");
            Tincture tincture4 = new Tincture(TinctureType.Colour, "SABLE");
            tokens.Add(Token.FieldDivision(FieldDivisionType.Quarterly));
            tokens.Add(Token.Tincture(tincture3));
            tokens.Add(Token.Keyword(KeyWord.And));
            tokens.Add(Token.Tincture(tincture4));

            // feed the parser
            SyntacticAnalyzer sa = new SyntacticAnalyzer();
            BlazonInstance blazon = sa.Execute(tokens);

            // check the result
            CheckBlazonInstanceContent(blazon);
            DividedField coa = blazon.CoatOfArms.Content as DividedField;
            CheckFieldDivision(coa, ppType, 2);

            // variated field
            ContentField f1 = coa.Subfields[0] as ContentField;
            Assert.IsInstanceOfType(f1.Background, typeof(PatternFilling));
            var variatedBackground = f1.Background as PatternFilling;

            Assert.AreEqual(FieldVariationType.PalyOf, variatedBackground.Type);
            Assert.AreEqual(3, variatedBackground.Number);
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

            Assert.AreEqual(expectedFilling, ordinaryCharge.Filling);
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
            Assert.IsInstanceOfType(filling, typeof(SolidFilling));

            Tincture tDef = (filling as SolidFilling).Tincture;
            Assert.AreEqual(expectedType, tDef.TinctureType);
            Assert.AreEqual(expectedText, tDef.Value);
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
