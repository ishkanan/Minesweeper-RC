using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Utility;
using Moq;
using System;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Minesweeper_RC_Test
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void TestGetFontResourceBytesValidResource()
        {
            var mockAssembly = new Mock<_Assembly>(Moq.MockBehavior.Loose);
            var fontName = "TestFontyMcFont";
            var testFontBytes = new byte[] { 1, 2, 3, 4, 5 };
            var testStream = new MemoryStream(testFontBytes);

            mockAssembly.Setup(a => a.GetManifestResourceStream(fontName)).Returns(testStream);
            var actualBytes = Utility.GetFontResourceBytes(mockAssembly.Object, fontName);
            CollectionAssert.AreEqual(testFontBytes, actualBytes);
        }

        [TestMethod]
        public void TestGetFontResourceBytesInvalidResource()
        {
            var mockAssembly = new Mock<_Assembly>(Moq.MockBehavior.Loose);
            var fontName = "TestFontyMcFont";

            mockAssembly.Setup(a => a.GetManifestResourceStream(fontName));
            Assert.ThrowsException<ApplicationException>(() => Utility.GetFontResourceBytes(mockAssembly.Object, fontName));
        }

        [TestMethod]
        public void TestAddFontFromResourceValidResource()
        {
            var mockAssembly = new Mock<_Assembly>(Moq.MockBehavior.Loose);
            var fontName = "TestFontyMcFont";
            var testFontBytes = new byte[] { 1, 2, 3, 4, 5 };
            var testStream = new MemoryStream(testFontBytes);

            mockAssembly.Setup(a => a.GetManifestResourceStream(fontName)).Returns(testStream);
            var actualData = Utility.GetFontResourcePointer(mockAssembly.Object, fontName);
            Assert.AreNotEqual<int>(0, actualData.Item1.ToInt32());
            Assert.AreEqual<int>(testFontBytes.Length, actualData.Item2);
        }

        [TestMethod]
        public void TestAddFontFromResourceInvalidResource()
        {
            var mockAssembly = new Mock<_Assembly>(Moq.MockBehavior.Loose);
            var fontName = "TestFontyMcFont";

            mockAssembly.Setup(a => a.GetManifestResourceStream(fontName));
            Assert.ThrowsException<ApplicationException>(() => Utility.GetFontResourcePointer(mockAssembly.Object, fontName));
        }
    }
}
