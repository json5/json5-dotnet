using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
  [TestClass]
  public class ObjectTests
  {
    [TestMethod]
    public void EmptyTest()
    {
      var v = Json5.Parse("{}");
      var o = (Json5Object)v;
      Assert.AreEqual(0, o.Count);
    }

    [TestMethod]
    public void DoubleQuotedKeyTest()
    {
      var v = Json5.Parse("{\"abc\": 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["abc"]);
    }

    [TestMethod]
    public void SingleQuotedKeyTest()
    {
      var v = Json5.Parse("{'abc': 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["abc"]);
    }

    [TestMethod]
    public void UnquotedAsciiKeyTest()
    {
      var v = Json5.Parse("{abc: 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["abc"]);
    }

    [TestMethod]
    public void UnquotedUnicodeKeyTest()
    {
      var v = Json5.Parse("{αβγ: 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["αβγ"]);
    }

    [TestMethod]
    public void UnquotedEscapedUnicodeKeyTest()
    {
      var v = Json5.Parse(@"{\u03B1\u03B2\u03B3: 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["αβγ"]);
    }

    [TestMethod]
    public void UnquotedKeyStartingWithReservedWordTest()
    {
      var v = Json5.Parse("{Infinity$: 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["Infinity$"]);

      v = Json5.Parse("{NaNNot: 0}");
      o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["NaNNot"]);
    }

    [TestMethod]
    public void ReservedWordKeyTest()
    {
      var v = Json5.Parse("{true: 0}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["true"]);

      v = Json5.Parse("{Infinity: 0}");
      o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(0D, (double)o["Infinity"]);
    }

    [TestMethod]
    public void MultiplePropertiesTest()
    {
      var v = Json5.Parse("{abc: 0, def: 1}");
      var o = (Json5Object)v;
      Assert.AreEqual(2, o.Count);
      Assert.AreEqual(0D, (double)o["abc"]);
      Assert.AreEqual(1D, (double)o["def"]);
    }

    [TestMethod]
    public void DuplicateKeyTest()
    {
      var v = Json5.Parse("{abc: 0, abc: 1}");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(1D, (double)o["abc"]);
    }

    [TestMethod]
    public void TrailingCommaTest()
    {
      var v = Json5.Parse("{abc: 0, def: 1, }");
      var o = (Json5Object)v;
      Assert.AreEqual(2, o.Count);
      Assert.AreEqual(0D, (double)o["abc"]);
      Assert.AreEqual(1D, (double)o["def"]);
    }

    [TestMethod]
    public void NestedTest()
    {
      var v = Json5.Parse("{abc: {def: 1} }");
      var o = (Json5Object)v;
      Assert.AreEqual(1, o.Count);
      Assert.AreEqual(1D, (double)o["abc"]["def"]);
    }
  }
}
