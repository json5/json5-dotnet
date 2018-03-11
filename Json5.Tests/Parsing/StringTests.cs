using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void DoubleQuoteEmptyTest()
        {
            var v = Json5.Parse("\"\"");
            var s = (string)v;
            Assert.AreEqual("", s);
        }

        [TestMethod]
        public void SingleQuoteEmptyTest()
        {
            var v = Json5.Parse("''");
            var s = (string)v;
            Assert.AreEqual("", s);
        }

        [TestMethod]
        public void DoubleQuoteWithSingleQuoteTest()
        {
            var v = Json5.Parse("\"'\"");
            var s = (string)v;
            Assert.AreEqual("'", s);
        }

        [TestMethod]
        public void SingleQuoteWithDoubleQuoteTest()
        {
            var v = Json5.Parse("'\"'");
            var s = (string)v;
            Assert.AreEqual("\"", s);
        }

        [TestMethod]
        public void AllowUnicodeSeparatorsTest()
        {
            var v = Json5.Parse("'\u2028\u2029'");
            var s = (string)v;
            Assert.AreEqual("\u2028\u2029", s);
        }

        [TestMethod]
        public void SimpleEscapesTest()
        {
            var v = Json5.Parse(@"'\b\f\n\r\t\v\0'");
            var s = (string)v;
            Assert.AreEqual("\b\f\n\r\t\v\0", s);
        }

        [TestMethod]
        public void CharacterEscapeTest()
        {
            var v = Json5.Parse(@"'\x12'");
            var s = (string)v;
            Assert.AreEqual("\x12", s);
        }

        [TestMethod]
        public void UnicodeEscapeTest()
        {
            var v = Json5.Parse(@"'\u1234'");
            var s = (string)v;
            Assert.AreEqual("\u1234", s);
        }

        [TestMethod]
        public void EscapedNewLines()
        {
            var v = Json5.Parse("'\\\n\\\r\\\r\n'");
            var s = (string)v;
            Assert.AreEqual("", s);
        }

        [TestMethod]
        public void NonEscapeCharactersTest()
        {
            var v = Json5.Parse(@"'\a\c\d\e'");
            var s = (string)v;
            Assert.AreEqual("acde", s);
        }
    }
}
