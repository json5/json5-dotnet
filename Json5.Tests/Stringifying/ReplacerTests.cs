using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class ReplacerTests
    {
        [TestMethod]
        public void KeysTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", 1 }, { "b", 2 }, { "3", 3 } }, new[] { "a", "3" });
            Assert.AreEqual("{a:1,'3':3}", s);
        }

        [TestMethod]
        public void FunctionTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", 1 }, { "b", 2 } }, (k, v) => (k == "a") ? 2 : v);
            Assert.AreEqual("{a:2,b:2}", s);
        }

        [TestMethod]
        public void ExposesParentValueTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", 1 }, { "b", 2 } }, (p, k, v) => (k == "b" && (double?)p["b"] != null) ? 2 : v);
            Assert.AreEqual("{a:{b:2}}", s);
        }
    }
}
