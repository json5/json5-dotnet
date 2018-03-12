using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void DoubleQuotedStringsTest()
        {
            var v = Json5.Parse("\"abc\"");
            Assert.AreEqual("abc", (string)v);
        }

        [TestMethod]
        public void SingleQuotedStringsTest()
        {
            var v = Json5.Parse("'abc'");
            Assert.AreEqual("abc", (string)v);
        }

        [TestMethod]
        public void NestedQuotesInStringsTest()
        {
            var v = Json5.Parse("['\"',\"'\"]");
            Assert.AreEqual("\"", (string)v[0]);
            Assert.AreEqual("'", (string)v[1]);
        }

        [TestMethod]
        public void EscapedCharactersTest()
        {
            var v = Json5.Parse("'\\b\\f\\n\\r\\t\\v\\0\\x0f\\u01fF\\\n\\\r\n\\\r\\\u2028\\\u2029\\a\\'\\\"'");
            Assert.AreEqual("\b\f\n\r\t\v\0\x0f\u01FFa'\"", (string)v);
        }

        [TestMethod]
        public void SeparatorsTest()
        {
            var v = Json5.Parse("'\u2028\u2029'");
            Assert.AreEqual("\u2028\u2029", (string)v);
        }
    }
}
