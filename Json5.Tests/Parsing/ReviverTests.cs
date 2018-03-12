using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class ReviverTests
    {
        [TestMethod]
        public void ModifiesPropertyValuesTest()
        {
            var val = Json5.Parse("{a:1,b:2}", (k, v) => (k == "a") ? "revived" : v);
            Assert.AreEqual("revived", (string)val["a"]);
            Assert.AreEqual(2D, (double)val["b"]);
        }

        [TestMethod]
        public void ModifiesNestedObjectPropertyValuesTest()
        {
            var val = Json5.Parse("{a:{b:2}}", (k, v) => (k == "b") ? "revived" : v);
            Assert.AreEqual("revived", (string)val["a"]["b"]);
        }

        [TestMethod]
        public void DeletesPropertyValuesTest()
        {
            var val = Json5.Parse("{a:1,b:2}", (k, v) => (k == "a") ? null : v);
            var o = (Json5Object)val;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(2D, (double)o["b"]);
        }

        [TestMethod]
        public void ModifiesArrayValuesTest()
        {
            var val = Json5.Parse("[0,1,2]", (k, v) => (k == "1") ? "revived" : v);
            Assert.AreEqual(0D, (double)val[0]);
            Assert.AreEqual("revived", (string)val[1]);
            Assert.AreEqual(2D, (double)val[2]);
        }

        [TestMethod]
        public void ModifiesNestedArrayValuesTest()
        {
            var val = Json5.Parse("[0,[1,2,3]]", (k, v) => (k == "2") ? "revived" : v);
            Assert.AreEqual(0D, (double)val[0]);
            Assert.AreEqual(1D, (double)val[1][0]);
            Assert.AreEqual(2D, (double)val[1][1]);
            Assert.AreEqual("revived", (string)val[1][2]);
        }

        [TestMethod]
        public void DeletesArrayValuesTest()
        {
            var val = Json5.Parse("[0,1,2]", (k, v) => (k == "1") ? null : v);
            Assert.AreEqual(0D, (double)val[0]);
            Assert.AreEqual(null, val[1]);
            Assert.AreEqual(2D, (double)val[2]);
        }

        [TestMethod]
        public void ModifiesRootValueTest()
        {
            var val = Json5.Parse("1", (k, v) => (k == "") ? "revived" : v);
            Assert.AreEqual("revived", (string)val);
        }

        [TestMethod]
        public void ExposesParentValueTest()
        {
            var val = Json5.Parse("{a:{b:2}}", (p, k, v) => (k == "b" && (double?)p["b"] != null) ? "revived" : v);
            Assert.AreEqual("revived", (string)val["a"]["b"]);
        }
    }
}
