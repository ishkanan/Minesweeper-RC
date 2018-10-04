using System;
using System.Drawing;

namespace Minesweeper_RC.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Multiplies a Size instance by a scalar.
        /// </summary>
        /// <param name="size">The Size instance.</param>
        /// <param name="factor">The scalar.</param>
        /// <returns></returns>
        public static Size MultiplyBy(this Size size, double factor)
        {
            return new Size((int)(size.Width * factor), (int)(size.Height * factor));
        }

        /// <summary>
        /// Multiplies a Size instance by an X and Y scalar.
        /// </summary>
        /// <param name="size">The Size instance.</param>
        /// <param name="Xfactor">The X scalar.</param>
        /// <param name="Yfactor">The Y scalar.</param>
        /// <returns></returns>
        public static Size MultiplyBy(this Size size, double XFactor, double YFactor)
        {
            return new Size((int)(size.Width * XFactor), (int)(size.Height * YFactor));
        }

        /// <summary>
        /// Flattens a 2D rectangular array to a 1D array. Elements are shallow-copied into
        /// the new array in this order - (0:0, 0:1 .. 0:X, 1:0, 1:1, ...)
        /// </summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="values">The array to flatten.</param>
        /// <returns>The flattened array.</returns>
        public static T[] Flatten<T>(this T[,] values)
        {
            var flat = new T[values.Length];
            var rowSize = values.GetLength(0);
            for (var y = 0; y < values.GetLength(1); y++)
                for (var x = 0; x < values.GetLength(0); x++)
                    flat[(y * rowSize) + x] = values[y, x];
            return flat;
        }
    }
}
