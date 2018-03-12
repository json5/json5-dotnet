using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class ObjectTests
    {
        [TestMethod]
        public void EmptyObjectsTest()
        {
            var v = Json5.Parse("{}");
            var o = (Json5Object)v;
            Assert.AreEqual(0, o.Count);
        }

        [TestMethod]
        public void DoubleStringPropertyNamesTest()
        {
            var v = Json5.Parse("{\"a\":1}");
            var o = (Json5Object)v;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(1D, (double)o["a"]);
        }

        [TestMethod]
        public void SingleStringPropertyNamesTest()
        {
            var v = Json5.Parse("{'a':1}");
            var o = (Json5Object)v;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(1D, (double)o["a"]);
        }

        [TestMethod]
        public void UnquotedPropertyNamesTest()
        {
            var v = Json5.Parse("{a:1}");
            var o = (Json5Object)v;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(1D, (double)o["a"]);
        }

        [TestMethod]
        public void SpecialCharacterPropertyNamesTest()
        {
            var v = Json5.Parse("{$_:1,_$:2,a\u200C:3}");
            var o = (Json5Object)v;
            Assert.AreEqual(3, o.Count);
            Assert.AreEqual(1D, (double)o["$_"]);
            Assert.AreEqual(2D, (double)o["_$"]);
            Assert.AreEqual(3D, (double)o["a\u200C"]);
        }

        [TestMethod]
        public void UnicodePropertyNamesTest()
        {
            var v = Json5.Parse("{ùńîċõďë:9}");
            var o = (Json5Object)v;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(9D, (double)o["ùńîċõďë"]);
        }

        [TestMethod]
        public void EscapedPropertyNamesTest()
        {
            var v = Json5.Parse(@"{\u0061\u0062:1,\u0024\u005F:2,\u005F\u0024:3}");
            var o = (Json5Object)v;
            Assert.AreEqual(3, o.Count);
            Assert.AreEqual(1D, (double)o["ab"]);
            Assert.AreEqual(2D, (double)o["$_"]);
            Assert.AreEqual(3D, (double)o["_$"]);
        }

        [TestMethod]
        public void MultiplePropertiesTest()
        {
            var v = Json5.Parse("{abc:1,def:2}");
            var o = (Json5Object)v;
            Assert.AreEqual(2, o.Count);
            Assert.AreEqual(1D, (double)o["abc"]);
            Assert.AreEqual(2D, (double)o["def"]);
        }

        [TestMethod]
        public void NestedObjectsTest()
        {
            var v = Json5.Parse("{a:{b:2}}");
            var o = (Json5Object)v;
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(2D, (double)o["a"]["b"]);
        }
    }
}
