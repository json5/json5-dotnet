using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
  [TestClass]
  public class NumberTests
  {
    [TestMethod]
    public void ZeroTest()
    {
      var v = Json5.Parse("0");
      var n = (double)v;
      Assert.AreEqual(0D, n);
    }

    [TestMethod]
    public void ZeroDotTest()
    {
      var v = Json5.Parse("0.");
      var n = (double)v;
      Assert.AreEqual(0D, n);
    }

    [TestMethod]
    public void ZeroExpTest()
    {
      var v = Json5.Parse("0e1");
      var n = (double)v;
      Assert.AreEqual(0D, n);

      v = Json5.Parse("0E1");
      n = (double)v;
      Assert.AreEqual(0D, n);
    }

    [TestMethod]
    public void ZeroDotDigitTest()
    {
      var v = Json5.Parse("0.1");
      var n = (double)v;
      Assert.AreEqual(0.1D, n);
    }

    [TestMethod]
    public void ZeroHexTest()
    {
      var v = Json5.Parse("0x1");
      var n = (double)v;
      Assert.AreEqual(1D, n);
    }

    [TestMethod]
    public void DecimalIntegerTest()
    {
      var v = Json5.Parse("1");
      var n = (double)v;
      Assert.AreEqual(1D, n);
    }

    [TestMethod]
    public void DecimalIntegerDotTest()
    {
      var v = Json5.Parse("1.");
      var n = (double)v;
      Assert.AreEqual(1D, n);
    }

    [TestMethod]
    public void DecimalIntegerExpTest()
    {
      var v = Json5.Parse("1e2");
      var n = (double)v;
      Assert.AreEqual(100D, n);

      v = Json5.Parse("1E2");
      n = (double)v;
      Assert.AreEqual(100D, n);
    }

    [TestMethod]
    public void DecimalIntegerDigitTest()
    {
      var v = Json5.Parse("12");
      var n = (double)v;
      Assert.AreEqual(12D, n);
    }

    [TestMethod]
    public void DecimalPointLeadingTest()
    {
      var v = Json5.Parse(".1");
      var n = (double)v;
      Assert.AreEqual(.1D, n);
    }

    [TestMethod]
    public void DecimalPointExpTest()
    {
      var v = Json5.Parse("1.e2");
      var n = (double)v;
      Assert.AreEqual(100D, n);

      v = Json5.Parse("1.E2");
      n = (double)v;
      Assert.AreEqual(100D, n);
    }

    [TestMethod]
    public void DecimalPointDigitTest()
    {
      var v = Json5.Parse("1.2");
      var n = (double)v;
      Assert.AreEqual(1.2D, n);
    }

    [TestMethod]
    public void DecimalFractionExpTest()
    {
      var v = Json5.Parse("1.2e3");
      var n = (double)v;
      Assert.AreEqual(1200D, n);

      v = Json5.Parse("1.2E3");
      n = (double)v;
      Assert.AreEqual(1200D, n);
    }

    [TestMethod]
    public void DecimalFractionDigitTest()
    {
      var v = Json5.Parse("1.23");
      var n = (double)v;
      Assert.AreEqual(1.23D, n);
    }

    [TestMethod]
    public void DecimalExponentSignTest()
    {
      var v = Json5.Parse("1e+2");
      var n = (double)v;
      Assert.AreEqual(100D, n);

      v = Json5.Parse("1E+2");
      n = (double)v;
      Assert.AreEqual(100D, n);

      v = Json5.Parse("1e-2");
      n = (double)v;
      Assert.AreEqual(0.01D, n);

      v = Json5.Parse("1E-2");
      n = (double)v;
      Assert.AreEqual(0.01D, n);
    }

    [TestMethod]
    public void HexadecimalIntegerTest()
    {
      var v = Json5.Parse("0x12");
      var n = (double)v;
      Assert.AreEqual(0x12, n);
    }

    [TestMethod]
    public void SignedNumberTest()
    {
      var v = Json5.Parse("+1");
      var n = (double)v;
      Assert.AreEqual(1, n);

      v = Json5.Parse("-1");
      n = (double)v;
      Assert.AreEqual(-1, n);

      v = Json5.Parse("-0x12");
      n = (double)v;
      Assert.AreEqual(-0x12, n);
    }

    [TestMethod]
    public void InfinityTest()
    {
      var v = Json5.Parse("Infinity");
      var n = (double)v;
      Assert.AreEqual(double.PositiveInfinity, n);

      v = Json5.Parse("+Infinity");
      n = (double)v;
      Assert.AreEqual(double.PositiveInfinity, n);

      v = Json5.Parse("-Infinity");
      n = (double)v;
      Assert.AreEqual(double.NegativeInfinity, n);
    }

    [TestMethod]
    public void NaNTest()
    {
      var v = Json5.Parse("NaN");
      var n = (double)v;
      Assert.AreEqual(double.NaN, n);

      v = Json5.Parse("+NaN");
      n = (double)v;
      Assert.AreEqual(double.NaN, n);

      v = Json5.Parse("-NaN");
      n = (double)v;
      Assert.AreEqual(double.NaN, n);
    }
  }
}
