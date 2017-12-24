using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using Heraldry.Blazon.Vocabulary.Entries;
using System.Collections.Generic;
using Heraldry.Blazon.Vocabulary.Numbers;
using HeraldryTest.SyntacticAnalysis;

namespace HeraldryTest.LexicalAnalysis
{
    [TestClass]
    public class LexicalAnalyzerTest
    {

        private TokenCreator Token = new TokenCreator();

        private BlazonVocabulary CreateVocabulary()
        {
            var sourcesDirectory = Environment.CurrentDirectory + "\\..\\..\\..";
            return VocabularyLoader.LoadFromDirectory(sourcesDirectory + "\\resources\\en_olde\\");
        }

        [TestMethod]
        public void TestParseText()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary());

            string input = "Quarterly 1st and 4th Sable";

            var tokens = analyzer.Execute(input);

            var expectedTokens = new List<Token>
            {
                new Token
                {
                    Position = 0,
                    Definition = new FieldDivisionDefinition { Type = Heraldry.Blazon.Elements.FieldDivisionType.Quarterly }
                },
                new Token
                {
                    Position = 10,
                    Definition = new NumberDefinition (1, NumberType.Ordinal),
                },
                new Token
                {
                    Position = 14,
                    Definition = new KeyWordDefinition { KeyWord = KeyWord.And }
                },
                new Token
                {
                    Position = 18,
                    Definition = new NumberDefinition (4, NumberType.Ordinal),
                },
                new Token
                {
                    Position = 22,
                    Definition = new TinctureDefinition { TinctureType = Heraldry.Blazon.Elements.TinctureType.Colour, Value = "black" }
                }
            };

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
            var analyzer = new LexAnalyzer(CreateVocabulary());

            var tokens = analyzer.Execute("one 1 first 1st");

            var expectedTokens = new List<Token>
            {
                Token.Number(NumberType.Cardinal, 1),
                Token.Number(NumberType.Cardinal, 1),
                Token.Number(NumberType.Ordinal, 1),
                Token.Number(NumberType.Ordinal, 1),
            };

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
