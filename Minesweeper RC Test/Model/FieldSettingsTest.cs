using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Model;

namespace Minesweeper_RC_Test.Model
{
    [TestClass]
    public class FieldSettingsTest
    {
        [TestMethod]
        public void TestInstantiateNoErrors()
        {
            Assert.IsInstanceOfType(new FieldSettings(10, 20, 5), typeof(FieldSettings));
        }

        [TestMethod]
        public void TestGetPropertiesAfterInstantiate()
        {
            var settings = new FieldSettings(10, 20, 5);
            Assert.AreEqual(10, settings.Width);
            Assert.AreEqual(20, settings.Height);
            Assert.AreEqual(5, settings.MineCount);
        }

        [TestMethod]
        public void TestGetFieldsSettingsNoError()
        {
            Assert.IsInstanceOfType(Game.GetFieldSettings(SkillLevel.Beginner), typeof(FieldSettings));
            Assert.IsInstanceOfType(Game.GetFieldSettings(SkillLevel.Intermediate), typeof(FieldSettings));
            Assert.IsInstanceOfType(Game.GetFieldSettings(SkillLevel.Expert), typeof(FieldSettings));
        }
    }
}
