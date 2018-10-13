using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Event;
using Minesweeper_RC.Model;

namespace Minesweeper_RC_Test.Event
{
    [TestClass]
    public class CellsRevealedMessageTest
    {
        [TestMethod]
        public void TestPropertiesSetAfterInstantiate()
        {
            var game = new Game(SkillLevel.Beginner);
            var primary = game.Minefield.Get(0, 0);
            var secondary = new Cell[]
            {
                game.Minefield.Get(1, 1),
                game.Minefield.Get(2, 2),
                game.Minefield.Get(3, 3)
            };
            var m = new CellsRevealedMessage(game, primary, secondary);
            Assert.AreEqual<IGame>(game, m.Game);
            Assert.AreEqual<Cell>(primary, m.PrimaryCell);
            CollectionAssert.AreEqual(secondary, m.SecondaryCells);
        }

        [TestMethod]
        public void TestInstantiateExceptions()
        {
            var game = new Game(SkillLevel.Beginner);
            var primary = game.Minefield.Get(0, 0);
            Assert.ThrowsException<ArgumentNullException>(() => new CellsRevealedMessage(null, null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new CellsRevealedMessage(game, null, null));
            Assert.ThrowsException<ArgumentNullException>(() => new CellsRevealedMessage(game, primary, null));
        }

        [TestMethod]
        public void TestInstantiateEmptySecondaryNoError()
        {
            var game = new Game(SkillLevel.Beginner);
            var primary = game.Minefield.Get(0, 0);
            _ = new CellsRevealedMessage(game, primary, new Cell[] { });
        }
    }
}
