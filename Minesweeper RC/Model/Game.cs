﻿using System;
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
            if (cell.IsMine || Minefield.CellCount - Settings.MineCount == _numRevealed)
            {
                State = GameState.Stopped;
                Result = cell.IsMine ? GameResult.Failure : GameResult.Success;
                // reveal all unrevealed cells
                var revealedCells = new List<Cell> { cell };
                for (var fy = 0; fy < Settings.Height; fy++)
                {
                    for (var fx = 0; fx < Settings.Width; fx++)
                    {
                        cell = Minefield.Get(fx, fy);
                        if (!cell.IsRevealed)
                        {
                            cell.IsRevealed = true;
                            revealedCells.Add(cell);
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
                            revealedCells.AddRange(Reveal(adjacent.Location));
                        else
                        {
                            adjacent.IsRevealed = true;
                            _numRevealed++;
                            revealedCells.Add(adjacent);
                        }
                    }
                });
                return revealedCells.ToArray();
            }

            return new Cell[] { cell };
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
        Cell[] Reveal(Point p);
        Cell[] Reveal(int x, int y);
    }
}
