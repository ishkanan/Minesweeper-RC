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

        [TestMethod]
        public void TestStartSuccessAndFail()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            Assert.AreEqual(Game.GameState.Stopped, game.State);
            game.Start();
            Assert.AreEqual(Game.GameState.Running, game.State);
            game.Start();

            // end the game and set a result
            var gamePrivate = new PrivateObject(game);
            gamePrivate.SetProperty("Result", Game.GameResult.Failure);
            gamePrivate.SetProperty("State", Game.GameState.Stopped);
            Assert.ThrowsException<InvalidOperationException>(() => game.Start());
        }

        [TestMethod]
        public void TestRevealNotStarted()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            Assert.ThrowsException<InvalidOperationException>(() => game.Reveal(3, 3));
        }

        [TestMethod]
        public void TestRevealInvalidParams()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            game.Start();

            foreach (int x in new int[] { -3, -2, -1, game.Settings.Width, game.Settings.Width + 1, game.Settings.Width + 2 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => game.Reveal(x, 3));
            foreach (int y in new int[] { -3, -2, -1, game.Settings.Height, game.Settings.Height + 1, game.Settings.Height + 2 })
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => game.Reveal(3, y));
        }

        [TestMethod]
        public void TestRevealAlreadyRevealed()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            game.Start();
            game.Reveal(game.Minefield.SafeStartLocation);
            Assert.ThrowsException<InvalidOperationException>(() => game.Reveal(game.Minefield.SafeStartLocation));
        }

        [TestMethod]
        public void TestRevealFlaggedCell()
        {
            var game = new Game(Game.SkillLevel.Beginner);
            game.Start();
            var cell = game.Minefield.Get(game.Minefield.SafeStartLocation);
            cell.IsFlagged = true;
            Assert.ThrowsException<InvalidOperationException>(() => game.Reveal(cell.Location));
        }
    }
}
