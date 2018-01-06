using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Numbers;
using Heraldry.SyntacticAnalysis;
using Heraldry.SyntacticAnalysis.Compilers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.SyntacticAnalysis.Compilers
{
    [TestClass]
    public class NumberCompilerTest : CompilerTestBase
    {
        [TestMethod]
        public void TestNumberList()
        {
            var root = CreateRoot(
                Token.Number(NumberType.Cardinal, 2),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Ordinal, 0),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Ordinal, 4),
                Token.Keyword(KeyWord.And),
                Token.Number(NumberType.Cardinal, 8)
                );

            NumberCompiler nc = new NumberCompiler(root);

            var list = nc.Nums();

            Assert.AreEqual(4, list.Count);

            int[] expVals = { 2, 0, 4, 8 };
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(expVals[i], list[i].Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ExpectedTokenNotFoundException))]
        public void TestNumberListTypeException()
        {
            var root = CreateRoot(
                Token.Number(NumberType.Cardinal, 2),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Cardinal, 0),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Ordinal, 4),
                Token.Keyword(KeyWord.And),
                Token.Number(NumberType.Cardinal, 8)
                );

            NumberCompiler nc = new NumberCompiler(root);

            var list = nc.Nums(NumberType.Cardinal);
        }

        [TestMethod]
        public void TestNumberSequence()
        {
            var root = CreateRoot(
                Token.Number(NumberType.Cardinal, 2),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Ordinal, 0),
                Token.Separator(Separator.Comma),
                Token.Number(NumberType.Ordinal, 4),
                Token.Keyword(KeyWord.And),
                Token.Number(NumberType.Cardinal, 8)
                );

            NumberCompiler nc = new NumberCompiler(root);

            int a = 0, b = 0;

            var sequence = root.Sequence()
                .Next(nc.Cardinal, n => a += n.Value)
                .Next(nc.Ordinal, n => b += n.Value)
                .Next(nc.Ordinal, n => b *= n.Value)
                .Next(nc.Cardinal, n => a *= n.Value);

            sequence.Compile();

            Assert.AreEqual(16, a);
            Assert.AreEqual(0, b);

        }
    }
}
