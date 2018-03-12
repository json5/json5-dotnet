using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class ObjectTests
    {
        [TestMethod]
        public void EmptyObjectsTest()
        {
            var s = Json5.Stringify(new Json5Object());
            Assert.AreEqual("{}", s);
        }

        [TestMethod]
        public void UnquotedPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", 1 } });
            Assert.AreEqual("{a:1}", s);
        }

        [TestMethod]
        public void SingleQuotedStringPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a-b", 1 } });
            Assert.AreEqual("{'a-b':1}", s);
        }

        [TestMethod]
        public void DoubleQuotedStringPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a'", 1 } });
            Assert.AreEqual("{\"a'\":1}", s);
        }

        [TestMethod]
        public void EmptyStringPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "", 1 } });
            Assert.AreEqual("{'':1}", s);
        }

        [TestMethod]
        public void SpecialCharacterPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "$_", 1 }, { "_$", 2 }, { "a\u200C", 3 } });
            Assert.AreEqual("{$_:1,_$:2,a\u200C:3}", s);
        }

        [TestMethod]
        public void UnicodePropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "ùńîċõďë", 9 } });
            Assert.AreEqual("{ùńîċõďë:9}", s);
        }

        [TestMethod]
        public void EscapedPropertyNamesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "\\\b\f\n\r\t\v\0\x01", 1 } });
            Assert.AreEqual(@"{'\\\b\f\n\r\t\v\0\x01':1}", s);
        }

        [TestMethod]
        public void MultiplePropertiesTest()
        {
            var s = Json5.Stringify(new Json5Object { { "abc", 1 }, { "def", 2 } });
            Assert.AreEqual("{abc:1,def:2}", s);
        }

        [TestMethod]
        public void NestedObjectsTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", new Json5Object { { "b", 2 } } } });
            Assert.AreEqual("{a:{b:2}}", s);
        }

        [TestMethod]
        public void CircularObjectsTest()
        {
            var o = new Json5Object();
            o["a"] = o;
            //Json5.Stringify(o);
            Assert.Fail("Not Implemented");
        }
    }
}
