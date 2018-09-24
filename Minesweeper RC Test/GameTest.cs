using System;
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
            Assert.AreEqual(game.Level, Game.SkillLevel.Beginner);
            Assert.AreEqual(game.State, Game.GameState.Stopped);
            Assert.IsInstanceOfType(game.Settings, typeof(Game.FieldSettings));
            Assert.IsInstanceOfType(game.Field, typeof(Cell[,]));
            Assert.IsNull(game.Result);
        }

        [TestMethod]
        public void TestFieldHasSettingsApplied()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            var settings = Game.GetFieldSettings(Game.SkillLevel.Beginner);
            Assert.AreEqual(game.Field.GetLength(0), settings.Width);
            Assert.AreEqual(game.Field.GetLength(1), settings.Height);

            var numMines = 0;
            for (var y = 0; y < settings.Height; y++)
            {
                for (var x = 0; x < settings.Width; x++)
                {
                    game.Field[x, y].Reveal();
                    numMines = (game.Field[x, y].IsMine ? numMines + 1 : numMines);
                }
            }
            Assert.AreEqual(numMines, settings.MineCount);
        }

        [TestMethod]
        public void TestFieldIsLockedBeforeStart()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            for (var y = 0; y < game.Settings.Height; y++)
                for (var x = 0; x < game.Settings.Width; x++)
                    Assert.AreEqual(game.Field[x, y].Locked, true);
        }
    }
}
