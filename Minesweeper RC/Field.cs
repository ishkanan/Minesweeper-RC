using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper_RC
{
    public class Field
    {
        private Cell[,] _field;
        private Size _fieldSize;

        /// <summary>
        /// Returns the size of the generated field.
        /// </summary>
        public Size FieldSize
        {
            get => new Size(_fieldSize.Width, _fieldSize.Height);
            private set => _fieldSize = value;
        }

        public int TotalMines
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the field as a 2D (rectangular) array.
        /// </summary>
        /// <returns>The array representing the field. Modifying the
        /// array does not modify the original field object.</returns>
        public Cell[,] As2DArray()
        {
            return (Cell[,])_field.Clone();
        }

        /// <summary>
        /// Returns the field as a flat (1D) array, with rows laid out
        /// in sequential form.
        /// </summary>
        /// <returns>The array representing the field. Modifying the
        /// array does not modify the original field object.</returns>
        public Cell[] AsFlatArray()
        {
            return _field.Cast<Cell>().ToArray();
        }

        /// <summary>
        /// Indicates a single blank point on the field that is suitable
        /// as a safe starting location.
        /// </summary>
        public Point SafeStartLocation
        {
            get;
            private set;
        }

        /// <summary>
        /// Fetches a cell at the specified co-ordinates.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Cell Get(Point p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            return _field[p.X, p.Y];
        }

        /// <summary>
        /// Fetches a cell at the specified co-ordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell Get(int x, int y)
        {
            return _field[x, y];
        }

        /// <summary>
        /// Fetches all cells at the specified locations.
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public Cell[] Get(Point[] locations)
        {
            if (locations == null)
                throw new ArgumentNullException("locations");
            if (locations.Length == 0)
                throw new ArgumentException("locations must contain at least 1 item");
            var cells = new Cell[locations.Length];
            for (var i = 0; i < locations.Length; i++)
                cells[i] = _field[locations[i].X, locations[i].Y];
            return cells;
        }

        /// <summary>
        /// Generates a new minefield of the specified size with specified mine count. Each
        /// field has a safe space of minimum size 3x3 whose center can be used as a start location.
        /// All cell co-ordinates are 0-based.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="numMines"></param>
        public Field(int width, int height, int numMines)
        {
            if (width < 4)
                throw new ArgumentOutOfRangeException("width must be greater than 3");
            if (height < 4)
                throw new ArgumentOutOfRangeException("height must be greater than 3");
            if (numMines < 1)
                throw new ArgumentOutOfRangeException("numMines must be greater than 0");
            if (numMines > (width * height) - 9)
                throw new ArgumentOutOfRangeException("numMines must be <= (W*H)-9");

            FieldSize = new Size(width, height);
            TotalMines = numMines;

            // 'available' holds all point permutations for faster mine assignment
            _field = new Cell[width, height];
            var available = new List<Point>(capacity: width * height);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    _field[x, y] = new Cell(false, 0, x, y);
                    available.Add(new Point(x, y));
                }
            }

            var rand = new Random();

            // make a safe start location up to 3x3 in size
            SafeStartLocation = available[rand.Next(0, available.Count)];
            var adjacents = GetAdjacentPoints(SafeStartLocation, width, height);
            adjacents.ToList().ForEach(p => available.Remove(p));

            // place mines and re-calculate neighbours
            for (var i = 0; i < numMines; i++)
            {
                // mine
                var loc = available[rand.Next(0, available.Count)];
                available.Remove(loc);
                _field[loc.X, loc.Y].IsMine = true;

                // neighbours
                adjacents = GetAdjacentPoints(loc, width, height);
                adjacents.ToList().ForEach(p => _field[p.X, p.Y].Neighbours++);
            }

            // prevent further editing to cells
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    _field[x, y].Lock();
        }

        /// <summary>
        /// Returns the points (co-ordinates) that surround/touch the specified center.
        /// Co-ordinates are between (0, 0) inclusive to (maxWidth, maxHeight) exclusive.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns>An array of the neighbour points, excluding the center.
        /// The rows of points are flattened out in order (e.g. [(0,0), (1,0), (0,1), (1,1)]
        /// where (x,y) are co-ordinate pairs)</returns>
        public static Point[] GetAdjacentPoints(Point center, int fieldWidth, int fieldHeight)
        {
            if (fieldWidth < 1)
                throw new ArgumentOutOfRangeException("fieldWidth must be greater than 0");
            if (fieldHeight < 1)
                throw new ArgumentOutOfRangeException("fieldHeight must be greater than 0");
            if (center.X < 0 || center.Y < 0 || center.X >= fieldWidth || center.Y >= fieldHeight)
                throw new ArgumentOutOfRangeException("center must be (0,0) < (x,y) < (fieldWidth,fieldHeight)");

            var points = new List<Point>();
            for (var y = Math.Max(center.Y - 1, 0); y <= Math.Min(center.Y + 1, fieldHeight - 1); y++)
            {
                for (var x = Math.Max(center.X - 1, 0); x <= Math.Min(center.X + 1, fieldWidth - 1); x++)
                {
                    var p = new Point(x, y);
                    if (p != center)
                        points.Add(p);
                }
            }
            return points.ToArray();
        }
    }
}
