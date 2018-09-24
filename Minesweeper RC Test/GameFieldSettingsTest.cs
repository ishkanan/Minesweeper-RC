using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC;

namespace Minesweeper_RC_Test
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
            Assert.AreEqual(settings.Width, 10);
            Assert.AreEqual(settings.Height, 20);
            Assert.AreEqual(settings.MineCount, 5);
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
