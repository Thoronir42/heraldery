using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Heraldry.Blazon.Vocabulary;
using Heraldry.LexicalAnalysis;
using Heraldry.LexicalAnalysis.Numbers;
using Heraldry.Blazon.Vocabulary.Entries;
using System.Collections.Generic;

namespace HeraldryTest.LexicalAnalysis
{
    [TestClass]
    public class LexicalAnalyzerTest
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

        [TestMethod]
        public void TestParseText()
        {
            LexAnalyzer analyzer = new LexAnalyzer(CreateVocabulary(), CreateNumberParser());

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
                    Definition = new NumberDefinition { Type = NumberType.Ordinal, Value = 1 }
                },
                new Token
                {
                    Position = 14,
                    Definition = new KeyWordDefinition { KeyWord = KeyWord.And }
                },
                new Token
                {
                    Position = 18,
                    Definition = new NumberDefinition { Type = NumberType.Ordinal, Value = 4 }
                },
                new Token
                {
                    Position = 22,
                    Definition = new TinctureDefinition { TinctureType = Heraldry.Blazon.Elements.TinctureType.Colour, Value = "black" }
                }
            };

            Assert.AreEqual(tokens.Count, expectedTokens.Count);

            for(int i = 0; i < tokens.Count; i++)
            {
                var a = tokens[i];
                var b = expectedTokens[i];
                Assert.AreEqual(a.Position, b.Position);
                Assert.AreEqual(a.Definition, b.Definition);
            }
        }
    }
}
