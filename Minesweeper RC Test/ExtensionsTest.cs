using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Extensions;
using System;
using System.Drawing;

namespace Minesweeper_RC_Test
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void TestSizeMultiplyBySingleScalar()
        {
            var size = new Size(3, 5);
            var factor = 2;
            var expected = new Size(size.Width * factor, size.Height * factor);
            Assert.AreEqual<Size>(expected, size.MultiplyBy(factor));
        }

        [TestMethod]
        public void TestSizeMultiplyByXYScalars()
        {
            var size = new Size(3, 5);
            int Xfactor = 2, Yfactor = 3;
            var expected = new Size(size.Width * Xfactor, size.Height * Yfactor);
            Assert.AreEqual<Size>(expected, size.MultiplyBy(Xfactor, Yfactor));
        }

        [TestMethod]
        public void TestFlattenRectArray()
        {
            var toTest = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var expected = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = toTest.Flatten();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
