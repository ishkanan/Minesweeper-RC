using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper_RC
{
    public class Field
    {
        /// <summary>
        /// Generates a new minefield of the specified size with specified mine count. Each
        /// field has a safe space of minimum size 3x3 whose center can be used as a start location.
        /// All cell locations are 0-based.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="numMines"></param>
        /// <returns>A tuple containing the center of the safe space, and a 2D array of cells
        /// representing the minefield.</returns>
        public static Tuple<Point, Cell[,]> Generate(int width, int height, int numMines)
        {
            if (width < 4)
                throw new ArgumentOutOfRangeException("width must be greater than 3");
            if (height < 4)
                throw new ArgumentOutOfRangeException("height must be greater than 3");
            if (numMines < 1)
                throw new ArgumentOutOfRangeException("numMines must be greater than 0");
            if (numMines > (width * height) - 9)
                throw new ArgumentOutOfRangeException("Too many mines for the specified size");

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
            var adjacents = GetAdjacentPoints(start, width, height);
            adjacents.ToList().ForEach(p => available.Remove(p));

            // place mines and re-calculate neighbours
            for (var i = 0; i < numMines; i++)
            {
                // mine
                var loc = available[rand.Next(0, available.Count)];
                available.Remove(loc);
                field[loc.X, loc.Y].IsMine = true;

                // neighbours
                adjacents = GetAdjacentPoints(loc, width, height);
                adjacents.ToList().ForEach(p => field[p.X, p.Y].Neighbours++);
            }

            // prevent further editing to cells
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    field[x, y].Lock();

            return new Tuple<Point, Cell[,]>(start, field);
        }

        /// <summary>
        /// Returns the points (co-ordinates) that surround/touch the specified center.
        /// Co-ordinates are between (0, 0) inclusive to (maxWidth, maxHeight) exclusive.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns>An array of the neighbour points, including the center.
        /// The rows of points are flattened out in order (e.g. [(0,0), (1,0), (0,1), (1,1)]
        /// where (x,y) are co-ordinate pairs)</returns>
        public static Point[] GetAdjacentPoints(Point center, int fieldWidth, int fieldHeight)
        {
            if (fieldWidth < 1)
                throw new ArgumentOutOfRangeException("fieldWidth must be greater than 0");
            if (fieldHeight < 1)
                throw new ArgumentOutOfRangeException("fieldHeight must be greater than 0");
            if (center == null)
                throw new ArgumentNullException("center");
            if (center.X < 0 || center.Y < 0 || center.X >= fieldWidth || center.Y >= fieldHeight)
                throw new ArgumentOutOfRangeException("center must be (0,0) < (x,y) < (fieldWidth,fieldHeight)");

            var points = new List<Point>();
            for (var y = Math.Max(center.Y - 1, 0); y <= Math.Min(center.Y + 1, fieldHeight - 1); y++)
                for (var x = Math.Max(center.X - 1, 0); x <= Math.Min(center.X + 1, fieldWidth - 1); x++)
                    points.Add(new Point(x, y));
            return points.ToArray();
        }
    }
}
