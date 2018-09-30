using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper_RC
{
    public class Game
    {
        private int _numRevealed;

        public class FieldSettings
        {
            public int Width { get; private set; }
            public int Height { get; private set; }
            public int MineCount { get; private set; }
            public FieldSettings(int width, int height, int mineCount)
            {
                Width = width;
                Height = height;
                MineCount = mineCount;
            }
        }

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

        public enum GameState
        {
            Stopped = 0,
            Starting,
            Running
        }

        public enum GameResult
        {
            Success = 0,
            Failure
        }

        public enum SkillLevel
        {
            Beginner = 0,
            Intermediate,
            Expert
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
        /// See documentation for Reveal(x, y).
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Cell[] Reveal(Point p)
        {
            return Reveal(p.X, p.Y);
        }

        /// <summary>
        /// Reveals a cell at the specified location. If cell is a mine, the game ends with a failed result.
        /// If the cell is a blank (no adjacent mines), all adjacent unrevealed non-mine cells are also revealed.
        /// If only mines are left, regardless of flag status, the game ends with a success result.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>An array of cells that were revealed. First element is always the specified cell, the rest
        /// in the order they were revealed in.</returns>
        public Cell[] Reveal(int x, int y)
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

            // is the game over?
            if (cell.IsMine || Minefield.AsFlatArray().Length - Settings.MineCount == _numRevealed)
            {
                State = GameState.Stopped;
                Result = cell.IsMine ? GameResult.Failure : GameResult.Success;
                // reveal all unrevealed cells
                var revealedCells = new List<Cell> { cell };
                for (var fy = 0; fy < Settings.Height; fy++)
                {
                    for (var fx = 0; fx < Settings.Width; fx++)
                    {
                        if (!Minefield.Get(x, y).IsRevealed)
                        {
                            Minefield.Get(x, y).IsRevealed = true;
                            revealedCells.Add(Minefield.Get(x, y));
                        }
                    }
                }
                return revealedCells.ToArray();
            }

            // reveal any unrevealed non-mine neighbours if cell is blank
            if (cell.Neighbours == 0)
            {
                var revealedCells = new List<Cell> { cell };
                Field.GetAdjacentPoints(cell.Location, Settings.Width, Settings.Height).ToList().ForEach(p =>
                {
                    var adjacent = Minefield.Get(p.X, p.Y);
                    if (!adjacent.IsMine && !adjacent.IsRevealed)
                    {
                        // recursively reveal if the adjacent is also a blank, otherwise just reveal it
                        if (adjacent.Neighbours == 0)
                            revealedCells.AddRange(Reveal(adjacent.Location.X, adjacent.Location.Y));
                        else
                        {
                            adjacent.IsRevealed = true;
                            revealedCells.Add(adjacent);
                        }
                    }
                });
                return revealedCells.ToArray();
            }

            return new Cell[] { cell };
        }
    }
}
