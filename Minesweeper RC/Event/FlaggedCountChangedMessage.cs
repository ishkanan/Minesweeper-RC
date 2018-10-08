using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class FlaggedCountChangedMessage : IMessage
    {
        public IGame Game { get; private set; }

        public FlaggedCountChangedMessage(IGame game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
