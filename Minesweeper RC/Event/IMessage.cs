using Minesweeper_RC.Model;

namespace Minesweeper_RC.Event
{
    /// <summary>
    /// A message to be sent over the event/message bus.
    /// </summary>
    public interface IMessage
    {
        IGame Game { get; }
    }
}
