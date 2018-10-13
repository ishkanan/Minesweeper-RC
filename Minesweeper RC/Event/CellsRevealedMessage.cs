using Minesweeper_RC.Model;
using System;

namespace Minesweeper_RC.Event
{
    public class CellsRevealedMessage : ICellMessage
    {
        private Cell[] _secondaryCells;

        public Cell PrimaryCell { get; private set; }

        public Cell[] SecondaryCells
        {
            get => (Cell[])_secondaryCells.Clone();
            private set => _secondaryCells = value;
        }

        public IGame Game { get; private set; }

        public CellsRevealedMessage(IGame game, Cell primary, Cell[] secondary)
        {
            this.Game = game ?? throw new ArgumentNullException("game");
            this.PrimaryCell = primary ?? throw new ArgumentNullException("primary");
            this.SecondaryCells = secondary ?? throw new ArgumentNullException("secondary");
        }
    }
}
