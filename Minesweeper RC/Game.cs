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

        private Cell[,] _field;

        public Cell[,] Field
        {
            get => (Cell[,])_field.Clone();
            private set => _field = value;
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

        public Point Start
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
            var result = GenerateField(Settings.Width, Settings.Height, Settings.MineCount);
            // yep .NET is still somewhat retarded...
            Start = result.Item1;
            Field = result.Item2;
        }

        public Cell Reveal(int x, int y)
        {
            if (State != GameState.Running)
                throw new InvalidOperationException("Game is not running");
            if (x < 0 || x > Settings.Width - 1)
                throw new ArgumentOutOfRangeException("x");
            if (y < 0 || y > Settings.Height - 1)
                throw new ArgumentOutOfRangeException("y");

            // reveal cell
            var cell = Field[x, y];
            if (cell.IsRevealed)
                throw new InvalidOperationException("Cell is already revealed");
            if (cell.IsFlagged)
                throw new InvalidOperationException("Cell is flagged as a mine");
            cell.Reveal();
            _numRevealed++;

            // have we blown up?
            if (cell.IsMine)
            {
                State = GameState.Stopped;
                Result = GameResult.Failure;
                for (var fy = 0; fy < Settings.Height; fy++)
                    for (var fx = 0; fx < Settings.Width; fx++)
                        Field[x, y].Reveal();
            }

            // did we win?
            if (Field.Length - Settings.MineCount == _numRevealed)
            {
                State = GameState.Stopped;
                Result = GameResult.Success;
                for (var fy = 0; fy < Settings.Height; fy++)
                    for (var fx = 0; fx < Settings.Width; fx++)
                        Field[x, y].Reveal();
            }

            return cell;
        }

        private static Tuple<Point, Cell[,]> GenerateField(int width, int height, int numMines)
        {
            /*
             * Generates and returns a new field, along with a safe starting location.
             */

            // 'available' holds all point permutations for faster mine assignment
            var field = new Cell[width, height];
            var available = new List<Point>(capacity: width * height);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    field[x, y] = new Cell(false, 0, x, y);
                    available.Add(new Point(x, y));
                }
            }

            var rand = new Random();

            // make a safe start location up to 3x3 in size
            var start = available[rand.Next(0, available.Count)];
            var cluster = GetSquareCluster(start, 3, width, height);
            cluster.Select((p, _) => available.Remove(p));

            // place mines and re-calculate neighbours
            for (var i = 0; i < numMines; i++)
            {
                // mine
                var loc = available[rand.Next(0, available.Count)];
                available.Remove(loc);
                field[loc.X, loc.Y].IsMine = true;
                
                // neighbours
                cluster = GetSquareCluster(loc, 3, width, height);
                cluster.Select((p, _) => field[p.X, p.Y].Neighbours++);
            }

            // prevent further editing to cells
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    field[x, y].Lock();

            return new Tuple<Point, Cell[,]>(start, field);
        }

        private static Point[] GetSquareCluster(Point center, int size, int maxWidth, int maxHeight)
        {
            /*
             * Returns an array of co-ordinates that make up square cluster of
             * specified size with specified center. Co-ordinates are bound
             * from (1,1) to (maxWidth,maxHeight).
             */
            if (size % 2 == 0 || size < 1)
                throw new ArgumentOutOfRangeException("size must be an odd positive number");
            var range = (size - 1) / 2;
            var cluster = new List<Point>();
            for (var y = Math.Max(center.Y - range, 1); y <= Math.Min(center.Y + range, maxHeight); y++)
                for (var x = Math.Max(center.X - range, 1); x <= Math.Min(center.X + range, maxWidth); x++)
                    cluster.Add(new Point(x, y));
            return cluster.ToArray();
        }
    }
}
