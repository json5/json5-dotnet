using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class BooleanTests
    {
        [TestMethod]
        public void TrueTest()
        {
            var s = Json5.Stringify(true);
            Assert.AreEqual("true", s);
        }

        [TestMethod]
        public void FalseTest()
        {
            var s = Json5.Stringify(false);
            Assert.AreEqual("false", s);
        }
    }
}
