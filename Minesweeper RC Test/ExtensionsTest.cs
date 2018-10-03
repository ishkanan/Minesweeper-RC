using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Extensions;
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
    }
}
