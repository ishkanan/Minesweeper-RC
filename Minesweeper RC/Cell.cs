using System;
using System.Drawing;

namespace Minesweeper_RC
{
    public class Cell
    {
        private bool _isMine;
        private int _neighbours;
        private Point _location;

        /// <summary>
        /// Gets or sets if this cell contains a mine.
        /// </summary>
        public bool IsMine
        {
            get => _isMine;
            set
            {
                if (Locked)
                    throw new InvalidOperationException("Cell is locked");
                _isMine = value;
            }
        }

        /// <summary>
        /// Gets or sets if this cell is flagged as a mine.
        /// </summary>
        public bool IsFlagged
        {
            get;
            set;
        }

        /// <summary>
        /// True if the cell can no longer be edited (i.e. during setup).
        /// </summary>
        public bool Locked
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the number of mines adjacent to this cell.
        /// </summary>
        public int Neighbours
        {
            get => _neighbours;
            set
            {
                if (Locked)
                    throw new InvalidOperationException("Cell is locked");
                _neighbours = value;
            }
        }

        public Point Location
        {
            get => _location;
            set
            {
                if (Locked)
                    throw new InvalidOperationException("Cell is locked");
                _location = value;
            }
        }

        public bool IsRevealed
        {
            get;
            set;
        }

        public Cell(bool isMine, int neighbours, int x, int y)
        {
            IsMine = isMine;
            Locked = false;
            Neighbours = neighbours;
            IsFlagged = false;
            Location = new Point(x, y);
            IsRevealed = false;
        }

        /// <summary>
        /// Permanently disables edit mode for the cell.
        /// </summary>
        public void Lock()
        {
            Locked = true;
        }
    }
}
