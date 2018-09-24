using System;
using System.Drawing;

namespace Minesweeper_RC
{
    public class Cell
    {
        private bool _isMine;
        private int _neighbours;
        private Point _location;

        public bool IsMine
        {
            get
            {
                if (Locked && !IsRevealed)
                    throw new InvalidOperationException("Cell is not revealed");
                return _isMine;
            }
            set
            {
                if (Locked)
                    throw new InvalidOperationException("Cell is locked");
                _isMine = value;
            }
        }

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

        public int Neighbours
        {
            get
            {
                if (Locked && !IsRevealed)
                    throw new InvalidOperationException("Cell is not revealed");
                return _neighbours;
            }
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
            private set;
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

        public void Reveal()
        {
            IsRevealed = true;
        }
    }
}
