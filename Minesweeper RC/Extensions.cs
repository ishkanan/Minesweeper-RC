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
    }
}
