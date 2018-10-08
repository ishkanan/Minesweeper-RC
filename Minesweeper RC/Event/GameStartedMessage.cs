using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class GameStartedMessage : IMessage
    {
        public Game Game { get; private set; }

        public GameStartedMessage(Game game)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
        }
    }
}
