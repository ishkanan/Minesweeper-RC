using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Utility;
using System.Drawing.Text;
using System.Resources;

namespace Minesweeper_RC_Test.View
{
    [TestClass]
    public class StateViewTest
    {
        [TestMethod]
        public void TestSevenSegmentFontIsAvailable()
        {
            var resManager = new ResourceManager(typeof(Minesweeper_RC.Properties.Resources));
            var font = Utility.GetResourceBytesPointer(resManager, "SevenSegmentFont");
            var collection = new PrivateFontCollection();
            collection.AddMemoryFont(font.Item1, font.Item2);
        }
    }
}
