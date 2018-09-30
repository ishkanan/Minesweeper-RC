using System;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC;

namespace Minesweeper_RC_Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestInstantiateNoError()
        {
            Assert.IsInstanceOfType(new Game(Game.SkillLevel.Beginner), typeof(Game));
        }

        [TestMethod]
        public void TestGetFieldSettingsNoError()
        {
            Assert.IsInstanceOfType(Game.GetFieldSettings(Game.SkillLevel.Beginner), typeof(Game.FieldSettings));
        }

        [TestMethod]
        public void TestPropertiesAfterInstantiate()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            Assert.AreEqual(Game.SkillLevel.Beginner, game.Level);
            Assert.AreEqual(Game.GameState.Stopped, game.State);
            Assert.IsInstanceOfType(game.Settings, typeof(Game.FieldSettings));
            Assert.IsNotNull(game.Settings);
            Assert.IsInstanceOfType(game.Minefield, typeof(Field));
            Assert.IsNotNull(game.Minefield);
            Assert.IsNull(game.Result);
        }

        [TestMethod]
        public void TestFieldHasCorrectSettingsApplied()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            var settings = Game.GetFieldSettings(Game.SkillLevel.Beginner);
            Assert.AreEqual(new Size(settings.Width, settings.Height), game.Minefield.FieldSize);

            var numMines = 0;
            for (var y = 0; y < settings.Height; y++)
                for (var x = 0; x < settings.Width; x++)
                    numMines = (game.Minefield.Get(x, y).IsMine ? numMines + 1 : numMines);
            Assert.AreEqual(settings.MineCount, numMines);
        }
    }
}
