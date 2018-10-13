using Minesweeper_RC.Model;

namespace Minesweeper_RC.Event
{
    /// <summary>
    /// A cell-related message to be sent over the event/message bus.
    /// </summary>
    public interface ICellMessage: IMessage
    {
        /// <summary>
        /// The cell that caused this message to be generated.
        /// </summary>
        Cell PrimaryCell { get; }
        /// <summary>
        /// Additional cells related to the operation performed on the primary cell.
        /// (e.g. additional cells that were revealed)
        /// </summary>
        Cell[] SecondaryCells { get; }
    }
}
