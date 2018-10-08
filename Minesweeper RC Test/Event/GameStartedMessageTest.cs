using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Event;
using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC_Test.Event
{
    [TestClass]
    public class GameStartedMessageTest
    {
        [TestMethod]
        public void TestPropertiesSetAfterInstantiate()
        {
            var game = new Game(SkillLevel.Beginner);
            var m = new GameStartedMessage(game);
            Assert.AreEqual<Game>(game, m.Game);
        }

        [TestMethod]
        public void TestInstantiateExceptions()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GameStartedMessage(null));
        }
    }
}
