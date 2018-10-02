using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Model;

namespace Minesweeper_RC_Test.Model
{
    [TestClass]
    public class GameFieldSettingsTest
    {
        [TestMethod]
        public void TestInstantiateNoErrors()
        {
            Assert.IsInstanceOfType(new Game.FieldSettings(10, 20, 5), typeof(Game.FieldSettings));
        }

        [TestMethod]
        public void TestGetPropertiesAfterInstantiate()
        {
            var settings = new Game.FieldSettings(10, 20, 5);
            Assert.AreEqual(10, settings.Width);
            Assert.AreEqual(20, settings.Height);
            Assert.AreEqual(5, settings.MineCount);
        }

        [TestMethod]
        public void TestGetFieldsSettingsNoError()
        {
            Assert.IsInstanceOfType(Game.GetFieldSettings(Game.SkillLevel.Beginner), typeof(Game.FieldSettings));
            Assert.IsInstanceOfType(Game.GetFieldSettings(Game.SkillLevel.Intermediate), typeof(Game.FieldSettings));
            Assert.IsInstanceOfType(Game.GetFieldSettings(Game.SkillLevel.Expert), typeof(Game.FieldSettings));
        }
    }
}
