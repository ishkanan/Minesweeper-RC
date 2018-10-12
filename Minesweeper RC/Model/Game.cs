using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper_RC.Model
{
    public class Game : IGame
    {
        private int _numRevealed;

        public static FieldSettings GetFieldSettings(SkillLevel level)
        {
            switch (level)
            {
                case SkillLevel.Beginner:
                    return new FieldSettings(8, 8, 10);
                case SkillLevel.Intermediate:
                    return new FieldSettings(16, 16, 40);
                case SkillLevel.Expert:
                    return new FieldSettings(24, 24, 99);
                default:
                    throw new ArgumentOutOfRangeException("Unknown skill level");
            }
        }

        public Field Minefield
        {
            get;
            private set;
        }

        public GameState State
        {
            get;
            private set;
        }

        public SkillLevel Level
        {
            get;
            private set;
        }

        public FieldSettings Settings
        {
            get;
            private set;
        }

        public Nullable<GameResult> Result
        {
            get;
            private set;
        }

        public Game(SkillLevel level)
        {
            Level = level;
            State = GameState.Stopped;
            Settings = GetFieldSettings(level);
            Minefield = new Field(Settings.Width, Settings.Height, Settings.MineCount);
            Result = null;
        }

        /// <summary>
        /// Starts the game. This does nothing if game is already started.
        /// </summary>
        /// <exception cref="InvalidOperationException">If game has already been played.</exception>
        public void Start()
        {
            if (Result != null)
                throw new InvalidOperationException("Game has expired");
            State = GameState.Running;
        }

        /// <summary>
        /// See documentation for Reveal(int, int, bool).
        /// </summary>
        /// <param name="p">Point holding X and Y co-ordinates of the cell.</param>
        /// <param name="confident">See documentation for Reveal(int, int, bool).</param>
        /// <returns>See documentation for Reveal(int, int, bool).</returns>
        public Cell[] Reveal(Point p, bool confident=false)
        {
            return Reveal(p.X, p.Y, confident);
        }

        /// <summary>
        /// Reveals a cell at the specified location. If cell is a mine, the game ends with a failed result.
        /// If the cell is a blank (no adjacent mines), all adjacent unrevealed non-mine cells are also revealed.
        /// If only mines are left, regardless of flag status, the game ends with a success result.
        /// </summary>
        /// <param name="x">X co-ordinate of cell.</param>
        /// <param name="y">Y co-ordinate of cell.</param>
        /// <param name="confident">If true, will reveal all non-flagged non-revealed neighbour cells if the number
        /// of flagged cells equals at least the number of mines in the neighbourhood, regardless if the flags are
        /// set on the correct cells. Traditionally this occurred when both left and right mouse buttons were pressed.
        /// </param>
        /// <returns>An array of cells that were revealed. First element is always the specified cell, the rest
        /// in the order they were revealed in.</returns>
        public Cell[] Reveal(int x, int y, bool confident=false)
        {
            if (State != GameState.Running)
                throw new InvalidOperationException("Game is not running");
            if (x < 0 || x > Settings.Width - 1)
                throw new ArgumentOutOfRangeException("x");
            if (y < 0 || y > Settings.Height - 1)
                throw new ArgumentOutOfRangeException("y");

            // reveal cell
            var cell = Minefield.Get(x, y);
            if (cell.IsRevealed)
                throw new InvalidOperationException("Cell is already revealed");
            if (cell.IsFlagged)
                throw new InvalidOperationException("Cell is flagged as a mine");
            cell.IsRevealed = true;
            _numRevealed++;
            var revealedCells = new List<Cell>(Minefield.CellCount) { cell };

            // is the game over? if so, reveal all unrevealed cells
            if (cell.IsMine || Minefield.CellCount - Settings.MineCount == _numRevealed)
            {
                State = GameState.Stopped;
                Result = cell.IsMine ? GameResult.Failure : GameResult.Success;
                revealedCells.AddRange(RevealAllNonFlaggedCells());
                return revealedCells.ToArray();
            }

            // reveal any unrevealed non-mine neighbours if cell is blank
            var neighbours = Field.GetAdjacentPoints(cell.Location, Settings.Width, Settings.Height);
            if (cell.Neighbours == 0)
            {
                neighbours.ToList().ForEach(p =>
                {
                    var adjacent = Minefield.Get(p.X, p.Y);
                    if (!adjacent.IsFlagged && !adjacent.IsMine && !adjacent.IsRevealed)
                    {
                        // recursively reveal if the adjacent is also a blank, otherwise just reveal it
                        if (adjacent.Neighbours == 0)
                            revealedCells.AddRange(Reveal(adjacent.Location, confident));
                        else
                        {
                            adjacent.IsRevealed = true;
                            _numRevealed++;
                            revealedCells.Add(adjacent);
                        }
                    }
                });
            }

            // confident reveal?
            if (confident)
            {
                // get neighbours and count mines and flags
                var flagCount = neighbours.Where(p => Minefield.Get(p).IsFlagged).Count();
                var mineCount = neighbours.Where(p => Minefield.Get(p).IsMine).Count();
                if (flagCount >= mineCount)
                {
                    neighbours.ToList().ForEach(p =>
                    {
                        var adjacent = Minefield.Get(p.X, p.Y);
                        if (!adjacent.IsFlagged && !adjacent.IsRevealed)
                            revealedCells.AddRange(Reveal(adjacent.Location, confident));
                    });
                }
            }

            return revealedCells.ToArray();
        }

        /// <summary>
        /// Reveals all unrevealed, non-flagged cells and returns the cells that were revealed.
        /// </summary>
        /// <returns></returns>
        private List<Cell> RevealAllNonFlaggedCells()
        {
            var revealedCells = new List<Cell>(Minefield.CellCount);
            for (var fy = 0; fy < Settings.Height; fy++)
            {
                for (var fx = 0; fx < Settings.Width; fx++)
                {
                    var cell = Minefield.Get(fx, fy);
                    if (!cell.IsFlagged && !cell.IsRevealed)
                    {
                        cell.IsRevealed = true;
                        revealedCells.Add(cell);
                    }
                }
            }
            return revealedCells;
        }
    }

    /// <summary>
    /// Possible game states.
    /// </summary>
    public enum GameState
    {
        Stopped = 0,
        Starting,
        Running
    }

    /// <summary>
    /// Possible game results.
    /// </summary>
    public enum GameResult
    {
        Success = 0,
        Failure
    }

    /// <summary>
    /// Available skill levels.
    /// </summary>
    public enum SkillLevel
    {
        Beginner = 0,
        Intermediate,
        Expert
    }

    /// <summary>
    /// Interface for a game instance.
    /// </summary>
    public interface IGame
    {
        Field Minefield { get; }
        GameState State { get; }
        SkillLevel Level { get; }
        FieldSettings Settings { get; }
        Nullable<GameResult> Result { get; }
        void Start();
        Cell[] Reveal(Point p, bool confident);
        Cell[] Reveal(int x, int y, bool confident);
    }
}
