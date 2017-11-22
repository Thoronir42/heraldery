﻿using System;
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
            TinctureDefinition tincture = new TinctureDefinition { Text = "AZURE", Type = TinctureType.Colour };
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
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token { Position = 0, Definition = divisionDefinition });

            // quaterly division qhich consists of two colours
            TinctureDefinition tincture1 = new TinctureDefinition { Text = "AZURE", Type = TinctureType.Colour };
            TinctureDefinition tincture2 = new TinctureDefinition();
            tincture1.Text = "OR";
            tincture2.Type = TinctureType.Colour;
            tokens.Add(new Token { Position = 10, Definition = tincture1 });
            tokens.Add(new Token { Position = 16, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 30, Definition = tincture2 });

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
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token { Definition = divisionDefinition });

            // quaterly division qhich consists of four colours
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(new Token { Position = 1, Definition = new NumberDefinition { Value = 1 } });
            tokens.Add(new Token { Position = 2, Definition = tincture1 });
            tokens.Add(new Token { Position = 3, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 4, Definition = new NumberDefinition { Value = 2 } });
            tokens.Add(new Token { Position = 5, Definition = tincture2 });
            tokens.Add(new Token { Position = 6, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 7, Definition = new NumberDefinition { Value = 3 } });
            tokens.Add(new Token { Position = 8, Definition = tincture3 });
            tokens.Add(new Token { Position = 9, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 10, Definition = new NumberDefinition { Value = 4 } });
            tokens.Add(new Token { Position = 11, Definition = tincture4 });

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
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token { Position = 0, Definition = divisionDefinition });

            // quaterly division qhich consists of four colours
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            TinctureDefinition tincture3 = new TinctureDefinition(TinctureType.Colour, "GULES");
            TinctureDefinition tincture4 = new TinctureDefinition(TinctureType.Colour, "SABLE");
            tokens.Add(new Token { Position = 1, Definition = new NumberDefinition { Value = 4 } });
            tokens.Add(new Token { Position = 2, Definition = tincture1 });
            tokens.Add(new Token { Position = 3, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 4, Definition = new NumberDefinition { Value = 2 } });
            tokens.Add(new Token { Position = 5, Definition = tincture2 });
            tokens.Add(new Token { Position = 6, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 7, Definition = new NumberDefinition { Value = 1 } });
            tokens.Add(new Token { Position = 8, Definition = tincture3 });
            tokens.Add(new Token { Position = 9, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 10, Definition = new NumberDefinition { Value = 3 } });
            tokens.Add(new Token { Position = 11, Definition = tincture4 });
            tokens.Add(new Token { Position = 12, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });

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
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token { Position = 0, Definition = divisionDefinition });

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 colour
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token { Position = 1, Definition = new NumberDefinition { Value = 4 } });
            tokens.Add(new Token { Position = 2, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 3, Definition = new NumberDefinition { Value = 3 } });
            tokens.Add(new Token { Position = 4, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 5, Definition = new NumberDefinition { Value = 2 } });
            tokens.Add(new Token { Position = 6, Definition = tincture1 });
            tokens.Add(new Token { Position = 7, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 8, Definition = new NumberDefinition { Value = 1 } });
            tokens.Add(new Token { Position = 9, Definition = tincture2 });
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
            // todo:
            // prepare data
            List<Token> tokens = new List<Token>();
            FieldDivisionDefinition divisionDefinition = new FieldDivisionDefinition { Type = FieldDivisionType.Quarterly };
            tokens.Add(new Token { Position = 0, Definition = divisionDefinition });

            // quaterly division qhich consists of two colours:
            // 4 and 3 and 2 colour; 1 quaterly 4 and 3 and 2 colour; 1 colour
            // note that the nested division is in the 1st field
            TinctureDefinition tincture1 = new TinctureDefinition(TinctureType.Colour, "AZURE");
            TinctureDefinition tincture2 = new TinctureDefinition(TinctureType.Colour, "OR");
            tokens.Add(new Token { Position = 1, Definition = new NumberDefinition { Value = 4 } });
            tokens.Add(new Token { Position = 2, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 3, Definition = new NumberDefinition { Value = 3 } });
            tokens.Add(new Token { Position = 4, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 5, Definition = new NumberDefinition { Value = 2 } });
            tokens.Add(new Token { Position = 6, Definition = tincture1 });
            tokens.Add(new Token { Position = 7, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 8, Definition = new NumberDefinition { Value = 1 } });
            tokens.Add(new Token { Position = 9, Definition = divisionDefinition });
            tokens.Add(new Token { Position = 10, Definition = new NumberDefinition { Value = 4 } });
            tokens.Add(new Token { Position = 11, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 12, Definition = new NumberDefinition { Value = 3 } });
            tokens.Add(new Token { Position = 13, Definition = new KeyWordDefinition { KeyWord = KeyWord.And } });
            tokens.Add(new Token { Position = 14, Definition = new NumberDefinition { Value = 2 } });
            tokens.Add(new Token { Position = 15, Definition = tincture1 });
            tokens.Add(new Token { Position = 16, Definition = new KeyWordDefinition { KeyWord = KeyWord.Separator } });
            tokens.Add(new Token { Position = 17, Definition = new NumberDefinition { Value = 1 } });
            tokens.Add(new Token { Position = 18, Definition = tincture2 });
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
