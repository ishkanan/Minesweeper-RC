using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class FieldMouseDownMessage : IMessage
    {
        public IGame Game { get; private set; }

        public FieldMouseDownMessage(IGame game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
