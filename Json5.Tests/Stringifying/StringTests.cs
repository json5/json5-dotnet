using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void SingleQuotedStringsTest()
        {
            var s = Json5.Stringify("abc");
            Assert.AreEqual("'abc'", s);
        }

        [TestMethod]
        public void DoubleQuotedStringsTest()
        {
            var s = Json5.Stringify("abc'");
            Assert.AreEqual("\"abc'\"", s);
        }

        [TestMethod]
        public void EscapedCharactersTest()
        {
            var s = Json5.Stringify("\\\b\f\n\r\t\v\0\x0f");
            Assert.AreEqual(@"'\\\b\f\n\r\t\v\0\x0f'", s);
        }

        [TestMethod]
        public void EscapedSingleQuotesTest()
        {
            var s = Json5.Stringify("'\"");
            Assert.AreEqual("'\\'\"'", s);
        }

        [TestMethod]
        public void EscapedDoubleQuotesTest()
        {
            var s = Json5.Stringify("''\"");
            Assert.AreEqual("\"''\\\"\"", s);
        }

        [TestMethod]
        public void EscapedSeparatorsTest()
        {
            var s = Json5.Stringify("\u2028\u2029");
            Assert.AreEqual("'\\u2028\\u2029'", s);
        }
    }
}
