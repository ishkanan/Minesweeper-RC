using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class GameFinishedMessage : IMessage
    {
        public Game Game { get; private set; }

        public GameFinishedMessage(Game game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
