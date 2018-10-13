using Minesweeper_RC.Model;

namespace Minesweeper_RC.Event
{
    public class NewGameRequestedMessage : IMessage
    {
        public IGame Game { get; private set; }

        public NewGameRequestedMessage(IGame game)
        {
            // used by MainForm (if game exists, create new with same skill level)
            Game = game;
        }
    }
}
