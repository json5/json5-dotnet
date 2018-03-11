using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class BooleanTests
    {
        [TestMethod]
        public void TrueTest()
        {
            var v = Json5.Parse("true");
            var b = (bool)v;
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void FalseTest()
        {
            var v = Json5.Parse("false");
            var b = (bool)v;
            Assert.IsFalse(b);
        }
    }
}
