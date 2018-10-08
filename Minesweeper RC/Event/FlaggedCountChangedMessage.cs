using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class FlaggedCountChangedMessage : IMessage
    {
        public Game Game { get; private set; }

        public FlaggedCountChangedMessage(Game game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
