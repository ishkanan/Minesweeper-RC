using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper_RC.Event;
using Minesweeper_RC.Model;

namespace Minesweeper_RC_Test.Event
{
    [TestClass]
    public class NewGameRequestedMessageTest
    {
        [TestMethod]
        public void TestPropertiesAfterInstantiate()
        {
            var m = new NewGameRequestedMessage(null);
            Assert.IsNull(m.Game);
            var game = new Game(SkillLevel.Beginner);
            m = new NewGameRequestedMessage(game);
            Assert.AreEqual<IGame>(game, m.Game);
        }
    }
}
