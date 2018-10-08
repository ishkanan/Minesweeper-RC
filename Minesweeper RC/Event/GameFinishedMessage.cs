using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class GameFinishedMessage : IMessage
    {
        public IGame Game { get; private set; }

        public GameFinishedMessage(IGame game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
