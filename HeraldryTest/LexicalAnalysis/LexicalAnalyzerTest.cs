using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using Heraldry.Blazon.Vocabulary.Entries;
using System.Collections.Generic;
using Heraldry.Blazon.Vocabulary.Numbers;
using HeraldryTest.SyntacticAnalysis;
using HeraldryTest.Helpers;
using Heraldry.Blazon.Elements;

namespace HeraldryTest.LexicalAnalysis
{
    [TestClass]
    public class LexicalAnalyzerTest
    {

        private TokenCreator Token = new TokenCreator();

        [TestMethod]
        public void TestParseText()
        {
            LexAnalyzer analyzer = new LexAnalyzer(MockVocabulary.Get());

            string input = "Quarterly 1st and 4th Sable";

            var expectedTokens = new List<Token>
            {
                new Token(0, new FieldDivisionDefinition(FieldDivisionType.Quarterly)),
                new Token(10, new NumberDefinition (1, NumberType.Ordinal)),
                new Token(14, new KeyWordDefinition (KeyWord.And)),
                new Token(18, new NumberDefinition (4, NumberType.Ordinal)),
                new Token(22, new TinctureDefinition(new Tincture (TinctureType.Colour, "black"))),
            };

            var tokens = analyzer.Execute(input);
            Assert.AreEqual(tokens.Count, expectedTokens.Count);

            for (int i = 0; i < tokens.Count; i++)
            {
                var a = expectedTokens[i];
                var b = tokens[i];

                Assert.AreEqual(a.Position, b.Position);
                Assert.AreEqual(a.Definition, b.Definition);
            }
        }

        [TestMethod]
        public void TestParseNumbers()
        {
            var analyzer = new LexAnalyzer(MockVocabulary.Get());

            var expectedTokens = new List<Token>
            {
                Token.Number(NumberType.Cardinal, 1),
                Token.Number(NumberType.Cardinal, 1),
                Token.Number(NumberType.Ordinal, 1),
                Token.Number(NumberType.Ordinal, 1),
            };


            var tokens = analyzer.Execute("one 1 first 1st");

            Assert.AreEqual(tokens.Count, expectedTokens.Count);

            for (int i = 0; i < tokens.Count; i++)
            {
                var a = expectedTokens[i];
                var b = tokens[i];

                Assert.AreEqual(a.Definition, b.Definition);
            }
        }
    }
}
