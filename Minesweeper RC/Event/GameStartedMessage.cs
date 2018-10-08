using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class GameStartedMessage : IMessage
    {
        public IGame Game { get; private set; }

        public GameStartedMessage(IGame game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
